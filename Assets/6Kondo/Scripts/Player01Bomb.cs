using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01Bomb : BulletBese
{
    [SerializeField] string _enemyBulletTag;
    [SerializeField] Transform[] _muzzle = null;
    [SerializeField] Explosion _explosionPrefab　= null;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == EnemyTag)
        {
            var explosionPrefab = Instantiate(_explosionPrefab);
            explosionPrefab.transform.position = collision.transform.position;

            for (int i = 0; i < _muzzle.Length; i++)
            {
                var bombChild = PlayerBulletPool.Instance.UseBullet(_muzzle[i].position, PlayerBulletType.BombChild);
                bombChild.transform.rotation = _muzzle[i].rotation;

            }
        }
    }
}
