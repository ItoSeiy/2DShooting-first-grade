using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy(中ボス,ボス)の基底クラス
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class EnemyBese : MonoBehaviour
{
    public float EnemyHp { get => _enemyHp;}

    [SerializeField] private float _enemyHp = default;
    private float _damageValue = default;


    private void Update()
    {
        Attack();
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
            //var bullet = collision.GetComponent<>();
            //_damageValue = bullet.GetDamegeValue();

            OnGetDamage();
            SetDamage(_damageValue);
        }

        if(EnemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
