using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveDescend : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    [SerializeField] float _speed = 5f;
    
    /// <summary>横の限界</summary>
    [SerializeField] float _horizontalLimit = 7.5f;
    /// <summary>上限</summary>
    [SerializeField] float _upperLimit = 3.5f;
    /// <summary>下限</summary>
    [SerializeField] float _lowerLimit = -3;

    Vector2 _direction;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Descend());
    }

    void Update()
    {
        _rb.velocity = _direction * _speed;
    }

    public IEnumerator Descend()
    {
        if (transform.position.x < 0)
        {
            //画面より左半分にいたら右に動く
            _direction = Vector2.right;
            Debug.Log("a");
        }
        else                         //右にいたら
        {
            //画面より右半分にいたら右に動く
            _direction = Vector2.left;
            Debug.Log("b");
        }

        

        while(true)//端に着くまで下がる
        {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x <= -_horizontalLimit)
            {
                Debug.Log("c");
                //画面左端にいたら下がる
                _direction = Vector2.down;
                break;
            }
            else if (transform.position.x >= _horizontalLimit)
            {
                Debug.Log("d");
                //画面右端にいたら下がる
                _direction = Vector2.down;
                break;
            }
        }


        while (true)//反対側に着くまで移動する
        {
            yield return new WaitForSeconds(0.1f);

            if (transform.position.y <= _lowerLimit && transform.position.x <= -_horizontalLimit)
            {
                Debug.Log("e");
                //画面下端にいたら右に行く
                _direction = Vector2.right;
                break;
            }
            else if (transform.position.y <= _lowerLimit && transform.position.x >= _horizontalLimit)
            {
                Debug.Log("f");
                //画面下端にいたら左に行く
                _direction = Vector2.left;
                break;
            }
        }

        yield return new WaitForSeconds(2);

        while (true)//上に上がる
        {
            Debug.Log("g");
            yield return new WaitForSeconds(0.1f);
            _direction = Vector2.up;

            if (transform.position.y >= _upperLimit)
            {
                Debug.Log("h");
                //上端にいたら止まる
                break;
            }
        }

        while(true)//いい感じの位置に
        {
            if (transform.position.x < 0)
            {
                //左にいたら右に行く
                _direction = Vector2.right;
                Debug.Log("a");
                break;
            }
            else
            {
                //右にいたら左に行く
                _direction = Vector2.left;
                Debug.Log("b");
                break;
            }
        }
        yield return new WaitForSeconds(Random.Range(1, 3));
        //止まる
        _direction = Vector2.zero;
    }
}
