using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRush : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    /// <summary>プレイヤーのタグ</summary>
    [SerializeField,Header("プレイヤーのタグ")] private string _playerTag = null;
    /// <summary>スピード</summary>
    [SerializeField, Header("スピード")] float _speed = 5f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = -3f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>切り替え</summary>
    bool _switch = false;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>判定回数の制限</summary>
    float _judgmentLimit = 0.1f;   
    /// <summary>修正値</summary>
    float _correctionValue = 0.5f;
    /// <summary>時間</summary>
    float _time;
    /// <summary>時間の限界</summary>
    [SerializeField,Header("追尾時間")] float _timeLimit = 4f;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine("Rush");
    }

    private void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
        _time += Time.deltaTime;
    }

    /// <summary>
    /// 一定時間プレイヤーをロックオンしたあと真下に突進する
    /// </summary>
    IEnumerator Rush()        
    {
        //プレイヤーの位置によって左右のどちらかに移動するかを決める
        /*//プレイヤーが右にいたら
        if (_player.transform.position.x >= transform.position.x)
        {
            Debug.Log("right");
            _switch = true;//パターン１に切り替え
            _dir = Vector2.right;//右に移動
        }
        else//左にいたら
        {
            Debug.Log("left");
            _switch = false;//パターン２に切り替え
            _dir = Vector2.left;//左に移動
        }
      
        while (true) //プレイヤーの辺りに着いたら
        {
            yield return new WaitForSeconds(_judgmentLimit);

            //右に移動したときに
            if (_switch && _player.transform.position.x <= transform.position.x)
            {
                _dir = Vector2.zero;//停止
                break;
            }
            //左に移動したときに
            else if (!_switch && _player.transform.position.x >= transform.position.x)
            {
               _dir = Vector2.zero;//停止
                break;
            }
        }*/

        _time = 0;

        //x座標だけプレイヤーの近くに移動する
        while (true)
        {
            yield return new WaitForSeconds(_judgmentLimit);
            
            //プレイヤーが右にいたら
            if (_player.transform.position.x > transform.position.x +_correctionValue)
            {
                Debug.Log("right");
                _dir = Vector2.right;//右に移動
            }
            //プレイヤーが左にいたら
            else if(_player.transform.position.x  < transform.position.x -_correctionValue)
            {
                Debug.Log("left");  
                _dir = Vector2.left;//左に移動
            }
            else//プレイヤーのx座標が近かったら
            {
                _dir = Vector2.zero;//停止
            }
            //限界に達したら
            if ( _time >= _timeLimit)
            {
                break;
            }
        }

        while (true)//サガる
        {
            yield return new WaitForSeconds(_judgmentLimit);
            _dir = Vector2.down;//サガる

            if (transform.position.y <= _lowerLimit)//サガったら
            {
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                _dir = Vector2.up;//上がる
                break;
            }
        }       
        
        while (true)//一定の場所まで上がる
        {
            yield return new WaitForSeconds(_judgmentLimit);
            
            if (3 <= transform.position.y)//一定の場所まできたら
            {
                Debug.Log("ラスト");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }            
        }
    }
}
