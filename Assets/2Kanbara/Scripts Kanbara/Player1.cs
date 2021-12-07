using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player1 : PlayerBase
{
    public override async void PlayerAttack()
    {
        int i = default;
        GetPower();
        if (_power < 50)
        {
            i = 0;
        }
        else if (50 < _power && _power < 100)
        {
            i = 1;
        }
        else if (100 < _power)
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
        GetPower();
        if (_power < 50)
        {
            i = 0;
        }
        else if (50 < _power && _power < 100)
        {
            i = 1;
        }
        else if (100 < _power)
        {
            i = 2;
        }
        GameObject go = Instantiate(_superBullet[i], _muzzle);
        await Task.Delay(_superAttackDelay);
        _isBulletStop = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
        //if (collision.gameObject.tag == "Power")
        //{
        //    //powerを取ったらpowerが増える処理を書く
        //}
        //if (collision.gameObject.tag == "Point")
        //{
        //    //pointを取ったらpointが増える処理を書く
        //}
        //if (collision.gameObject.tag == "1up")
        //{
        //    //1upを取ったら1upが増える処理を書く
        //}
    //}

    public override void Bom()
    {
        //base.Bom();
        //ここにボムを使う処理を書く
    }

    //private int _power = default;
    //public int GetPower()
    //{
    //    return _power = FindObjectOfType<GameManager>().GetComponent<GameManager>().Power;
    //}
}
