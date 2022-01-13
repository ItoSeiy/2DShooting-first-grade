using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public void Use(int i)
    {
        EnemyBulletPool.Instance.UseBullet(transform.position, (EnemyBulletType)i);
    }
}
