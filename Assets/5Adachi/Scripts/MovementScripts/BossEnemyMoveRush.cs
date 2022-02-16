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
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 3f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = -3f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("降りた後の停止時間")] float _stopTime = 2f;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 0.1f;
    /// <summary>修正値</summary>
    const float PLAYER_POSTION_OFFSET = 0.5f;
    /// <summary>時間</summary>
    float _time = 0f;
    /// <summary>上に滞在する時間、追尾時間</summary>
    [SerializeField,Header("追尾時間(上に滞在している時間)")] float _stayingTime = 4f;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Rush());
    }

    private void Update()
    {
        _rb.velocity = _dir.normalized * _speed;
        _time += Time.deltaTime;
    }

    /// <summary>
    /// 一定時間プレイヤーをロックオンしたあと真下にサガる。その後上に上がる。
    /// </summary>
    IEnumerator Rush()        
    {
        _time = 0;

        //x座標だけプレイヤーの近くに移動する
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            //プレイヤーが右にいたら
            if (_player.transform.position.x > transform.position.x + PLAYER_POSTION_OFFSET)
            {
                Debug.Log("right");
                _dir = Vector2.right;//右に移動
            }
            //プレイヤーが左にいたら
            else if(_player.transform.position.x  < transform.position.x - PLAYER_POSTION_OFFSET)
            {
                Debug.Log("left");  
                _dir = Vector2.left;//左に移動
            }
            else//プレイヤーのx座標が近かったら
            {
                _dir = Vector2.zero;//停止
            }
            //限界に達したら
            if ( _time >= _stayingTime)
            {
                break;
            }
        }

        while (true)//サガる
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);
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
            yield return new WaitForSeconds(JUDGMENT_TIME);
            
            if (_upperLimit <= transform.position.y)//一定の場所まできたら
            {
                Debug.Log("ラスト");
                _dir = Vector2.zero;//停止
                yield return new WaitForSeconds(_stopTime);//停止時間
                break;
            }            
        }
    }
}
