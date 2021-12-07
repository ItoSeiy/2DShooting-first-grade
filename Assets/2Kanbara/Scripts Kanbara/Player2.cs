using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player2 : PlayerBase
{
    public override async void PlayerAttack()
    {
        int i = default;
        if (_playerPower < 50)
        {
            i = 0;
        }
        else if (50 < _playerPower && _playerPower < 100)
        {
            i = 1;
        }
        else if (100 < _playerPower)
        {
            i = 2;
        }
        GameObject go = Instantiate(_bullet[i], _muzzle);
        await Task.Delay(_attackDelay);
        _isBulletStop = false;
    }

    public override async void PlayerSuperAttack()
    {
        int i = default;
        if (_playerPower < 50)
        {
            i = 0;
        }
        else if (50 < _playerPower && _playerPower < 100)
        {
            i = 1;
        }
        else if (100 < _playerPower)
        {
            i = 2;
        }
        GameObject go = Instantiate(_superBullet[i], _muzzle);
        await Task.Delay(_superAttackDelay);
        _isBulletStop = false;
    }

    public override async void Bom()
    {
        //base.Bom();
        //ここにボムを使う処理を書く
        await Task.Delay(_bomCoolTime);
        _isBom = false;
    }
}
