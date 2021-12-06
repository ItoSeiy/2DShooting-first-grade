using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] Vector2 _bulletspeed = default;
    float _time;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(_bulletspeed, ForceMode2D.Impulse);
    }
}
