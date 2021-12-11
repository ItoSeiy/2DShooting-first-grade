using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBese
{
    [SerializeField] Transform[] _muzzle = null;
    [SerializeField] GameObject _bullet = null;
    [SerializeField] AudioSource _onDestroyAudio = null;

    protected override void Attack()
    {
        for (int i = 0;i < _muzzle.Length; i++)
        {
            Instantiate(_bullet, _muzzle[i]);
        }
    }

    protected override void OnGetDamage()
    {
        if (EnemyHp == 0) 
        {
            AudioSource.PlayClipAtPoint(_onDestroyAudio.clip,transform.position);
            
        }
    }    
}
