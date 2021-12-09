using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bulletの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BulletBese : MonoBehaviour
{
    public float Damege { get => _damege;}
    
    [SerializeField, Header("Bulletが与えるダメージ")] protected float _damege;

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletAttack(collision);
    }

    /// <summary>
    /// Bulletの基底クラスの関数
    /// 衝突した相手のインタフェースを参照し攻撃を加える関数
    /// </summary>
    /// <param name="col">当たった相手のコライダー</param>
    protected virtual void BulletAttack(Collider2D col)
    {
        var target = col.gameObject?.GetComponent<IDamage>();
        target.AddDamage(_damege);
    }
}
