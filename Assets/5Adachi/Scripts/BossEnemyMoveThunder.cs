using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    Rigidbody2D _rb;
    int _back = 0;
    int _thunder = 0;
     float _xMove = 5f;
     float _yMove = 6f;
    bool _isMove = false;
    [SerializeField,Header("スピード")] float _speed;
    int sign;
    int _switch = 0;

    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), (Mathf.Clamp(transform.position.y, -4f, 4f)));

        if (_switch == 0)
        {
            if (transform.position.x <= -7.5f)
            {
                Debug.Log("nice!");
                _switch = 1;
                _rb.velocity = new Vector2(0, 0);
            }
            else
            {
                Debug.Log("maane");
                _rb.velocity = new Vector2(-4, 0);
            }
        }
        if (_switch == 1)
        {
            Debug.Log("dayone");
            if (transform.position.x <= 7.5f)
            {
                _rb.velocity = new Vector2(_xMove, _yMove);
                _thunder++;

                if (_thunder >= 100)
                {
                    _thunder = 0;
                    _rb.velocity = new Vector2(_xMove, _yMove);
                    _yMove *= -1;
                }
            }
            else
            {
                _rb.velocity = new Vector2(0, 0);
                _switch = 2;
                _xMove *= -1;
            }
        }

        if (_switch == 2)
        {
            Debug.Log("dayone");
            if (transform.position.x >= -7.5f)
            {
                _rb.velocity = new Vector2(_xMove, _yMove);
                _thunder++;

                if (_thunder >= 100)
                {
                    _thunder = 0;
                    _rb.velocity = new Vector2(_xMove, _yMove);
                    _yMove *= -1;
                }
            }
            else
            {
                _rb.velocity = new Vector2(0, 0);
                _switch = 1;
                _xMove *= -1;
            }
        }
    }
}
