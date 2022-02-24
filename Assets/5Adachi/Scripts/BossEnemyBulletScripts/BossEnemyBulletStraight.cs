using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletStraight : BulletBese
{
    protected override void BulletMove()
    {
        Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
    }
}
