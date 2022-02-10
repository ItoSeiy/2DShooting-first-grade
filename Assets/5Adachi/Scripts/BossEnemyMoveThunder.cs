using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;   
    /// <summary>水平、横方向</summary>
    const float HORIZONTAL = 1f;
    /// <summary>垂直、縦方向</summary>
    float _vertical = 1f;
    /// <summary>スピード</summary>
    [SerializeField, Header("スピード")] float _speed;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>中央位置</summary>
    float _middlePos = 0;
    /// <summary>判定の際に待ってほしい時間</summary>
    float _judgmentTime = 0.1f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>逆の動き</summary>
    const float REVERSE_MOVEMENT = -1f;
    /// <summary>現在のパターン</summary>
    int _pattern = 0;
    /// <summary>最初に左にいるパターン</summary>
    const  int PATTERN1 = 1;
    /// <summary>最初に右にいるパターン</summary>
    const int PATTERN2 = 2;
    /// <summary>パターン初期化</summary>
    const int PATTERN_INIT = 0;
    /// <summary>時間</summary>
    float _time = 0f;
    /// <summary>時間制限,上下移動を逆にする<summary>
    [SerializeField,Header("上下移動を逆にする")] float _timeLimit = 0.5f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Thunder());
    }
    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;//その方向に移動
        _time += Time.deltaTime;//時間
    }

    /// <summary>
    /// 端に移動してから反対側にジグザグ移動する
    /// </summary>
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

        //端についたら停止
        while (true)
        {
            yield return new WaitForSeconds(_judgmentTime);//判定回数の制御

            if (transform.position.x <= _leftLimit)//左についたら
            {
                Debug.Log("a");
                _pattern = PATTERN1;//パターン1に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
            else if (transform.position.x >= _rightLimit)//右についたら
            {
                Debug.Log("a");
                _pattern = PATTERN2;//パターン2に切り替え
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }
        }

        _time = PATTERN_INIT;//タイムをリセット

        //左から右にジグザグ動く
        while (true && _pattern == PATTERN1)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(_judgmentTime);//判定回数の制御

            if (transform.position.x <= _rightLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(HORIZONTAL, _vertical);//右上or右下に動きながら

                if (_time >= _timeLimit)//制限時間になったら
                {
                    _time = PATTERN_INIT;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする                   
                }
                else if (transform.position.y >= 4)
                {
                    _time = PATTERN_INIT;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする   
                }
                else if (transform.position.y <= -4)
                {
                    _time = PATTERN_INIT;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする   
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
            yield return new WaitForSeconds(_judgmentTime);//判定回数の制御

            if (transform.position.x >= _leftLimit)//端についていないなら繰り返す
            {
                _dir = new Vector2(-HORIZONTAL, _vertical);//左上or左下に動きながら

                if (_time >= _timeLimit)//制限時間になったら
                {
                    _time = PATTERN_INIT;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする   
                }
                else if (transform.position.y >= 4)
                {
                    _time = PATTERN_INIT;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする   
                }
                else if (transform.position.y <= -4)
                {
                    _time = PATTERN_INIT;//タイムをリセット
                    _vertical *= REVERSE_MOVEMENT;//上下の動きを逆にする   
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
