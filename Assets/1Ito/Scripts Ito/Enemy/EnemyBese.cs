using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの基底クラス
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBese : MonoBehaviour, IDamage
{
    public float EnemyHp { get => _enemyHp;}
    public float AddDamageRatio { get => _damageRatio;}
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public float Timer { get => _timer; }
    public float AttackInterval { get => _attackInterval; }

    [SerializeField, Header("体力")] private float _enemyHp = 10f;
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 1f;
    /// <summary>
    /// 攻撃力の割合
    /// </summary>
    [SerializeField, Header("攻撃力を何割にするか"), Range(0f, 1f)] float _damageRatio = 1f;
    private float _timer = default;
    Rigidbody2D _rb = null;

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
    }

    /// <summary>
    /// ダメージを喰らった際にBulletBaseから呼び出される関数
    /// Enemyがダメージを喰らう
    /// 攻撃力のデバフを行う
    /// ダメージを喰らった際の処理もここで呼ばれる
    /// 受けるダメージ量はBulletが指定する
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    void IDamage.AddDamage(float damage)
    {
        //攻撃力を設定した分減らす処理
        damage *= _damageRatio;

        _enemyHp -= damage;
        OnGetDamage();
    }
}
