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
    [SerializeField, Header("Bulletの動く向き")] Vector2 _direction = Vector2.up;
    [SerializeField, Header("Bulletのスピード")] float _speed = default;
    private void Start()
    {
        BulletMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletAttack(collision);
    }

    /// <summary>
    /// Bulletがまっすぐ飛ぶ記述がデフォルトではされている
    /// 変則的な動きを行いたければオーバライドをすること
    /// </summary>
    protected virtual void BulletMove()
    {
        var _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _direction.normalized * _speed;
    }

    /// <summary>
    /// 基本的にはオーバライドを行わなくてもよい
    /// 衝突した相手のインタフェースを参照し攻撃を加える関数
    /// オーバライドする際は中身に[base.BulletAttack(col);]と記述する(基底クラスの機能を呼び出せる)
    /// </summary>
    /// <param name="col">当たった相手のコライダー</param>
    protected virtual void BulletAttack(Collider2D col)
    {
        var target = col.gameObject?.GetComponent<IDamage>();
        target.AddDamage(_damage);
    }
}
