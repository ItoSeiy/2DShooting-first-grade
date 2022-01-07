using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public void Use()
    {
        PlayerBulletPool.Instance.UseBullet(this.transform.position, PlayerBulletPool.BulletType.Player01Power2);
    }
}
