using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletFollow : BulletBese
{
    GameObject _enemy;
    float _timer;
    Vector2 _oldDir = Vector2.up;
    [SerializeField, Header("追従する時間")]
    float _followTime = 2f;

    protected override void OnEnable()
    {
        _timer = 0;
        _enemy = GameObject.FindWithTag(OpponenTag);
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;
        if (_timer >= _followTime) return;

        if(_enemy)
        {
            Vector2 dir = _enemy.transform.position - this.transform.position;
            Rb.velocity = dir.normalized * Speed;
            _oldDir = dir;
        }
        else if(!_enemy)
        {
            Rb.velocity = _oldDir.normalized * Speed;
        }
    }
}
