using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    float _damage = 0;
    float _time = 0;
    bool _firstTime = false;
    bool _firstDamage = false;

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time > 30 && !_firstTime)
        {
            Debug.LogWarning("I—¹");
            _firstTime = true;
        }
        if(_damage > 100000 && !_firstDamage)
        {
            Debug.LogWarning("100000Damage" + _time);
            _firstDamage = true;
        }
    }
    protected override void OnGetDamage()
    {
        Debug.LogError(_damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _damage += collision.GetComponent<BulletBese>().Damage;
        Debug.Log(_damage);
    }
}
