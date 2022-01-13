using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy01 : EnemyBese
{
    protected override void Attack()
    {
        //Debug.Log(gameObject + "Attack");
    }

    protected override void OnGetDamage()
    {
        //Debug.Log(gameObject + "GetDamage");
    }
}
