using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class Player02Bomb : BulletBese
{
    [SerializeField] float _childBulletDelay = 1f;
    [SerializeField] Transform[] _muzzle = null;
    protected override void OnEnable()
    {
        StartCoroutine(UseBombChildBullet());
    }

    IEnumerator UseBombChildBullet()
    {
        for (int i = 0; i < _muzzle.Length; i++)
        {
            var bombChild = PlayerBulletPool.Instance.UseBullet(_muzzle[i].position, PoolObjectType.Player02BombChild);
            bombChild.transform.rotation = _muzzle[i].rotation;
            yield return new WaitForSeconds(_childBulletDelay);
        }
        gameObject.SetActive(false);
    }
}
