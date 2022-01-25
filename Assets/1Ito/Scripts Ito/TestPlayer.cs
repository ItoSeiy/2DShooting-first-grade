using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public void Use(int i)
    {
        PlayerBulletPool.Instance.UseBullet(transform.position, (PoolObjectType)i);
    }
}
