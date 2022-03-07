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
    bool _isFirstTime = true;


    protected override void OnEnable()
    {
        _isFirstTime = true;
        _timer = 0;
        base.OnEnable();
        FindEnemy();
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;

        if (_timer >= _followFnishTime)
        {
            Rb.velocity = gameObject.transform.rotation * new Vector2(0, Speed);
            return;
        }

        if (_enemy && _timer >= _followStartTime)
        {
            Vector2 dir = _enemy.transform.position - this.transform.position;
            Rb.velocity = dir.normalized * Speed;
        }
        else
        {
            if(_isFirstTime)
            {
                FindEnemy();
                _isFirstTime = false;
                BulletMove();
            }
            Rb.velocity = gameObject.transform.rotation * new Vector2(0, Speed);
        }
    }

    private void FindEnemy()
    {
        _enemy = GameObject.FindWithTag(OpponenTag);
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
