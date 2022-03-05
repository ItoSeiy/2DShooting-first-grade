using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletDelayFollow : BulletBese
{
    /// <summary>ターゲットのGameObject</summary>
    GameObject _opponen;
    /// <summary>タイマー</summary>
    float _timer;
    /// <summary>Playerがいた方向</summary>
    Vector2 _oldDir = Vector2.down;
    /// <summary>一定時間遅れたらプレイヤーに方向を変える(秒)</summary>
    [SerializeField, Header("一定時間遅れたらプレイヤーがいる方向に変える(秒)")] float _delayChangeDirTime = 2f;
    /// <summary>時間の修正値</summary>
    const float DELAY_CHANGE_DIR_TIME_OFFSET = 0.1f;

    protected override void OnEnable()
    {
        _timer = 0;//タイマーをリセット
        _opponen = GameObject.FindWithTag(OpponenTag);//PlayerのTagをとってくる
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;//タイマー

        //時間になったら
        if (_timer >= _delayChangeDirTime + DELAY_CHANGE_DIR_TIME_OFFSET) return;

        if(_timer < _delayChangeDirTime && _opponen)//時間になるまでは
        {
            //マズルの向きに合わせた方向に移動
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        }

        else if (_timer >= _delayChangeDirTime && _opponen)//もし時間になったら
        {
            //回転したい方向を計算
            Vector2 dir = transform.position - _opponen.transform.position;
            //回転したい方向に回転
            transform.rotation = Quaternion.FromToRotation(Vector3.down, dir);
            //回転した方向に移動
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        }
        else//時間外労働になったら
        {
            //プレイヤーがいた方向か下方向に移動
            Rb.velocity = _oldDir.normalized * Speed;
        }
    }
}
