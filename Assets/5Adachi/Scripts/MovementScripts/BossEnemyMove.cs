using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove : BossMoveAction
{
    /// <summary>タイマー</summary>
    float _timer = 0f;
    bool _speedUp = false;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>移動時間</summary>
    [SerializeField, Header("ダッシュ時間")] float _moveTime = 0.5f;
    /// <summary>中央位置</summary>
    const float MIDDLE_POS = 0f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;

    public override void Enter(BossController contlloer)
    {
        _speedUp = false;
        StartCoroutine(Test(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        //その方向に移動
        if (_speedUp)
        {
            contlloer.Rb.velocity = _dir * (contlloer.Speed * 2f);
        }
        else
        {
            contlloer.Rb.velocity = _dir * contlloer.Speed * 0.5f;
        }
        
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
            yield return new WaitForSeconds(JUDGMENT_TIME);

            //プレイヤーの方向に移動
            _dir = new Vector2(GameManager.Instance.Player.transform.position.x - controller.transform.position.x, GameManager.Instance.Player.transform.position.y - controller.transform.position.y).normalized;
            if(_timer >= 2f)
            {
                //ダッシュ時間
                _speedUp = true;
                yield return new WaitForSeconds(0.5f);
                //停止時間
                _dir = Vector2.zero;
                yield return new WaitForSeconds(1f);
                _speedUp = false;
                _timer = 0;
            }
        }
    }
}
