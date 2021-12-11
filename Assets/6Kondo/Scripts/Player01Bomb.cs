using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Bomb : BulletBese
{
    [SerializeField] string _enemyTag;
    [SerializeField] string _enemyBulletTag;
    [SerializeField] Vector2 _direction = Vector2.up;
    [SerializeField] float _speed = 10f;
    [SerializeField] GameObject _bombChildBullet = null;
    [SerializeField] Transform[] _muzzle = null;
    Rigidbody2D _rb;
    public Explosion m_explosionPrefab;
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _direction.normalized * _speed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
            

        if (collision.tag == _enemyTag)
        {
            Instantiate(m_explosionPrefab, collision.transform);
            for (int i = 0; i < _muzzle.Length; i++)
            {
                Instantiate(_bombChildBullet, _muzzle[i].position,_muzzle[i].rotation);
            }
            Destroy(this.gameObject);
        }
    }
}
