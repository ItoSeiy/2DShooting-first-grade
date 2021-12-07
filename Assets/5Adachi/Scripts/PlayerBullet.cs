using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBullet : BulletBese
{
    [SerializeField] string _enemyTag;
    [SerializeField] float _bulletSpeed = 10f;
    [SerializeField] Vector2 _direction = Vector2.up;
    Rigidbody2D _rb;
    public override void BulletMovement()
    {
        
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Vector2 v = _direction.normalized * _bulletSpeed;
        _rb.velocity = v;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _enemyTag)
        {
            var enemy = GameObject.FindGameObjectsWithTag(_enemyTag);

            foreach(var e in enemy)
            {
                Destroy(e);
            }
        }
    } 
        
}
