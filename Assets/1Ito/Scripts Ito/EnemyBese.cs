using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの基底クラス
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyBese : MonoBehaviour
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
        }
    }
    public virtual void Attack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    public virtual void OnGetDamage()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    private void SetDamage(float damage)
    {
        _enemyHp -= damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            var bullet = collision.GetComponent<BulletBese>();
            SetDamage(bullet.Damege);
            
            OnGetDamage();
        }

        if(EnemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
