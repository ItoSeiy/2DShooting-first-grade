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

    [SerializeField, Header("体力")] private float _enemyHp = default;
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = default;
    private float _timer = default;

    private void Update()
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
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// ダメージを受けた際の処理を書いてください
    /// </summary>
    protected abstract void OnGetDamage();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ダメージを喰らった際にBulletBaseから呼び出される関数
    /// Enemyがダメージを喰らう
    /// ダメージを喰らった際の処理もここで呼ばれる
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    void IDamage.AddDamage(float damage)
    {
        _enemyHp -= damage;
        OnGetDamage();
    }
}
