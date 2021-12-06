using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player1 : PlayerBase
{
    public override async void PlayerAttack()
    {
        //base.PlayerAttack();
        GameObject gob = Instantiate(_bullet, _muzzle);
        await Task.Delay(_attackDelay);
        _bulletStop = false;
    }

    public override async void PlayerSuperAttack()
    {
        //base.PlayerSuperAttack();
        GameObject gob = Instantiate(_bullet, _muzzle);
        await Task.Delay(_superAttackDelay);
        _bulletStop = false;
    }
}
