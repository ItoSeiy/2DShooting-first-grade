using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bulletの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class BulletBese : MonoBehaviour
{
    public float Damage { get => _damage;}
    
    [SerializeField, Header("Bulletが与えるダメージ")] private float _damage;

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletAttack(collision);
    }

    /// <summary>
    /// Bulletの基底クラスの関数
    /// 衝突した相手のインタフェースを参照し攻撃を加える関数
    /// 派生クラスではbase.BulletAttack(col);と記述する(基底クラスの機能を呼び出せる)
    /// </summary>
    /// <param name="col">当たった相手のコライダー</param>
    protected virtual void BulletAttack(Collider2D col)
    {
        var target = col.gameObject?.GetComponent<IDamage>();
        target.AddDamage(_damage);
    }
}
