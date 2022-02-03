using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerBase
{
    public override void Bom()
    {
        //ここにボムを使う処理を書く
        ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01Bomb01);
        Debug.Log("ボムドーン！");
        base.Bom();
    }

    public override void InvincibleMode()
    {
        base.InvincibleMode();
    }

    public override void PlayerAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01Power1);
                break;
            case _level2:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01Power2);
                break;
            case _level3:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01Power3);
                break;
        }
        Play(_playerBulletAudio);
        base.PlayerAttack();
    }

    public override void PlayerSuperAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01SuperPower1);
                break;
            case _level2:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01SuperPower2);
                break;
            case _level3:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01SuperPower3);
                break;
        }
        Play(_playerSuperBulletAudio);
        base.PlayerSuperAttack();
    }

    public override void PlayerChargeAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01ChargePower1);
                break;
            case _level2:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01ChargePower2);
                break;
            case _level3:
                ObjectPool.Instance.UseBullet(_muzzle.position, PoolObjectType.Player01ChargePower3);
                break;
        }
        _audioSource.Stop();
        Play(_playerChargeShotBulletAudio);
        base.PlayerChargeAttack();
    }
}
