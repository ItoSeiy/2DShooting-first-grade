using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの基底クラス
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour, IDamage
{
    public float Speed => _speed;
    public float EnemyHp { get => _enemyHp;}
    public float AddDamageRatio { get => _damageRatio;}
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public float Timer { get => _timer; }
    public float AttackInterval { get => _attackInterval; }
    public string PlayerBulletTag { get => _playerBulletTag; }

    [SerializeField, Header("動きのスピード")] private float _speed = 5f;
    [SerializeField, Header("体力")] private float _enemyHp = 10f;
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 1f;
    /// <summary>
    /// 攻撃力の割合
    /// </summary>
    [SerializeField, Header("攻撃力を何割にするか"), Range(0f, 1f)] float _damageRatio = 1f;
    [SerializeField, Header("プレイヤーのBulletのタグ")] string _playerBulletTag = "PlayerBullet";
    [SerializeField] string _finishTag = "Finish";
    private float _timer = default;
    Rigidbody2D _rb = null;
　　protected bool _isDeleteAble = false;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _attackInterval)
        {
            Attack();
            _timer = 0;
        }
    }

    /// <summary>
    /// 攻撃の処理を書いてください
    /// 例)Bulletプレハブを生成するなど
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// ダメージを受けた際に行う処理を書いてください
    /// 例)ダメージを受けた際のアニメーションなど
    /// </summary>
    protected abstract void OnGetDamage();


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyHp <= 0)
        {
            gameObject.SetActive(false);
        }
        if(collision.tag == _finishTag)
        {
            if(!_isDeleteAble)
            {
                _isDeleteAble = true;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// ダメージを喰らった際にBulletBaseから呼び出される関数
    /// Enemyがダメージを喰らう
    /// 攻撃力のデバフを行う
    /// ダメージを喰らった際の処理もここで呼ばれる
    /// 受けるダメージ量はBulletが指定する
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    public void AddDamage(float damage, Collider2D col)
    {
        //攻撃力を設定した分減らす処理
        damage *= _damageRatio;

        _enemyHp -= damage;
        OnGetDamage();
    }
   
    
}
