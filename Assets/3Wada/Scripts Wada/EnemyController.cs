using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBese

{
    [SerializeField] Transform[] _muzzle;
    [SerializeField] GameObject _bullet;
    

    protected override void Attack()
    {
        // 各 muzzle から弾を発射する
        for(int i = 0 ; i < _muzzle.Length; i++)
        {
            Instantiate(_bullet,_muzzle[i]);
        }
    }
}
