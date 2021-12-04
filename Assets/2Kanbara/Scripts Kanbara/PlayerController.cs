using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _movespeed = 3;
    [SerializeField] float _latemove = 1;
    int[] _bulletindex = new int[] { 0, 1, 2 };
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Fire3"))
        {
            Vector2 go = new Vector2(h, v).normalized;
            _rb.velocity = go * _latemove;
        }
        else
        {
            Vector2 go = new Vector2(h, v).normalized;
            _rb.velocity = go * _movespeed;
        }

        if (Input.GetButton("Fire1"))
        {


        }
    }
}
