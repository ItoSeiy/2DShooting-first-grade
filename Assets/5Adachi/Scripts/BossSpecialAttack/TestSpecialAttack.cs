using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialAttack : EnemyBese
{
    /// <summary>バレットのプレハブ</summary>
    [SerializeField] List<GameObject> _enemyBulletPrefab = new List<GameObject>();
    /// <summary>バレットを発射する場所</summary>
    [SerializeField] List<Transform> m_muzzles =new List<Transform>();
    void Start()
    {
        
    }
    protected override void Update()
    {
        base.Update();
    }

   
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }


}
