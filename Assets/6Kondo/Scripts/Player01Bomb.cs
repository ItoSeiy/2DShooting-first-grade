using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Bomb : BulletBese
{
    //[SerializeField] string _enemyTag;
    [SerializeField] string _enemyBulletTag;
    //[SerializeField] Vector2 _direction = Vector2.up;
    
    [SerializeField] GameObject _bombChildBullet = null;
    [SerializeField] Transform[] _muzzle = null;
    //Rigidbody2D _rb;
    public Explosion m_explosionPrefab;

    //void OnBecameInvisible()
    //{
    //    Destroy(this.gameObject);
    //}

    protected override void BulletMove()
    {
        base.BulletMove();
    }
    //private void Start()
    //{
    //    _rb = GetComponent<Rigidbody2D>();_speed;
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == EnemyTag)
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
