using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement05 : EnemyBase
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
    int _thunder = 0;
    float _xMove = 5f;
    float _yMove = 6f;
    bool _switch = false;

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
            //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        }
        _thunder++;
        /*if (transform.position.x >= 8)//反対側にワープする予定
        {
            transform.position = new Vector2(transform.position.x * -1 + 0.1f, y);
        }
        else if (transform.position.x <= -8)
        {
            transform.position = new Vector2(transform.position.x - 0.1f, y);
        }*/
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
                    StartCoroutine(Down01());
                    break;
                }
                if (_count == 8)
                {
                    StartCoroutine(Attack());
                    break;
                }
                if (_count == 7)
                {
                    if (transform.position.x > 0)
                    {
                        StartCoroutine(Left());
                    }
                    else
                    {
                        StartCoroutine(Right());
                    }
                    break;
                }
            }

        }

        //下の方全体に突進
        IEnumerator Down01()
        {
            _isMove02 = true;

            //端に移動する
            if (transform.position.x < 0)
            {
                Rb.velocity = new Vector2(-3, 0);
                Debug.Log("a");
            }
            else
            {
                Rb.velocity = new Vector2(3, 0);
                Debug.Log("b");
            }

            yield return new WaitForSeconds(4);

            //端についたら下に移動する
            if (transform.position.x <= -7.5f)
            {
                Debug.Log("c");
                Rb.velocity = new Vector2(0, -3);

            }
            else if (transform.position.x >= 7.5f)
            {
                Debug.Log("d");
                Rb.velocity = new Vector2(0, -3);
            }

            yield return new WaitForSeconds(Random.Range(2f, 4f));

            //反対側に移動
            if (transform.position.x <= -7.5f)
            {
                Debug.Log("e");
                Rb.velocity = new Vector2(7, 0);
            }
            else if (transform.position.x >= -7.5f)
            {
                Debug.Log("f");
                Rb.velocity = new Vector2(-7, 0);
            }

            yield return new WaitForSeconds(4);

            //上に上がる
            Rb.velocity = new Vector2(0, 5);

            yield return new WaitForSeconds(3);

            //真ん中あたりに戻る
            if (transform.position.x < 0)
            {
                Rb.velocity = new Vector2(Random.Range(1, 6), 0);
                Debug.Log("a");
            }
            else
            {
                Rb.velocity = new Vector2(Random.Range(-7, 0), 0);
                Debug.Log("b");
            }

            yield return new WaitForSeconds(2.5f);

            Rb.velocity = new Vector2(0, 0);
            _isMove02 = false;
            StartCoroutine(RandomMovement());
        }

        //プレイヤーに突進
        IEnumerator Attack()//プレイヤーの位置によって左右のどちらかに移動するかを決める
        {
            Debug.Log("huuuu");
            Rb.velocity = new Vector2(0, 0);
            if (_player.transform.position.x >= transform.position.x)//プレイヤーが右にいたら
            {
                Debug.Log("right");
                _switch = true;
                Rb.velocity = new Vector2(4, 0);
            }
            else                                                     //左にいたら
            {
                Debug.Log("left");
                _switch = false;
                Rb.velocity = new Vector2(-4, 0);
            }

            while (true) //プレイヤーの辺りに着いたら
            {
                yield return new WaitForSeconds(0.1f);
                if (_switch && _player.transform.position.x <= transform.position.x)//右に移動したときに
                {
                    Rb.velocity = new Vector2(0, 0);
                    break;
                }
                else if (!_switch && _player.transform.position.x >= transform.position.x)//左に移動したときに
                {
                    Rb.velocity = new Vector2(0, 0);
                    break;
                }
            }

            yield return new WaitForSeconds(0.5f);

            while (true)//下に行く
            {
                yield return new WaitForSeconds(0.1f);
                Rb.velocity = new Vector2(0, -4);
                if (transform.position.y <= -3)//下まで来たら
                {
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(1); ;
                    Debug.Log("Up");
                    break;
                }
            }

            Rb.velocity = new Vector2(0, 3);

            while (true)//一定の場所まで上に上がる
            {
                yield return new WaitForSeconds(0.1f);
                if (3 <= transform.position.y)//一定の場所まできたら
                {
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(2);
                    StartCoroutine(RandomMovement());
                    break;
                }
            }
        }


        //ジグザク動く
        IEnumerator Left()
        {
            Debug.Log("Left");
            _isMove02 = true;
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (transform.position.x <= -7.5f)
                {
                    Debug.Log("nice!");
                    Rb.velocity = new Vector2(0, 0);
                    StartCoroutine(ThunderL());
                    break;
                }
                else//左に移動
                {
                    Debug.Log("maane");
                    Rb.velocity = new Vector2(-4, 0);
                }
            }
        }

        IEnumerator Right()
        {
            Debug.Log("Rignt");

            _isMove02 = true;
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (transform.position.x >= 7.5f)
                {
                    Debug.Log("nice!");
                    Rb.velocity = new Vector2(0, 0);
                    StartCoroutine(ThunderR());
                    break;
                }
                else//右に移動
                {
                    Debug.Log("maane");
                    Rb.velocity = new Vector2(4, 0);
                }
            }
        }
        IEnumerator ThunderL()
        {
            _thunder = 0;
            Debug.Log("dayone");
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (transform.position.x <= 7.5f)
                {
                    Rb.velocity = new Vector2(_xMove, _yMove);
                    if (_thunder >= 100)
                    {
                        _thunder = 0;
                        Rb.velocity = new Vector2(_xMove, _yMove);
                        _yMove *= -1;
                    }
                }
                else
                {
                    Rb.velocity = new Vector2(0, 0);
                    _isMove02 = false;
                    StartCoroutine(RandomMovement());
                    break;
                }
            }
        }

        IEnumerator ThunderR()
        {
            _thunder = 0;
            Debug.Log("dayone");
            while (true)
            {
                yield return new WaitForSeconds(0.1f);
                if (transform.position.x >= -7.5f)
                {
                    Rb.velocity = new Vector2(-_xMove, _yMove);
                    if (_thunder >= 100)
                    {
                        _thunder = 0;
                        Rb.velocity = new Vector2(-_xMove, _yMove);
                        _yMove *= -1;
                    }
                }
                else
                {
                    Rb.velocity = new Vector2(0, 0);
                    _isMove02 = false;
                    StartCoroutine(RandomMovement());
                    break;
                }
            }
        }
    }
}
