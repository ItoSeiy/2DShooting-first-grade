using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped: MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    [SerializeField,Header("スピード")] float _speed = 5f;    
    /// <summary>横限</summary>
    [SerializeField,Header("横限")] float _horizontalLimit = 7.5f;
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
    float _judgmentLimit = 0.1f;
    /// <summary>切り替え</summary>
    int _switch = 0;
    /// <summary>下にいる時に左にいるパターン</summary>
    int _pattern01 = 1;
    /// <summary>下にいる時に右にいるパターン</summary>
    int _pattern02 = 2;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UShaped());
    }

    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
    }
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

        while(true)//端に着くまで下がる
        {
            yield return new WaitForSeconds(_judgmentLimit);
            //反対側に着いたら
            if (transform.position.x <= -_horizontalLimit || transform.position.x >= _horizontalLimit)
            {
                Debug.Log("c");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.down;//画面左端にいたら下がる
                break;
            }
        }


        while (true)//反対側に着くまで移動する
        {
            yield return new WaitForSeconds(_judgmentLimit);
            //反対側に着いたら
            if (transform.position.x <= -_horizontalLimit && transform.position.y <= _lowerLimit)
            {
                Debug.Log("e");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.right;//画面下端にいたら右に行く
                _switch = _pattern01;//パターン1
                break;
            }
            //反対側に着いたら
            else if (transform.position.x >= _horizontalLimit　&& transform.position.y <= _lowerLimit)
            {
                Debug.Log("f");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.left;//画面下端にいたら左に行く
                _switch = _pattern02;//パターン2
                break;
            }
        }

        while (true)//反対側に着くまで横移動する
        {
            yield return new WaitForSeconds(_judgmentLimit);
            //反対側についたら上に行く(パターン1or2）
            if ((transform.position.x >= _horizontalLimit && _switch == _pattern01) || (transform.position.x <= -_horizontalLimit && _switch == _pattern02))
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
            yield return new WaitForSeconds(_judgmentLimit);
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
