using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerBase
{
    public override void Bom()
    {
        //ここにボムを使う処理を書く
        PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Bomb01);
        base.Bom();
    }

    public override void InvincibleMode()
    {
        base.InvincibleMode();
    }
}
