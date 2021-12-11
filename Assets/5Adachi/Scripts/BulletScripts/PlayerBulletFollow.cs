using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletFollow : BulletBese
{
    [SerializeField] string _enemyTag = null;
    GameObject _enemy;
    Rigidbody2D _rb;
    float _timer;
    [SerializeField, Header("追従する時間")] float _followTime = 2f;

    private void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag(_enemyTag);
        if(_enemy)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;
        if (_timer >= _followTime) return;

        Vector2 _dir = _enemy.transform.position - this.transform.position;
        _dir = _dir.normalized * Speed;
        _rb.velocity = _dir;
        
    }
}
