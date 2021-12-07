using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBese

{
    [SerializeField] Transform _muzzle;
    [SerializeField] GameObject _bullet;
    [SerializeField] float m_fireInterval = 1f;

    public override void Attack()
    {
        // 各 muzzle から弾を発射する
        Instantiate(_bullet,_muzzle);
    }
}
