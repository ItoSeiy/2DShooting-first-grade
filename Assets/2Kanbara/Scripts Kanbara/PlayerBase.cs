using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerBase : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField, Header("動くスピード")] float _moveSpeed = default;
    [SerializeField, Header("精密操作のスピード")] float _lateMove = default;
    [SerializeField, Header("弾を発射するポジション")] public Transform _muzzle = default;
    [SerializeField, Header("弾")] public GameObject[] _bullet = default;
    [SerializeField, Header("精密操作時の弾")] public GameObject[] _superBullet = default;
    [SerializeField, Header("残機")] int _playerLife = default;
    [SerializeField, Header("発射する間隔(ミリ秒)")] public int _attackDelay = default;
    [SerializeField, Header("精密操作時の発射する間隔(ミリ秒)")] public int _superAttackDelay = default;
    [SerializeField, Header("ボムのクールタイム（ミリ秒）")] public int _bomCoolTime = default;
    [SerializeField, Header("Enemyのタグ")] string _enemyTag = default;
    [SerializeField, Header("EnemyのBulletのタグ")] string _enemyBulletTag = default;
    [SerializeField, Header("Powerのタグ")] string _powerTag = default;
    [SerializeField, Header("Pointのタグ")] string _pointTag = default;
    [SerializeField, Header("1upのタグ")] string _1upTag = default;
    [SerializeField, Header("Invincibleのタグ")] string _invincibleTag = default;
    [SerializeField, Header("この数値以上なら、一定時間無敵モードになる変数")] int _invincibleMax = default;
    [SerializeField, Header("無敵モードのクールタイム")] public int _invincibleCoolTime = default;
    [SerializeField, Header("Playerのパワーの上限")] int _playerPowerMax = default;
    int _bomCount = default;
    public int _playerPower = default;
    int _invincibleObjectCount = default;

    /// <summary>連続で弾を撃てないようにするフラグ</summary>
    public bool _isBulletStop = default;
    /// <summary>精密操作時のフラグ</summary>
    bool _isLateMode = default;
    /// <summary>無敵モードのフラグ</summary>
    public bool _godMode = default;
    /// <summary>ボムの使用時に立つフラグ</summary>
    public bool _isBom = default;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bomCount = GameManager.Instance.BombCount;
        _playerPower = GameManager.Instance.Power;
        _invincibleObjectCount = GameManager.Instance.InvincibleObjectCount;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Fire3"))
        {
            Vector2 dir = new Vector2(h, v).normalized;
            _rb.velocity = dir * _lateMove;
            _isLateMode = true;
        }
        else
        {
            Vector2 dir = new Vector2(h, v).normalized;
            _rb.velocity = dir * _moveSpeed;
            _isLateMode = false;
        }

        if (Input.GetButton("Fire1") && _isLateMode && !_isBulletStop)
        {
            PlayerSuperAttack();
            _isBulletStop = true;
        }
        else if (Input.GetButton("Fire1") && !_isBulletStop)
        {
            PlayerAttack();
            _isBulletStop = true;
        }

        if(Input.GetButtonDown("Jump") && _bomCount != 0 && !_isBom)
        {
            Bom();
            _isBom = true;
            _bomCount -= 1;
        }
    }

    public virtual void PlayerAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    public virtual void PlayerSuperAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }
    public virtual void Bom()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == _enemyTag || collision.gameObject.tag == _enemyBulletTag && !_godMode)
        {
            _playerLife -= 1;
            // ここにリスポーンの処理を呼び出すべきでしょう
            if(_playerLife == 0)
            {
                //ゲームマネージャーからGameOverの関数を呼び出す
            }
        }
        if (collision.gameObject.tag == _powerTag && _playerPower < _playerPowerMax)
        {
            GameManager.Instance.GetPower(1);
        }
        if (collision.gameObject.tag == _pointTag)
        {
            GameManager.Instance.GetScore(1);        
        }
        if (collision.gameObject.tag == _1upTag)
        {
            //1upを取ったら1upが増える処理を書く
        }
        if(collision.gameObject.tag == _invincibleTag)
        {
            GameManager.Instance.GetInvicibleObjectCount(1);
            if(_invincibleObjectCount > _invincibleMax)
            {
                InvincibleMode();
                _invincibleObjectCount = 0;
            }
        }
    }
    public virtual void InvincibleMode()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }
}
