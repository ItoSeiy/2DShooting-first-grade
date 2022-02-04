using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement03 : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;

    bool _isMove02 = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("待機時間")] public float _stopTime = default;
    Vector2 _dir;
    int _count = 0;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(RandomMovement());
    }

    protected override void Attack()
    {

    }

    protected override void OnGetDamage()
    {

    }

    protected override void Update()
    {
        base.Update();

        if (_isMove02 == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        }
    }
    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            while (true)
            {
                Rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(_stopTime);

                if (transform.position.y > 2.5f)      //上に移動しすぎたら
                {
                    y = Random.Range(-1.0f, -0.1f);
                }
                else if (transform.position.y < 1.5f)//下に移動しすぎたら
                {
                    y = Random.Range(0.1f, 1.0f);
                }
                else                    //上下どっちにも移動しすぎてないなら
                {
                    y = Random.Range(-1.0f, 1.0f);
                }
                if (transform.position.x > 4)         //右に移動しすぎたら
                {
                    x = (Random.Range(-3.0f, -1.0f));
                }
                else if (transform.position.x < -4)   //左に移動しぎたら
                {
                    x = (Random.Range(1.0f, 3.0f));
                }
                else                     //左右どっちにも移動しすぎてないなら
                {
                    x = Random.Range(-3.0f, 3.0f);
                }

                _dir = new Vector2(x, y);

                Rb.velocity = _dir * _speed;
                yield return new WaitForSeconds(0.5f);

                Debug.Log(x);
                Debug.Log(y);
                _count = Random.Range(0, 10);
                Debug.Log(_count);
                if (_count == 9)
                {
                    Veritical();
                    break;
                }
            }
        }
        void Veritical()//プレイヤーの位置によって左右のどちらかに移動するかを決める
        {
            _isMove02 = true;
            Debug.Log("huuuu");
            Rb.velocity = new Vector2(0, 0);
            if (_player.transform.position.x >= transform.position.x)//プレイヤーが右にいたら
            {
                Debug.Log("right");
                Rb.velocity = new Vector2(4, 0);
                StartCoroutine(Stop1());
            }
            else                                                     //左にいたら
            {
                Debug.Log("left");
                Rb.velocity = new Vector2(-4, 0);
                StartCoroutine(Stop2());
            }
        }



        IEnumerator Stop1()//プレイヤーと同じx座標になると止まる
        {
            Debug.Log("Step1");
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (_player.transform.position.x <= transform.position.x)
                {
                    Rb.velocity = new Vector2(0, 0);
                    StartCoroutine(Down());
                    break;
                }
            }
        }
        IEnumerator Stop2()//プレイヤーと同じx座標になると止まる
        {
            Debug.Log("Step2");
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (_player.transform.position.x >= transform.position.x)
                {
                    Rb.velocity = new Vector2(0, 0);
                    StartCoroutine(Down());
                    break;
                }
            }
        }
        IEnumerator Down()//下に行く
        {
            Debug.Log("Down");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Down");
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                Rb.velocity = new Vector2(0, -4);

                if (_player.transform.position.y + 1 >= transform.position.y)//プレイヤーの高さまで来たら
                {
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(1);
                    Debug.Log("Up");
                    StartCoroutine(Up());
                    break;
                }
                else if (transform.position.y <= -4)//下降制限まで来たら
                {
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(1);
                    //_isMove = false;
                    Debug.Log("Up");
                    StartCoroutine(Up());
                    break;
                }
            }
        }
        IEnumerator Up()//上に行く
        {
            Debug.Log("Up");
            Rb.velocity = new Vector2(0, 3);
            yield return new WaitForSeconds(3);
            Rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2);
            StartCoroutine(Back());
        }

        IEnumerator Back()//反対側に行く
        {
            Debug.Log("Back");

            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (0 >= transform.position.x)//プレイヤーが右にいたら
                {
                    Debug.Log("right");
                    Rb.velocity = new Vector2(4, 0);
                    yield return new WaitForSeconds(Random.Range(1, 3));
                    Rb.velocity = new Vector2(0, 0);
                    StartCoroutine(RandomMovement());
                    break;
                }
                else                          //左にいたら
                {
                    Debug.Log("left");
                    Rb.velocity = new Vector2(-4, 0);
                    yield return new WaitForSeconds(Random.Range(1, 3));
                    Rb.velocity = new Vector2(0, 0);
                    _isMove02 = false;
                    StartCoroutine(RandomMovement());
                    break;
                }
            }
        }
    }
}
