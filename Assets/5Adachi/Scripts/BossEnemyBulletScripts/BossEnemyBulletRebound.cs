using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletRebound : BulletBese
{
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    bool _rebound = true;
    protected override void BulletMove()
    {
        //弾が画面端まできたら１回だけ跳ね返る
        if ((transform.position.x <= _leftLimit || transform.position.x >= _rightLimit) && _rebound)
        {
            Vector3 localAngle = transform.localEulerAngles;// ローカル座標を基準に取得
            localAngle.z = -localAngle.z;// 角度を設定
            transform.localEulerAngles = localAngle;//回転する
            _rebound = false;//２回目はリバウンドできなくなる
        }
        else
        {
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);//マズルの向きに合わせて移動
        }
    }
}
