using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy01 : EnemyBase
{
    [SerializeField] Vector2 _direction = default;
    protected override void Attack()
    {

    }

    protected override void OnGetDamage()
    {

    }

    protected override void Update()
    {
        Rb.velocity = _direction;
        base.Update();
    }
}
