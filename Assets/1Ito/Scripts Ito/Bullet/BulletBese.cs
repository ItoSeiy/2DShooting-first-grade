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
    [SerializeField, Header("Bulletの動きをどの関数で呼び出すか")] BulletMoveMethod _bulletMoveMethod = BulletMoveMethod.Start;
    private void Start()
    {
        if(_bulletMoveMethod == BulletMoveMethod.Start)
            BulletMove();
    }

    private void Update()
    {
        if (_bulletMoveMethod == BulletMoveMethod.Update)
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
    /// 衝突した相手のインタフェース(IDamage)を参照し攻撃を加える関数
    /// オーバライドする際は中身に[base.BulletAttack(col);]と記述する(基底クラスの機能を呼び出せる)
    /// </summary>
    /// <param name="col">当たった相手のコライダー</param>
    protected virtual void BulletAttack(Collider2D col)
    {
        var target = col.gameObject?.GetComponent<IDamage>();
        target.AddDamage(_damage);
    }

    enum BulletMoveMethod
    {
        /// <summary>
        /// スタート関数で呼び出す
        /// </summary>
        Start,
        /// <summary>
        /// アップデート関数で呼び出す
        /// </summary>
        Update
    }
}
