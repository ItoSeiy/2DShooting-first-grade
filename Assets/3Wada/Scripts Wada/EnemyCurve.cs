
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// まっすぐ下に移動し、トリガーに当たったら曲げるように動かすコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCurve : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float _speed = 1f;
    /// <summary>カーブする時にかける力</summary>
    [SerializeField] float _chasingPower = 1f;
    Rigidbody2D _rb;
    void Start()
    {
        // まずまっすぐ下に移動させる
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.down * _speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //トリガーに触れたら曲がる
        _rb.AddForce(Vector2.right * _chasingPower);
    }
}