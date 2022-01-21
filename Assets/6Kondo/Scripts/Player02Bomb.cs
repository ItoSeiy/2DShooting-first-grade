using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class Player02Bomb : BulletBese
{
    [SerializeField] Explosion _explosionPrefab = null;
    [SerializeField] float _childBulletDelay = 1f;
    [SerializeField] Transform[] _muzzle = null;
    SpriteRenderer _sprite = null;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.BulletAttack(collision);
        StartCoroutine(UseBombChildBullet(collision));
    }

    IEnumerator UseBombChildBullet(Collider2D collision)
    {
        if (collision.tag == OpponenTag)
        {
            Rb.velocity = Vector2.zero;
            for (int i = 0; i < _muzzle.Length; i++)
            {
                var bombChild = PlayerBulletPool.Instance.UseBullet(_muzzle[i].position, PlayerBulletType.BombChild);
                bombChild.transform.rotation = _muzzle[i].rotation;
                yield return new WaitForSeconds(_childBulletDelay);
            }
            gameObject.SetActive(false);
        }
        else if (collision.tag == GameZoneTag)
        {
            gameObject.SetActive(false);
        }
    }
}
