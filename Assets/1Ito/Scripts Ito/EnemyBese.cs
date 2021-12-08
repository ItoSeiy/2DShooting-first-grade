using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの基底クラス
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyBese : MonoBehaviour, IDamage
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
    protected virtual void Attack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    protected virtual void OnGetDamage()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual void AddDamage(float damage)
    {
        _enemyHp -= damage;
        OnGetDamage();
    }
}
