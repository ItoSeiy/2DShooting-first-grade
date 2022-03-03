using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped : BossMoveAction
{
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 3.5f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = -3f;
    /// <summary>最短移動時間</summary>
    [SerializeField, Header("最短移動時間")] float _shortMoveTime = 1f;
    /// <summary>最長移動時間</summary>
    [SerializeField, Header("最長移動時間")] float _longMoveTime = 3f;
    /// <summary>停止時間</summary>
    [SerializeField,Header("停止時間")] float _stopTime = 1f;
    /// <summary>中央位置</summary>
    const float MIDDLE_POS = 0;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1/60f;
    /// <summary>判定を遅らす</summary>
    const float DELAY_TIME = 1f;
    /// <summary>方向なし</summary>
    const float NO_DIR = 0f;


    public override void Enter(BossController contlloer)
    {
        StartCoroutine(UShaped(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        contlloer.Rb.velocity = _dir.normalized * contlloer.Speed;//方向にスピードを加える

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
        StopAllCoroutines();
    }

    /// <summary>
    /// U字型のような移動をする
    /// </summary>
    public IEnumerator UShaped(BossController controller)
    {
        _dir = new Vector2(-controller.transform.position.x, NO_DIR);

        //ボスのポジションXが0だったら棒立ちして詰むので
        if (controller.transform.position.x == 0f)
        {
            _dir = Vector2.right;//右に移動
        }

        while(true)//端に着くまで横に動く
        {           
            //反対側に着いたら
            if ((controller.transform.position.x <= _leftLimit) || (controller.transform.position.x >= _rightLimit))
            {
                Debug.Log("c");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.down;//下がる
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
        }

        while (true)//反対側に着くまで移動する
        {           
            if(controller.transform.position.y <= _lowerLimit)
            {

                Debug.Log("f");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = new Vector2(-controller.transform.position.x, NO_DIR);//画面下端にいたら今いる場所の反対側に横移動
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
        }

        yield return new WaitForSeconds(DELAY_TIME);//判定を遅らす

        while (true)//反対側に着くまで横移動する
        {          
            //反対側についたら上に行く
            if (controller.transform.position.x >= _rightLimit || controller.transform.position.x <= _leftLimit)
            {
                Debug.Log("g");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.up;//上に上がる
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
        }

        while (true)//ある程度上にいくまで移動する
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);

            if (controller.transform.position.y >= _upperLimit)//ある程度上にいったら
            {
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                Debug.Log("h");

                if (controller.transform.position.x < MIDDLE_POS)//左にいたら
                {                   
                    _dir = Vector2.right;//右に行く
                    Debug.Log("a");
                    break;
                }
                else//右にいたら
                {    
                    _dir = Vector2.left;//左に行く
                    Debug.Log("b");
                    break;
                }
            }
        }
        
        yield return new WaitForSeconds(Random.Range(_shortMoveTime, _longMoveTime)); //移動時間(ランダム)    
        _dir = Vector2.zero;//停止
        yield break;
    }

}
