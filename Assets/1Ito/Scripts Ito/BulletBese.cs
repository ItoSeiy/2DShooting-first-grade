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
    
    [SerializeField, Header("Bulletが与えるダメージ")] private float _damege;

    private void Update()
    {
        BulletMovement();
    }

    public virtual void BulletMovement()
    {
        Debug.LogError("弾又は爆弾等の移動の処理を派生クラスからオーバーライドしてください。");
    }
}
