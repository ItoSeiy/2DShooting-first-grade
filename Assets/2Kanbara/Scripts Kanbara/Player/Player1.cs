using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerBase
{
    public override void Bom()
    {
        //ここにボムを使う処理を書く
        PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Bomb01);
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
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power1);
                break;
            case _level2:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power2);
                break;
            case _level3:
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power3);
                break;
        }
        
        base.PlayerAttack();
    }

    public override void PlayerSuperAttack()
    {
        switch (GameManager.Instance.PlayerLevel)
        {
            case _level1:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01SuperPower1);
                break;
            case _level2:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01SuperPower2);
                break;
            case _level3:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01SuperPower3);
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
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01ChargePower1);
                break;
            case _level2:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01ChargePower2);
                break;
            case _level3:
                PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01ChargePower3);
                break;
        }
        _audioSource.PlayOneShot(_chargeBulletShootingAudio, _musicVolume);
        base.PlayerChargeAttack();
    }
}
