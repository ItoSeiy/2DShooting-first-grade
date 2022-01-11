using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletFollow : BulletBese
{
    GameObject _enemy;
    float _timer;
    [SerializeField, Header("追従する時間")] float _followTime = 2f;

    protected override void OnEnable()
    {
        _timer = 0;
        _enemy = GameObject.FindWithTag(EnemyTag);
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;
        if (_timer >= _followTime) return;

        if(_enemy)
        {
            Vector2 dir = _enemy.transform.position - this.transform.position;
            dir = dir.normalized * Speed;
            Rb.velocity = dir;   
        }
        else if(!_enemy)
        {
            Rb.velocity = Vector2.up.normalized * Speed;
        }
    }
}
