using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletReboundFollow : BulletBese
{
    /// <summary>BossのGameObject</summary>
    GameObject _bossEnemy;
    /// <summary>タイマー</summary>
    float _timer;
    /// <summary>Playerがいた方向</summary>
    Vector2 _oldDir = Vector2.down;
    /// <summary>一定時間遅れたらプレイヤーに方向を変える(秒)</summary>
    [SerializeField, Header("一定時間遅れたらプレイヤーがいる方向に変える(秒)")] float _delayChangeDirTime = 2f;
    /// <summary>時間の修正値</summary>
    const float DELAY_CHANGE_DIR_TIME_OFFSET = 0.1f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>上限</summary>
    [SerializeField,Header("上限")] float _upperLimit = 4f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _downLimit = -4f;
    /// <summary>横限の条件式</summary>
    bool _horizontalLimit;
    /// <summary></summary>
    bool _verticalLimit;
    bool _rebound = true;
    protected override void OnEnable()
    {
        _bossEnemy = GameObject.FindWithTag(PlayerTag);//BossのTagをとってくる
        base.OnEnable();
    }

    protected override void BulletMove()
    {
        //横限の条件式
        _horizontalLimit = transform.position.x >= _rightLimit || transform.position.x <= _leftLimit;
        //縦限の条件式
        _verticalLimit = transform.position.y >= _upperLimit || transform.position.y <= _downLimit;
        //一回も使ってなかったら
        if ((_horizontalLimit || _verticalLimit) && _rebound)
        {
            //プレイヤーの方向を計算
            Vector2 dir = _bossEnemy.transform.position - transform.position;
            //速度が変わらないようにし、スピードを加える
            dir = dir.normalized * Speed;
            //方向を変える
            Rb.velocity = dir;
            //方向を保存
            _oldDir = dir;
        }
        else if(!_rebound)
        {
            Rb.velocity = _oldDir.normalized * Speed;
        }
        else
        {
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);//マズルの向きに合わせて移動
        }
    }
}
