﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerBase
{
    public override void Bom()
    {
        //ここにボムを使う処理を書く
        PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02Bomb01);
        Debug.Log("ボムドーン！！");
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
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02Power1);
                break;
            case _level2:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02Power2);
                break;
            case _level3:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02Power3);
                break;
        }
        _audioSource.PlayOneShot(_bulletShootingAudio, _musicVolume);
        base.PlayerAttack();
    }

    public override void PlayerSuperAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02SuperPower1);
                break;
            case _level2:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02SuperPower2);
                break;
            case _level3:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02SuperPower3);
                break;
        }
        _audioSource.PlayOneShot(_superBulletShootingAudio, _musicVolume);
        base.PlayerSuperAttack();
    }

    public override void PlayerChargeAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02ChargePower1);
                break;
            case _level2:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02ChargePower2);
                break;
            case _level3:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player02ChargePower3);
                break;
        }
        _audioSource.PlayOneShot(_chargeBulletShootingAudio, _musicVolume);
        base.PlayerChargeAttack();
    }
}
