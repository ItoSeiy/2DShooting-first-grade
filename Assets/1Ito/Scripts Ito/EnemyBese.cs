using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy(中ボス,ボス)の基底クラス
/// </summary>
public class EnemyBese : MonoBehaviour
{
    [SerializeField] float _enemyHp = default;

    public virtual void Attack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(_enemyHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
