using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : BossMoveAction
{
    /// <summary>水平、横方向</summary>
    float _horizontal = 0f;
    /// <summary>垂直、縦方向</summary>
    float _veritical = 0f;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>移動時間</summary>
    [SerializeField, Header("移動時間")] float _moveTime = 0.5f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 4f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -4f;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 2.5f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = 1.5f;
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
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;

    public override void Enter(BossController contlloer)
    {
        StartCoroutine(Test(contlloer));
    }

    public override void Exit(BossController contlloer)
    {
        //その方向に移動
        contlloer.Rb.velocity = _dir * contlloer.Speed;

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

    public override void ManagedUpdate(BossController contlloer)
    {
        StopAllCoroutines();
    }

    IEnumerator Test(BossController controller)
    {
        while(true)
        {
            //プレイヤーの方向に移動
            _dir = (GameManager.Instance.Player.transform.position - controller.transform.position).normalized;
            yield return new WaitForSeconds(JUDGMENT_TIME);
        }
    }
}
