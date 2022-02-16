using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : BulletBese
{
    protected override void BulletMove()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        Rb.velocity = velocity;
    }
}
