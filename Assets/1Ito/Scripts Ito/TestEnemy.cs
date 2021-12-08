using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBese
{
    public override void AddDamage(float damage)
    {

        base.AddDamage(damage);
    }

    protected override void OnGetDamage()
    {
        base.OnGetDamage();
    }
}
