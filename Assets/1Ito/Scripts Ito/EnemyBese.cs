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
    [SerializeField] public float _enemyHp = default;

    public virtual void Attack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            //var bullet = GetComponent<Bullet>();
            //_enemyHp -= 
        }

        if(_enemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
