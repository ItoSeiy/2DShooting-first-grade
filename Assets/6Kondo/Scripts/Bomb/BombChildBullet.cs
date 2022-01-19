using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChildBullet : BulletBese
{
    [SerializeField, Header("EnemyのBulletのタグ")] string _enemyBulletTag = "Bullet";
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _enemyBulletTag)
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == OpponenTag)
        {
            base.BulletAttack(collision);
            gameObject.SetActive(false);
        }
        else if(collision.tag == GameZoneTag)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void BulletMove()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        Rb.velocity = velocity;
    }
}
