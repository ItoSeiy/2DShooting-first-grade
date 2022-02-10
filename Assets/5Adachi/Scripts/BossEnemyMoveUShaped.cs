using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped: MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    [SerializeField,Header("スピード")] float _speed = 5f;    
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 3.5f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = -3;
    /// <summary>中央位置</summary>
    float _middlePos = 0;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>最短移動時間</summary>
    float _shortMoveTime = 1f;
    /// <summary>最長移動時間</summary>
    float _longMoveTime = 3f;
    /// <summary>停止時間</summary>
    [SerializeField,Header("停止時間")] float _stopTime = 1f;
    /// <summary>判定回数の制限</summary>
    float _judgmentTime = 0.1f;
    /// <summary>切り替え</summary>
    int _switch = 0;
    /// <summary>下にいる時に左にいるパターン</summary>
    const int PATTERN = 1;
    /// <summary>下にいる時に右にいるパターン</summary>
    const int PATTERN2 = 2;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UShaped());
    }

    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
    }

    /// <summary>
    /// U字型のような移動をする
    /// </summary>
    public IEnumerator UShaped()
    {
        if (transform.position.x < _middlePos)//画面より左半分にいたら
        {            
            _dir = Vector2.right;//右に動く
            Debug.Log("a");
        }
        else//画面より右半分にいたら
        {         
            _dir = Vector2.left;//左に動く
            Debug.Log("b");
        }

        while(true)//端に着くまで横に動く
        {
            yield return new WaitForSeconds(_judgmentTime);
            //反対側に着いたら
            if ((transform.position.x <= _leftLimit) || (transform.position.x >= _rightLimit))
            {
                Debug.Log("c");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.down;//下がる
                break;
            }
        }


        while (true)//反対側に着くまで移動する
        {
            yield return new WaitForSeconds(_judgmentTime);
            //反対側に着いたら
            if (transform.position.x <= _leftLimit && transform.position.y <= _lowerLimit)
            {
                Debug.Log("e");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.right;//画面下端にいたら右に行く
                _switch = PATTERN;//パターン1
                break;
            }
            //反対側に着いたら
            else if (transform.position.x >= _rightLimit && transform.position.y <= _lowerLimit)
            {
                Debug.Log("f");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.left;//画面下端にいたら左に行く
                _switch = PATTERN2;//パターン2
                break;
            }
        }

        while (true)//反対側に着くまで横移動する
        {
            yield return new WaitForSeconds(_judgmentTime);
            //反対側についたら上に行く(パターン1or2）
            if ((transform.position.x >= _rightLimit && _switch == PATTERN) || (transform.position.x <= _leftLimit && _switch == PATTERN2))
            {
                Debug.Log("g");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.up;//上に上がる
                break;
            }
        }

        while (true)//ある程度上にいくまで移動する
        {
            yield return new WaitForSeconds(_judgmentTime);
            if (transform.position.y >= _upperLimit)//ある程度上にいったら
            {
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                Debug.Log("h");

                if (transform.position.x < _middlePos)//左にいたら
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
        //移動時間
        yield return new WaitForSeconds(Random.Range(_shortMoveTime, _longMoveTime));      
        _dir = Vector2.zero;//停止
    }
}
