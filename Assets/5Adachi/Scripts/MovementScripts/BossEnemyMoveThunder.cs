using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : BossMoveAction
{
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>垂直、縦方向</summary>
    float _vertical = 1f;
    /// <summary>タイマー</summary>
    float _timer = 0f;       
    /// <summary>現在のパターン</summary>
    int _pattern = 0;
    /// <summary>正常位置に軌道修正する</summary>
    bool _fix = false;   
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 4f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = -4f;
    /// <summary>時間制限,上下移動を逆にする時間<summary>
    [SerializeField, Header("上下移動を逆にする時間")] float _timeLimit = 0.5f;
    /// <summary>最初に左にいるパターン</summary>
    const int PATTERN1 = 1;
    /// <summary>最初に右にいるパターン</summary>
    const int PATTERN2 = 2;
    /// <summary>水平、横方向</summary>
    const float HORIZONTAL = 1f;
    /// <summary>逆の動き</summary>
    const float REVERSE_MOVEMENT = -1f;
    /// <summary>判定の際に待ってほしい時間</summary>
    const float JUDGMENT_TIME = 1/60;      
    /// <summary>タイマーのリセット用</summary>
    const float TIMER_RESET = 0f;      
    /// <summary>上に上がる</summary>
    const float MOVEUP = 1f;
    /// <summary>下にサガる</summary>
    const float MOVEDOWN = -1f;
    /// <summary>中央位置</summary>
    const float MIDDLE_POS = 0;

    public override void Enter(BossController contlloer)
    {
        _fix = true;//雷のような軌道を修正できるようにする
        StartCoroutine(Thunder(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        contlloer.Rb.velocity = _dir.normalized * contlloer.Speed;//その方向に移動
        _timer += Time.deltaTime;//時間

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
    /// 端に一直線に移動した後、反対側に着くまでジグザグ移動する
    /// </summary>
    IEnumerator Thunder(BossController controller)
    {
        _dir = new Vector2(-controller.transform.position.x, 0f);

        //ボスのポジションXが0だったら棒立ちして詰むので
        if (controller.transform.position.x == 0f)
        {
            _dir = Vector2.right;//右に移動
        }

        //端についたら停止
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制御

            if (controller.transform.position.x <= _leftLimit)//左についたら
            {
                Debug.Log("a");
                _pattern = PATTERN1;//パターン1に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
            else if (controller.transform.position.x >= _rightLimit)//右についたら
            {
                Debug.Log("a");
                _pattern = PATTERN2;//パターン2に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
        }

        _timer = TIMER_RESET;//タイムをリセット

        //ジグザクする動き

        //左から右にジグザグ動く
        while (true && _pattern == PATTERN1)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制御

            if (controller.transform.position.x <= _rightLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(HORIZONTAL, _vertical);//右上or右下に動きながら

                if (_timer >= _timeLimit)//制限時間になったら
                {
                    _timer = TIMER_RESET;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする                   
                }

                //画面外に行きそうになったら１度だけ軌道修正する
                else if (controller.transform.position.y >= _upperLimit && _fix)
                {
                    _vertical = MOVEDOWN;//下にサガる動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("3");
                }
                else if (controller.transform.position.y <= _lowerLimit && _fix)
                {
                    _vertical = MOVEUP;//上の動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("4");
                }
            }
            else
            {
                _dir = Vector2.zero;//停止
                break;
            }
        }

        //右から左にジグザグ動く
        while (true && _pattern == PATTERN2)
        {
            Debug.Log("2");
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制御

            if (controller.transform.position.x >= _leftLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(-HORIZONTAL, _vertical);//左上or左下に動きながら

                if (_timer >= _timeLimit)//制限時間になったら
                {
                    Debug.Log("4");
                    _timer = TIMER_RESET;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする   
                }

                //画面外に行きそうになったら１度だけ軌道修正する
                else if (controller.transform.position.y >= _upperLimit && _fix)
                {
                    _vertical = MOVEDOWN;//下にサガる動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("3");
                }
                else if (controller.transform.position.y <= _lowerLimit && _fix)
                {
                    _vertical = MOVEUP;//上に上がる動きにする   
                    _timer = TIMER_RESET;//タイムをリセット
                    _fix = false;//使えないようにする
                    Debug.Log("3");
                }
            }
            else//端についたら
            {
                _dir = Vector2.zero;//停止                
                break;
            }
        }

        _fix = true;//使えるようにする
        yield break;
    }

}
