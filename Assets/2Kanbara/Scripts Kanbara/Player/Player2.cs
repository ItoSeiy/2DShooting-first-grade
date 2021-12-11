using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player2 : PlayerBase
{
    [SerializeField, Header("この数値未満ならレベル１")] int _level1 = default;
    [SerializeField, Header("この数値以上ならレベル３")] int _level3 = default;

    public override async void PlayerAttack()
    {
        int _level = default;
        if (_playerPower < _level1)//レベル1のとき
        {
            _level = 0;
        }
        else if (_level1 <= _playerPower && _playerPower < _level3)//レベル2のとき
        {
            _level = 1;
        }
        else if (_level3 <= _playerPower)//レベル3のとき
        {
            _level = 2;
        }
        GameObject go = Instantiate(_bullet[_level], _muzzle);
        await Task.Delay(_attackDelay);
        _isBulletStop = false;
    }

    public override async void PlayerSuperAttack()
    {
        int _level = default;
        if (_playerPower < _level1)//レベル1のとき
        {
            _level = 0;
        }
        else if (_level1 <= _playerPower && _playerPower < _level3)//レベル2のとき
        {
            _level = 1;
        }
        else if (_level3 <= _playerPower)//レベル3のとき
        {
            _level = 2;
        }
        GameObject go = Instantiate(_superBullet[_level], _muzzle);
        await Task.Delay(_superAttackDelay);
        _isBulletStop = false;
    }

    public override async void Bom()
    {
        base.Bom();
        //ここにボムを使う処理を書く
        await Task.Delay(_bomCoolTime);
        _isBom = false;
    }

    public override void InvincibleMode()
    {
        base.InvincibleMode();
    }
}
