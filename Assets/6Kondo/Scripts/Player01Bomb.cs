using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class Player01Bomb : BulletBese
{
    [SerializeField] string _enemyBulletTag;
    [SerializeField] Transform[] _muzzle = null;
    [SerializeField] Explosion _explosionPrefab　= null;
    [SerializeField] float _childBulletDelay = 1f;
    float _timer;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == EnemyTag)
        {
            var explosionPrefab = Instantiate(_explosionPrefab);
            explosionPrefab.transform.position = collision.transform.position;
            UseBombChildBullet();
        }
        base.OnTriggerEnter2D(collision);
    }

    async void UseBombChildBullet()
    {
        for (int i = 0; i < _muzzle.Length; i++)
        {
            var bombChild = PlayerBulletPool.Instance.UseBullet(_muzzle[i].position, PlayerBulletType.BombChild);
            bombChild.transform.rotation = _muzzle[i].rotation;

        }
    }


}
