using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChildBullet : BulletBese
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == GameZoneTag)
        {
            return;
        }
        if (collision.tag == EnemyTag)
        {
            base.BulletAttack(collision);
            gameObject.SetActive(false);
        }
    }

    protected override void BulletMove()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(Speed, 0, 0);
        gameObject.transform.position += velocity * Time.deltaTime;
    }
}
