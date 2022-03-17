using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : BossMoveAction
{
    /// <summary>タイマー</summary>
    float _timer = 0f;
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
    /// <summary>中央位置</summary>
    const float MIDDLE_POS = 0f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;

    public override void Enter(BossController contlloer)
    {
        Debug.Log("発動完了");
        StartCoroutine(Test(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        //その方向に移動
        contlloer.Rb.velocity = _dir * contlloer.Speed / 2f;
        //タイマー
        _timer += Time.deltaTime;

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

    IEnumerator Test(BossController controller)
    {
        _timer = 0;

        while (true)
        {
            Debug.Log("a");
            //プレイヤーの方向に移動
            _dir = new Vector2(GameManager.Instance.Player.transform.position.x - controller.transform.position.x, GameManager.Instance.Player.transform.position.y - controller.transform.position.y).normalized;
            //yield return new WaitForSeconds(1f);

            if (_timer >= 10f)
            {
                break;
            }
        }
        _dir = Vector2.zero;
        yield break;
    }
}
