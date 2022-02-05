using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>水平、横方向</summary>
    private float _horizontal = 0;
    /// <summary>垂直、縦方向</summary>
    private float _veritical = 0;
    /// <summary>速度</summary>
    float _speed = 4f;
    /// <summary>停止時間</summary>
    [SerializeField,Header("停止時間")] public float _stopTime = 2;
    /// <summary>上限</summary>
    [SerializeField,Header("上限")] float _upperLimit = 2.5f;
    /// <summary>下限</summary>
    [SerializeField,Header("下限")] float _lowerLimit = 1.5f;  
    /// <summary>右限</summary>
    [SerializeField,Header("右限")] float _rightLimit = 4f;
    /// <summary>左限</summary>
    [SerializeField,Header("左限")] float _leftLimit = -4f;
    /// <summary>移動時間</summary>
    [SerializeField,Header("移動時間")] float _moveTime = 0.5f;

    IEnumerator RandomMovement()
    {       
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            _rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);

            if(transform.position.y > _upperLimit)      //上に移動しすぎたら
            {
                _veritical = Random.Range(-1.0f, -0.1f);
            }
            else if (transform.position.y < _lowerLimit)//下に移動しすぎたら
            {
                _veritical = Random.Range(0.1f, 1.0f);
            }
            else　　　　　　　　　　　　　　      //上下どっちにも移動しすぎてないなら
            {
                _veritical = Random.Range(-1.0f, 1.0f);
            }
            if (transform.position.x > _rightLimit)         //右に移動しすぎたら
            {
                _horizontal = (Random.Range(-3.0f, -1.0f));
            }
            else if(transform.position.x < _leftLimit)   //左に移動しぎたら
            {
                _horizontal = (Random.Range(1.0f, 3.0f));
            }
            else　　　　　　　　　　　　         //左右どっちにも移動しすぎてないなら
            {
                _horizontal = Random.Range(-3.0f, 3.0f);               
            }

            _dir = new Vector2(_horizontal, _veritical);

            _rb.velocity = _dir * _speed;
            yield return new WaitForSeconds(_moveTime);
            
            Debug.Log(_horizontal);
            Debug.Log(_veritical);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomMovement());
    }
    private void Update()
    {

    }
}
