using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveAttack : MonoBehaviour
{
    Rigidbody2D _rb;
    public bool _isMove = false;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _isMove = true;
        Veritical();
    }
    private void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
    }
    private void Veritical()//プレイヤーの位置によって左右のどちらかに移動するかを決める関数
    {
        Debug.Log("huuuu");
        _rb.velocity = new Vector2(0, 0);
        if (_player.transform.position.x >= transform.position.x)//プレイヤーが右にいたら
        {
            Debug.Log("right");
            _rb.velocity = new Vector2(4, 0);
            StartCoroutine(Stop1());
        }
        else//左にいたら
        {
            Debug.Log("left");
            _rb.velocity = new Vector2(4, 0);
            StartCoroutine(Stop2());
        }
    }
    

    IEnumerator Stop1()//プレイヤーと同じx座標になると止まる関数
    {
            Debug.Log("1");
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (_player.transform.position.x <= transform.position.x)
            {
                _rb.velocity = new Vector2(0, 0);
                StartCoroutine(Down());
                break;
            }
        }
    }
    IEnumerator Stop2()//プレイヤーと同じx座標になると止まる関数
    {
            Debug.Log("2");
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (_player.transform.position.x >= transform.position.x)
            {
                _rb.velocity = new Vector2(0, 0);
                StartCoroutine(Down());
                break;
            }
        }
    }
    IEnumerator Down()//下に行く
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (_isMove)
            {
                _rb.velocity = new Vector2(0, -4);

                if (_player.transform.position.y + 1 >= transform.position.y)//プレイヤーの高さまで来たら
                {
                    _rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(1);
                    Debug.Log("Up");
                    StartCoroutine(Up());
                    break;
                }
                else if (transform.position.y <= -4)//下降制限まで来たら
                {
                    _rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(1);
                    //_isMove = false;
                    Debug.Log("Up");
                    StartCoroutine(Up());
                    break;
                }
            }
        }
    }
        IEnumerator Up()//上に行く
        {
            //Debug.Log("Up");
            _isMove = false;
            _rb.velocity = new Vector2(0, 3);
            yield return new WaitForSeconds(3);
            _rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2);
            StartCoroutine(Back());
        }

        IEnumerator Back()//反対側に行く
        {
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (0 >= transform.position.x)//プレイヤーが右にいたら
                {
                    Debug.Log("right");
                    _rb.velocity = new Vector2(4, 0);
                    yield return new WaitForSeconds(Random.Range(1,3));
                    _rb.velocity = new Vector2(0, 0);
                    break;
                }
                else//左にいたら
                {
                    Debug.Log("left");
                    _rb.velocity = new Vector2(-4, 0);
                    yield return new WaitForSeconds(Random.Range(1,3));
                    _rb.velocity = new Vector2(0, 0);
                    break;
                }
            }
        }
}
