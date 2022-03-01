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
    GameObject _enemy;

    protected override void OnEnable()
    {
        _timer = 0;
        _enemy = GameObject.FindWithTag(OpponenTag);
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;

        if (_timer >= _followFnishTime)
        {
            Rb.velocity = _oldDir;
            return;
        }

        if (_enemy && _timer >= _followStartTime)
        {
            Vector2 dir = _enemy.transform.position - this.transform.position;
            dir = dir.normalized * Speed;
            Rb.velocity = dir;
            _oldDir = dir;
        }
        else
        {
            Vector2 velocity = gameObject.transform.rotation * new Vector2(0, Speed);
            Rb.velocity = velocity;
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
