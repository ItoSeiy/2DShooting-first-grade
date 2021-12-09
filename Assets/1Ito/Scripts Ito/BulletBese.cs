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
        BulletDamege(collision);
    }

    protected virtual void BulletDamege(Collider2D col)
    {
        var target = col.gameObject?.GetComponent<IDamage>();
        target.AddDamage(_damege);
    }
}
