using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveAttack : MonoBehaviour
{
    Rigidbody2D _rb;

    public bool _isMove = false;
    public bool _isMove2 = false;
    int _count = 0;
    int _count2 = 0;
    public float _x;
    public float _x2;
    public float _y;
    public float _y2;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _isMove = true;
        _isMove2 = true;

        _x = _player.transform.position.x;

        Veritical();
    }
    private void Veritical()//プレイヤーの位置によって左右のどちらかに移動するかを決める関数
    {
        Debug.Log("huuuu");
        _rb.velocity = new Vector2(0, 0);
        if (_player.transform.position.x >= transform.position.x)//プレイヤーが右にいたら
        {
            Debug.Log("right");
            _count = 1;
            _rb.velocity = new Vector2(_x, 0);
        }
        else//左にいたら
        {
            Debug.Log("left");
            _count = 2;
            _rb.velocity = new Vector2(_x, 0);
        }
    }

    /*IEnumerator Back()
    {
        Debug.Log("Back");
        if (_count == 4 && transform.position.x <= 0)
        {

            Debug.Log("c1");
            _rb.velocity = new Vector2(5, 0);
            yield return new WaitForSeconds(2);
            _count = 6;
            yield break;

        }
        else if (_count == 5 && transform.position.x >= 0)
        {

            Debug.Log("c2");
            _rb.velocity = new Vector2(-5, 0);
            yield return new WaitForSeconds(2);
            _count = 6;
            yield break;
        }
    }*/

    private void Stop01()//プレイヤーと同じx座標になると止まる関数
    {
        if (_count == 1)
        {
            Debug.Log("1");
            if (_player.transform.position.x <= transform.position.x)
            {
                _rb.velocity = new Vector2(0, 0);
                StartCoroutine(Down());
            }
        }
        else if (_count == 2)
        {
            Debug.Log("2");
            if (_player.transform.position.x >= transform.position.x)
            {
                _rb.velocity = new Vector2(0, 0);
                StartCoroutine(Down());
            }
        }
    }

    private void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));

        if (_isMove)
        {
            Stop01();
        }


        /*if (_count == 4 || _count == 5)
        {
            Back();           
        }*/
    }





    IEnumerator Down()//下に行くやつ
    {
        yield return new WaitForSeconds(0.5f);
        if (_isMove)
        {
            _y = _player.transform.position.y;
            _rb.velocity = new Vector2(0, _y * 3);

            if (_player.transform.position.y + 1 >= transform.position.y)//プレイヤーの高さまで来たら
            {
                _rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1);
                Debug.Log("Up");
                StartCoroutine(Up());
            }
            else if (transform.position.y <= -4)//下降制限まで来たら
            {
                _rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1);
                //_isMove = false;
                Debug.Log("Up");
                StartCoroutine(Up());
            }
        }

        IEnumerator Up()//上に行くやつ
        {
            //Debug.Log("Up");
            _isMove = false;
            _rb.velocity = new Vector2(0, 3);
            yield return new WaitForSeconds(3);
            _rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2);
            _count = 3;

            /*if (_isMove2)
            {
                if (transform.position.x >= 0)
                {
                    _isMove2 = false;
                    _count = 4;
                    Debug.Log("b1");
                    _rb.velocity = new Vector2(-5, 0);
                    //StartCoroutine(Back());
                }
                else if (transform.position.x <= 0)
                {
                    _isMove2 = false;
                    _count = 5;
                    Debug.Log("b2");
                    _rb.velocity = new Vector2(5, 0);
                    //StartCoroutine(Back());
                }
            }*/
        }

        /*void Back()
        {
            Debug.Log("Back");
            if (_count == 4 && transform.position.x <= 0)
            {
                Debug.Log("c1");
                _rb.velocity = new Vector2(5, 0);
                _count = 6;

            }
            else if (_count == 5 && transform.position.x >= 0)
            {

                Debug.Log("c2");
                _rb.velocity = new Vector2(-5, 0);
                _count = 6;
            }
        }*/
    }
}
