using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : BulletBese
{
    [SerializeField] string _enemyTag;
    [SerializeField] string _enemyBulletTag;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _speed = 10f;
    [SerializeField] GameObject _bombChildBullet = null;
    [SerializeField] Transform[] _muzzle = null;
    Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _direction.normalized * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _enemyTag)
        {
            for (int i = 0; i < _muzzle.Length; i++)
            {
                Instantiate(_bombChildBullet, _muzzle[i]);
            }

            //var enemy = GameObject.FindGameObjectsWithTag(_enemyTag);
            //var bullet = GameObject.FindGameObjectsWithTag(_enemyBulletTag);

            //foreach (var e in enemy)
            //{
            //    Destroy(e);
            //}

            //foreach (var b in bullet)
            //{
            //    Destroy(b);
            //}
            //Destroy(this.gameObject);
        }
    }
}
