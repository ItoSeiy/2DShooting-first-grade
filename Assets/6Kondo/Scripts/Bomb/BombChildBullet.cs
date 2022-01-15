﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombChildBullet : BulletBese
{
    [SerializeField] string _bulletTag = "Bullet";
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _bulletTag)
        {
            base.BulletAttack(collision);
        }
        else if (collision.tag == EnemyTag)
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
