using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveStraight : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField, Header("ˆÚ“®‘¬“x")] public float _moveSpeed = default;
    float v = 0f;
    private void Update()
    {
        v += 1f;
        Vector2 dir = new Vector2(0, v);
        _rb.velocity = dir * _moveSpeed;
    }
}
