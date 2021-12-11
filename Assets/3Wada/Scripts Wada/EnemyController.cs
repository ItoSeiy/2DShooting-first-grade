using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBese
{
    [SerializeField] GameObject[] _muzzle;

    protected override void Attack()
    {
      
    }

    protected override void OnGetDamage()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
