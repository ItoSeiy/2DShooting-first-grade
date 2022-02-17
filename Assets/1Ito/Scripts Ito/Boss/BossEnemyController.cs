using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyBase
{
    [SerializeField]
    Transform[] _muzzles = null;



    protected override void Attack()
    {
        //ObjectPool.Instance(_);
    }

    protected override void OnGetDamage()
    {
        throw new System.NotImplementedException();
    }


}
