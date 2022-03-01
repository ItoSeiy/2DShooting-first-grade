using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRush : BossMoveAction
{
    /// <summary>プレイヤーのオブジェクト</summary>
    GameObject _player;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>タイマー</summary>
    float _time = 0f;
    /// <summary>プレイヤーのタグ</summary>
    [SerializeField,Header("プレイヤーのタグ")] private string _playerTag = null;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 3f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = -3f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("降りた後の停止時間")] float _stopTime = 2f;
    /// <summary>上に滞在する時間、追尾時間</summary>
    [SerializeField,Header("追尾時間(上に滞在している時間)")] float _stayingTime = 4f;    
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1/60f;
    /// <summary>修正値</summary>
    const float PLAYER_POSTION_OFFSET = 0.5f;
    /// <summary>方向なし</summary>
    const float NO_DIR = 0f;
    /// <summary>中央位置</summary>
    const float MIDDLE_POS = 0f;

    public override void Enter(BossController contlloer)
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Rush(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        contlloer.Rb.velocity = _dir * contlloer.Speed;
        _time += Time.deltaTime;

        //右に移動したら右を向く
        if (contlloer.Rb.velocity.x > MIDDLE_POS)
        {
            contlloer.Sprite.flipX = true;
        }
        //左に移動したら左を向く
        else if (contlloer.Rb.velocity.x < MIDDLE_POS)
        {
            contlloer.Sprite.flipX = false;
        }
    }

    public override void Exit(BossController contlloer)
    {
        
    }

    /// <summary>
    /// 一定時間プレイヤーをロックオンしたあと真下にサガる。その後上に上がる。
    /// </summary>
    IEnumerator Rush(BossController controller)        
    {
        _time = 0;

        //x座標だけプレイヤーの近くに移動する
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            //プレイヤーが右にいたら
            if (_player.transform.position.x > controller.transform.position.x + PLAYER_POSTION_OFFSET || _player.transform.position.x < controller.transform.position.x - PLAYER_POSTION_OFFSET)
            {
                Debug.Log("right");
                _dir = new Vector2(_player.transform.position.x - controller.transform.position.x, NO_DIR).normalized;
            }
            else//プレイヤーのx座標が近かったら
            {
                Debug.Log("stop");
                _dir = new Vector2(_player.transform.position.x - controller.transform.position.x, NO_DIR);
            }
            //限界に達したら
            if ( _time >= _stayingTime)
            {
                break;
            }
        }

        while (true)//サガる
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            _dir = Vector2.down;//サガる

            if (controller.transform.position.y <= _lowerLimit)//サガったら
            {
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.up;//上がる
                break;
            }
        }       
        
        while (true)//一定の場所まで上がる
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            if (_upperLimit <= controller.transform.position.y)//一定の場所まできたら
            {
                Debug.Log("ラスト");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }            
        }
        yield break;
    }

}
