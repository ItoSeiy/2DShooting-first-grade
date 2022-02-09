using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>カウント</summary>
    int _count = 0;
    /// <summary>カウントの限界数</summary>
    int _limitCount = 5;
    /// <summary>水平、横方向</summary>
    float _horizontal = 1f;
    /// <summary>垂直、縦方向</summary>
    float _vertical = 1f;
    /// <summary>スピード</summary>
    [SerializeField, Header("スピード")] float _speed;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>中央位置</summary>
    float _middlePos = 0;
    /// <summary>判定回数の制限</summary>
    float _judgmentLimit = 0.1f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>逆の動き</summary>
    int _reverseMovement = -1;
    /// <summary>切り替え</summary>
    int _switch = 0;
    /// <summary>最初に左にいるパターン</summary>
    int _pattern01 = 1;
    /// <summary>最初に右にいるパターン</summary>
    int _pattern02 = 2;
    /// <summary>初期化</summary>
    int _initialize = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Thunder");
    }
    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
    }

    /// <summary>
    /// 端に移動してから反対側にジグザグ移動する
    /// </summary>
    /// <returns></returns>
    IEnumerator Thunder()
    {
        if (transform.position.x >= _middlePos)//画面右側にいたら
        {
            Debug.Log("left");
            _dir = Vector2.left;//左に移動
        }
        else//左側にいたら
        {
            Debug.Log("right");
            _dir = Vector2.right;//右に移動
        }

        while (true)
        {
            yield return new WaitForSeconds(_judgmentLimit);

            if (transform.position.x <= _leftLimit)//左についたら
            {
                Debug.Log("a");
                _switch = _pattern01;//パターン1に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
            else if (transform.position.x >= _rightLimit)//右についたら
            {
                Debug.Log("a");
                _switch = _pattern02;//パターン2に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
        }

        //左から右にジグザグ動く
        while (true && _switch == _pattern01)
        {
            yield return new WaitForSeconds(_judgmentLimit);
            _count++;//カウントを+1

            if (transform.position.x <= _rightLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(_horizontal, _vertical);//右に動く

                if (_count >= _limitCount)//一定のカウントになったら
                {
                    _count = _initialize;//カウントを0に
                    _vertical *= _reverseMovement;//上下の動きを逆にする                   
                }
            }
            else
            {
                _dir = Vector2.zero;//停止
                break;
            }
        }

        //右から左にジグザグ動く
        while (true && _switch == _pattern02)
        {
            yield return new WaitForSeconds(_judgmentLimit);
            _count++;//カウントを+1
            if (transform.position.x >= _leftLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(-_horizontal, _vertical);//左に動く

                if (_count >= _limitCount)//一定のカウントになったら
                {
                    _count = _initialize;//カウントを０に                    
                    _vertical *= _reverseMovement;//上下の動きを逆にする   
                }
            }
            else//端についたら
            {
                _dir = Vector2.zero;//停止
                break;
            }
        }
    }
}
