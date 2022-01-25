using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    Rigidbody2D _rb;
    float _time;
    [SerializeField,Header("スピード")] float _speed;
    int sign;
    bool _switch = false;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _time += Time.deltaTime;
        if (transform.position.x >= 0)
        {
            Thunder(-1);
        }
        else
        {
            Thunder(1);
        }
    }

    IEnumerator Thunder()
    {
        if (!_switch)
        {
            _rb.velocity = new Vector2(2 * sign, 1);
            _switch = true;
        }
        else
        {
            _rb.velocity = new Vector2(-1 * sign, -1);
            _switch = false;
        }
    }
}
