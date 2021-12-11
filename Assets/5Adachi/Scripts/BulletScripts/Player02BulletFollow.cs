using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02BulletFollow : BulletBese
{
    [SerializeField,Header("Bulletのスピード")] float m_speed = 1f;
    Vector2 _direction;
    GameObject _enemy;
    Rigidbody2D _rb;
    float _timer;
    [SerializeField, Header("追従する時間")] float _followTime = 2f;

    private void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Player");
        if(_enemy)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _followTime) return;
        _direction = _enemy.transform.position - this.transform.position;
        _direction = _direction.normalized * m_speed;
        _rb.velocity = _direction;
    }
}
