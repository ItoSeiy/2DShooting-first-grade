using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player1 : PlayerBase
{
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
