using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveUShaped: MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;  
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>現在のパターン</summary>
    int _pattern = 0;
    /// <summary>スピード</summary>
    [SerializeField,Header("スピード")] float _speed = 5f;    
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
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//ゲットコンポーネント
        StartCoroutine(UShaped());//スタートコルーチン
    }

    void Update()
    {
        _rb.velocity = _dir.normalized * _speed;//方向にスピードを加える
    }

    /// <summary>
    /// U字型のような移動をする
    /// </summary>
    public IEnumerator UShaped()
    {
        _dir = new Vector2(-transform.position.x, 0f);

        //ボスのポジションXが0だったら棒立ちして詰むので
        if (transform.position.x == 0f)
        {
            _dir = Vector2.right;//右に移動
        }

        while(true)//端に着くまで横に動く
        {           
            //反対側に着いたら
            if ((transform.position.x <= _leftLimit) || (transform.position.x >= _rightLimit))
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
            if(transform.position.y <= _lowerLimit)
            {

                Debug.Log("f");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = new Vector2(-transform.position.x, 0f);//画面下端にいたら今いる場所の反対側に横移動
                break;
            }
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
        }

        yield return new WaitForSeconds(DELAY_TIME);//判定を遅らす

        while (true)//反対側に着くまで横移動する
        {          
            //反対側についたら上に行く
            if (transform.position.x >= _rightLimit || transform.position.x <= _leftLimit)
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

            if (transform.position.y >= _upperLimit)//ある程度上にいったら
            {
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                Debug.Log("h");

                if (transform.position.x < MIDDLE_POS)//左にいたら
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
    }
}
