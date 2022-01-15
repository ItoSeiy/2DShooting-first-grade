using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player1 : PlayerBase
{
    public override void Bom()
    {
        //ここにボムを使う処理を書く
        PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.BombChild);
        base.Bom();
    }

    public override void InvincibleMode()
    {
        base.InvincibleMode();
    }
}
