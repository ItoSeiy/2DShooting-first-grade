using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : BossMoveAction
{
    /// <summary>水平、横方向</summary>
    float _horizontal = 0f;
    /// <summary>垂直、縦方向</summary>
    float _veritical = 0f;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>停止時間</summary>
    [SerializeField,Header("停止時間")] float _stopTime = 2f;
    /// <summary>移動時間</summary>
    [SerializeField,Header("移動時間")] float _moveTime = 0.5f;
    /// <summary>右限</summary>
    [SerializeField,Header("右限")] float _rightLimit = 4f;
    /// <summary>左限</summary>
    [SerializeField,Header("左限")] float _leftLimit = -4f;
    /// <summary>上限</summary>
    [SerializeField,Header("上限")] float _upperLimit = 2.5f;
    /// <summary>下限</summary>
    [SerializeField,Header("下限")] float _lowerLimit = 1.5f;           
    /// <summary>左方向</summary>
    const float LEFT_DIR = -1f;
    /// <summary>右方向</summary>
    const float RIGHT_DIR = 1f;
    /// <summary>上方向</summary>
    const float UP_DIR = 1f;
    /// <summary>下方向</summary>
    const float DOWN_DIR = -1f;
    /// <summary>方向なし</summary>
    const float NO_DIR = 0f;
    /// <summary>中央位置</summary>
    const float MIDDLE_POS = 0f;

    public override void Enter(BossController contlloer)
    {
        StartCoroutine(RandomMovement(contlloer)) ;
    }

    public override void ManagedUpdate(BossController contlloer)
    {       
        //右に移動したら右を向く
        if(contlloer.Rb.velocity.x > MIDDLE_POS)
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
    /// ランダム方向に動く
    /// </summary>
    IEnumerator RandomMovement(BossController controller)
    {       
        while (true)
        {
            //一定時間止まる
            controller.Rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);

            //場所によって移動できる左右方向を制限する
            if (controller.transform.position.x > _rightLimit)         //右に移動しすぎたら
            {
                _horizontal = Random.Range(LEFT_DIR, NO_DIR);//左移動可能
            }
            else if(controller.transform.position.x < _leftLimit)   //左に移動しぎたら
            {
                _horizontal = Random.Range(NO_DIR, RIGHT_DIR);//右移動可能
            }
            else　　　　　　　　　　　　         //左右どっちにも移動しすぎてないなら
            {
                _horizontal = Random.Range(LEFT_DIR, RIGHT_DIR);//自由に左右移動可能          
            }

            //場所によって移動できる上下方向を制限する
            if(controller.transform.position.y > _upperLimit)      //上に移動しすぎたら
            {
                _veritical = Random.Range(DOWN_DIR, NO_DIR);//下移動可能
            }
            else if (controller.transform.position.y < _lowerLimit)//下に移動しすぎたら
            {
                _veritical = Random.Range(NO_DIR, UP_DIR);//上移動可能
            }
            else　　　　　　　　　　　　　　      //上下どっちにも移動しすぎてないなら
            {
                _veritical = Random.Range(DOWN_DIR, UP_DIR);//自由に上下移動可能
            }

            _dir = new Vector2(_horizontal, _veritical);//ランダムに移動
            //一定時間移動する
            controller.Rb.velocity = _dir.normalized * controller.Speed;
            yield return new WaitForSeconds(_moveTime);
            
            //Debug.Log("x" + _horizontal);
            //Debug.Log("y" + _veritical);
        }
    }
}
