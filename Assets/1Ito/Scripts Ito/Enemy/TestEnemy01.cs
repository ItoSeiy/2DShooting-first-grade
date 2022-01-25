using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy01 : EnemyBese
{
    [SerializeField] Vector2 _direction = default;
    protected override void Attack()
    {
        //Debug.Log(gameObject + "Attack");
    }

    protected override void OnGetDamage()
    {
        //Debug.Log("GetDamage");
    }

    protected override void Update()
    {
        Rb.velocity = _direction;
        base.Update();
    }
}
