using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : BulletBese
{
    [SerializeField] string _enemyTag;
    [SerializeField] string _enemyBulletTag;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _speed = 10f;
    Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector3 v = _direction.normalized * _speed;
        _rb.velocity = v;
    }
    public override void BulletMovement()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _enemyTag)
        {
            var enemy = GameObject.FindGameObjectsWithTag(_enemyTag);
            var bullet = GameObject.FindGameObjectsWithTag(_enemyBulletTag);

            foreach(var e in enemy)
            {
                Destroy(e);
            }

            foreach(var b in bullet)
            {
                Destroy(b);
            }
            Destroy(this.gameObject);
        }
    }
}
