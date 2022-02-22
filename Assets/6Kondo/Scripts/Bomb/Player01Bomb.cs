using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class Player01Bomb : BulletBese
{
    [SerializeField] Explosion _explosionPrefab　= null;
    [SerializeField] float _childBulletDelay = 1f;
    [SerializeField] Transform[] _muzzle = null;
    SpriteRenderer _sprite = null;

    [SerializeField] AudioSource _audioSource;
    [SerializeField, Header("BombChildが発射された時の音")] AudioClip _bombChildAudio;

    protected override void Awake()
    {
        base.Awake();
        _sprite = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        var playerAudio = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
        playerAudio.Stop();
        StartCoroutine(UseBombChildBullet(collision));
    }


    IEnumerator UseBombChildBullet(Collider2D collision)
    {
        if (collision.tag == OpponenTag)
        {
            base.BulletAttack(collision);

            var explosionPrefab = Instantiate(_explosionPrefab);
            explosionPrefab.transform.position = collision.transform.position;

            _sprite.enabled = false;
            Rb.velocity = Vector2.zero;
            for (int i = 0; i < _muzzle.Length; i++)
            {
                var bombChild = ObjectPool.Instance.UseBullet(_muzzle[i].position, PoolObjectType.Player01BombChild);
                bombChild.transform.rotation = _muzzle[i].rotation;
                yield return new WaitForSeconds(_childBulletDelay);
            }

            _sprite.enabled = true;
            gameObject.SetActive(false);
        }
        else if(collision.tag == GameZoneTag)
        {
            gameObject.SetActive(false);
        }
    }
}
