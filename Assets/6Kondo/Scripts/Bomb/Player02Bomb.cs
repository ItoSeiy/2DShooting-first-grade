using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class Player02Bomb : BulletBese
{
    [SerializeField] float _childBulletDelay = 1f;
    [SerializeField] Transform[] _muzzle = null;
    AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void OnEnable()
    {
        StartCoroutine(UseBombChildBullet());
    }

    IEnumerator UseBombChildBullet()
    {
        for (int i = 0; i < _muzzle.Length; i++)
        {
            var bombChild = ObjectPool.Instance.UseObject(_muzzle[i].position, PoolObjectType.Player02BombChild);
            bombChild.transform.rotation = _muzzle[i].rotation;
            _audioSource.Play();
            yield return new WaitForSeconds(_childBulletDelay);
        }
        gameObject.SetActive(false);
    }
}
