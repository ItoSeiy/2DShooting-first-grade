using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMuve : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    public Rigidbody2D _rb;

    void Start()
    {
        // ‚Ü‚¸‚Ü‚Á‚·‚®‰º‚ÉˆÚ“®‚³‚¹‚é
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector2.down * _speed;
    }



}
