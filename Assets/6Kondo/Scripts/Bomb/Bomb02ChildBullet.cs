using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb02ChildBullet : BulletBese
{
    [SerializeField, Header("’Ç]‚µŽn‚ß‚éŽžŠÔ")] 
    float _followStartTime = 2f;

    [SerializeField, Header("’Ç]‚ðI‚í‚éŽžŠÔ")]
    float _followFnishTime = 5f;

    [SerializeField, Header("Enemy‚ÌBullet‚Ìƒ^ƒO")]
    string _enemyBulletTag = "EnemyBullet";

    float _timer = 0;
    Vector2 _oldDir;
    GameObject[] _enemies;
    bool _isFirstTime = true;


    protected override void OnEnable()
    {
        _isFirstTime = true;
        _timer = 0;
        base.OnEnable();
        _enemies = GameObject.FindGameObjectsWithTag(OpponenTag);
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;

        if (_timer >= _followFnishTime)
        {
            Rb.velocity = gameObject.transform.rotation * new Vector2(0, Speed);
            return;
        }

        foreach(var enemy in _enemies)
        {
            if (enemy && _timer >= _followStartTime)
            {
                Vector2 dir = enemy.transform.position - this.transform.position;
                Rb.velocity = dir.normalized * Speed;
                break;
            }
            else
            {
                Rb.velocity = gameObject.transform.rotation * new Vector2(0, Speed);
                break;
            }
        }
    }

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
        else if (collision.tag == GameZoneTag)
        {
            gameObject.SetActive(false);
        }
    }
}
