using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialAttack : EnemyBese
{
    /// <summary>バレットのプレハブ</summary>
    [SerializeField,Header("Bulletのプレハブ")] List<GameObject> _enemyBulletPrefab = new List<GameObject>();
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    void Start()
    {
        if (_muzzles == null || _muzzles.Length == 0)
        {
            _muzzles = new Transform[1] { this.transform };
        }
    }
    protected override void Update()
    {
        base.Update();
    }
 
    protected override void Attack()
    {
        //throw new System.NotImplementedException();

        foreach (Transform t in _muzzles)
        {
            Instantiate(_enemyBulletPrefab[0], t.position, Quaternion.identity);
        }
    }

    protected override void OnGetDamage()
    {
        throw new System.NotImplementedException();
    }
}
