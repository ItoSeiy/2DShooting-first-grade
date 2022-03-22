using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bulletの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BulletBese : MonoBehaviour, IPauseable
{
    public float Damage { get => _damage;}
    public float Speed { get => _speed;}
    /// <summary>相手のタグ</summary>
    public string OpponenTag { get => _opponentTag; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    private BulletMoveMethod MoveMethod { get => _bulletMoveMethod; set => _bulletMoveMethod = value; }
    public string GameZoneTag { get => _gameZoneTag;}

    [SerializeField, Header("Bulletが与えるダメージ")]
    private float _damage = 10f;

    [SerializeField, Header("Bulletのスピード")] 
    float _speed = 4f;

    [SerializeField, Header("基底クラスのStraightBulletの移動方向")]
    Vector2 _direciton = Vector2.up;

    [SerializeField, Header("Bulletの動きをどの関数で呼び出すか")]
    BulletMoveMethod _bulletMoveMethod = BulletMoveMethod.Update;

    [SerializeField, Header("相手(当たったら消えるオブジェクトのタグ")] 
    string _opponentTag;

    [SerializeField, Header("壁のタグ")] 
    string _gameZoneTag = "Finish";

    [SerializeField, Header("Bulletの親オブジェクトのタグ")]
    string _parentTag = "Parent";


    Rigidbody2D _rb = null;
    BulletParent _bulletParent = null;

    Vector2 _oldVelocity;

    protected virtual void Awake()
    {
        if(string.IsNullOrWhiteSpace(_opponentTag))
        {
            Debug.LogError($"{gameObject.name}のOpponenTagタグが設定されていません\n設定してください");
        }
        _rb = GetComponent<Rigidbody2D>();
        _bulletParent = transform.parent?.GetComponent<BulletParent>();
    }

    protected virtual void OnEnable()
    {
        PauseManager.Instance.SetEvent(this);
        if(_bulletMoveMethod == BulletMoveMethod.Start)
        {
            if (PauseManager.Instance.PauseFlg == true)
            {
                Debug.Log(PauseManager.Instance.PauseFlg + "ポーズ");
                _rb.velocity = Vector2.zero;
                return;
            }
            BulletMove();
        }
    }

    protected virtual void OnDisable()
    {
        PauseManager.Instance.RemoveEvent(this);
    }

    protected virtual void Update()
    {
        if (_bulletMoveMethod == BulletMoveMethod.Update)
        {
            if (PauseManager.Instance.PauseFlg == true)
            {
                _rb.velocity = Vector2.zero;
                return;
            }
            BulletMove();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //敵または壁に当たったら子オブジェクトを非アクティブにする
        if(collision.tag == _opponentTag || collision.tag == _gameZoneTag)
        {
            BulletAttack(collision);
            //子オブジェクトがまだ残っていたら子オブジェクトを非アクティブにする
            if(gameObject.activeInHierarchy)
            {
                this.gameObject.SetActive(false);
            }

            //子オブジェクトが残っていなかったら子オブジェクトをアクティブにし、親を非アクティブにする
            if (_bulletParent && _bulletParent.AllBulletChildrenDisable() && _bulletParent.tag == _parentTag)
            {
                _bulletParent?.ChildrenBulletEnable();
                _bulletParent?.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Bulletがまっすぐ飛ぶ記述がデフォルトではされている
    /// 変則的な動きを行いたければオーバライドをすること
    /// この関数がStart関数かUpdate関数で呼ばれるかはインスペクター上から変更すること
    /// </summary>
    protected virtual void BulletMove()
    {
        _rb.velocity = _direciton.normalized * _speed;
    }

    /// <summary>
    /// 基本的にはオーバライドを行わなくてもよい
    /// 衝突した相手のインタフェース(IDamage)を参照し攻撃を加える関数
    /// オーバライドする際は中身に[base.BulletAttack(col);]と記述する(基底クラスの機能を呼び出せる)
    /// </summary>
    /// <param name="col">当たった相手のコライダー</param>
    protected virtual void BulletAttack(Collider2D col)
    {
        var target = col.gameObject.GetComponent<IAttackble>();
        target?.AddDamage(_damage, col);
    }
    void IPauseable.PauseResume(bool isPause)
    {
        if(isPause)
        {
            _oldVelocity = _rb.velocity;
            _rb.velocity = Vector2.zero;
        }
        else
        {
            _rb.velocity = _oldVelocity;
        }
    }


    enum BulletMoveMethod
    {
        /// <summary>
        /// スタート関数で呼び出す
        /// </summary>
        Start,
        /// <summary>
        /// アップデート関数で呼び出す
        /// </summary>
        Update
    }
}
