using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    
    /// <summary>水平、横方向</summary>
    private float _horizontal = 0f;
    /// <summary>垂直、縦方向</summary>
    private float _veritical = 0f;
    /// <summary>速度</summary>
    float _speed = 4f;
    /// <summary>停止時間</summary>
    [SerializeField,Header("停止時間")] float _stopTime = 2f;
    /// <summary>右限</summary>
    [SerializeField,Header("右限")] float _rightLimit = 4f;
    /// <summary>左限</summary>
    [SerializeField,Header("左限")] float _leftLimit = -4f;
    /// <summary>上限</summary>
    [SerializeField,Header("上限")] float _upperLimit = 2.5f;
    /// <summary>下限</summary>
    [SerializeField,Header("下限")] float _lowerLimit = 1.5f;     
    /// <summary>移動時間</summary>
    [SerializeField,Header("移動時間")] float _moveTime = 0.5f;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>左方向</summary>
    float _leftDir = -1f;
    /// <summary>右方向</summary>
    float _rightDir = 1f;
    /// <summary>上方向</summary>
    float _upDir = 1f;
    /// <summary>下方向</summary>
    float _downDir = -1;
    /// <summary>方向なし</summary>
    float _noDir = 0f;


    /// <summary>
    /// ランダム方向に動く
    /// </summary>
    IEnumerator RandomMovement()
    {       
        while (true)
        {
            //一定時間止まる
            _rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);

            //場所によって移動できる左右方向を制限する
            if (transform.position.x > _rightLimit)         //右に移動しすぎたら
            {
                _horizontal = Random.Range(_leftDir, _noDir);//左移動可能
            }
            else if(transform.position.x < _leftLimit)   //左に移動しぎたら
            {
                _horizontal = Random.Range(_noDir, _rightDir);//右移動可能
            }
            else　　　　　　　　　　　　         //左右どっちにも移動しすぎてないなら
            {
                _horizontal = Random.Range(_leftDir, _rightDir);//自由に左右移動可能          
            }

            //場所によって移動できる上下方向を制限する
            if(transform.position.y > _upperLimit)      //上に移動しすぎたら
            {
                _veritical = Random.Range(_downDir, _noDir);//下移動可能
            }
            else if (transform.position.y < _lowerLimit)//下に移動しすぎたら
            {
                _veritical = Random.Range(_noDir, _upDir);//上移動可能
            }
            else　　　　　　　　　　　　　　      //上下どっちにも移動しすぎてないなら
            {
                _veritical = Random.Range(_downDir, _upDir);//自由に上下移動可能
            }

            _dir = new Vector2(_horizontal, _veritical);
            //一定時間移動する
            _rb.velocity = _dir.normalized * _speed;
            yield return new WaitForSeconds(_moveTime);
            
            Debug.Log("x" + _horizontal);
            Debug.Log("y" + _veritical);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine("RandomMovement");
        
    }
    private void Update()
    {
        //_rb.velocity = _dir * _speed;
    }
}
