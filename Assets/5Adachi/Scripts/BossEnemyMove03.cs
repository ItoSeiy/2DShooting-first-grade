using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove03 : MonoBehaviour
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
    }

    private void Update()
    {
        StartCoroutine(Vertical());
    }

    IEnumerator Vertical()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        {
            //Debug.Log("e");
            if (_isMove)
            {
                _x = _player.transform.position.x;
                if (_count == 0)
                {
                    if (_player.transform.position.x >= transform.position.x)
                    {
                        //Debug.Log("a");
                        _count = 1;
                        _rb.velocity = new Vector2(_x, 0);
                        //yield return new WaitForSeconds();
                    }
                    else// if (_player.transform.position.x <= transform.position.x)
                    {
                        //Debug.Log("i");
                        _count = 2;
                        _rb.velocity = new Vector2(_x, 0);
                        //yield return new WaitForSeconds();
                    }
                }


                if (_count == 1)
                {
                    if (_player.transform.position.x <= transform.position.x)
                    {
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(Down());
                    }
                }

                if (_count == 2)
                {
                    if (_player.transform.position.x >= transform.position.x)
                    {
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(Down());

                    }
                }
            }
            IEnumerator Down()
            {
                if (_isMove)
                {
                    _y = _player.transform.position.y;
                    _rb.velocity = new Vector2(0, _y * 3);
                    yield return new WaitForSeconds(0.5f);

                    if (_player.transform.position.y >= transform.position.y)
                    {
                        //_isMove = false;
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(Up());
                    }
                    else if (transform.position.y <= -4)
                    {
                        //_isMove = false;
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(Up());
                    }
                }
                IEnumerator Up()
                {
                    _isMove = false;
                    _rb.velocity = new Vector2(0, 3);
                    yield return new WaitForSeconds(3);
                    _rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(2);

                    if(transform.position.x >= 0)
                    {
                        _rb.velocity = new Vector2(-5, 0);
                        yield return new WaitForSeconds(1);
                        if(transform.position.x <= 0)
                        {
                            _rb.velocity = Vector2.zero;
                            yield return new WaitForSeconds(20);
                        }
                    }
                    else if(transform.position.x < 0)
                    {
                        _rb.velocity = new Vector2(5, 0);
                        yield return new WaitForSeconds(1);
                        if (transform.position.x >= 0)
                        {
                            _rb.velocity = Vector2.zero;
                            yield return new WaitForSeconds(20);
                        }
                    }
                    
                    //StartCoroutine(Vertical2());

                    IEnumerator Vertical2()
                    {
                        {
                            //Debug.Log("e");
                            if (_isMove2)
                            {                      
                            _x2 = _player.transform.position.x;
                            //Debug.Log(_x2);
                                if (_count2 == 0)
                                {
                                Debug.Log("haahahaha");
                                if (_player.transform.position.x >= transform.position.x)
                                    {
                                        Debug.Log("a");
                                        _count2 = 1;
                                        _rb.velocity = new Vector2(_x2, 0);
                                        //yield return new WaitForSeconds();
                                    }
                                    else if (_player.transform.position.x <= transform.position.x)
                                    {
                                        Debug.Log("i");
                                        _count2 = 2;
                                        _rb.velocity = new Vector2(_x2, 0);
                                        //yield return new WaitForSeconds();
                                    }
                                }

                                //if (_count == 1)
                                //{
                                    //Debug.Log("_count" + _count);
                                    if (_count2 == 1)
                                    {
                                        Debug.Log("_count2" + _count2);
                                        Debug.Log("_player.transform.position.x " + _player.transform.position.x);
                                        Debug.Log("transform.position.x" + transform.position.x);
                                        if (_player.transform.position.x <= transform.position.x)
                                        {
                                            Debug.Log("aaaaaaaaaaaaa");
                                            _rb.velocity = new Vector2(0, 0);
                                            yield return new WaitForSeconds(0.5f);
                                            StartCoroutine(Down2());
                                        }
                                    }

                                    if (_count2 == 2)
                                    {
                                        Debug.Log("_count2" + _count2);
                                        Debug.Log("_player.transform.position.x " + _player.transform.position.x);
                                        Debug.Log("transform.position.x" + transform.position.x);
                                        if (-_player.transform.position.x <= transform.position.x)
                                        {
                                            Debug.Log("bbbbbbbbbbbbbb");
                                            _rb.velocity = new Vector2(0, 0);
                                            yield return new WaitForSeconds(0.5f);
                                            StartCoroutine(Down2());
                                        }
                                    }
                                //}

                                /*if (_count == 2)
                                {
                                    Debug.Log("_count" + _count);
                                    if (_count2 == 1)
                                    {
                                        Debug.Log("_count2" + _count2);
                                        Debug.Log("_player.transform.position.x " + _player.transform.position.x);
                                        Debug.Log("transform.position.x" + transform.position.x);
                                        if (-_player.transform.position.x >= -transform.position.x)
                                        {
                                            Debug.Log("cccccccccccccccc");
                                            _rb.velocity = new Vector2(0, 0);
                                            yield return new WaitForSeconds(0.5f);
                                            StartCoroutine(Down2());
                                        }
                                    }

                                    if (_count2 == 2)
                                    {
                                        Debug.Log("_count2" + _count2);
                                        Debug.Log("_player.transform.position.x " + _player.transform.position.x);
                                        Debug.Log("transform.position.x" + transform.position.x);
                                        if (_player.transform.position.x >= transform.position.x)
                                        {
                                            Debug.Log("ddddddddddddddddd");
                                            _rb.velocity = new Vector2(0, 0);
                                            yield return new WaitForSeconds(0.5f);
                                            StartCoroutine(Down2());
                                        }
                                    }
                                }*/
                            }
                            IEnumerator Down2()
                            {
                                if (_isMove2)
                                {
                                    _y2 = _player.transform.position.y;
                                    _rb.velocity = new Vector2(0, _y2 * 3);
                                    yield return new WaitForSeconds(0.5f);

                                    if (_player.transform.position.y >= transform.position.y)
                                    {
                                        //_isMove = false;
                                        _rb.velocity = new Vector2(0, 0);
                                        yield return new WaitForSeconds(1);
                                        StartCoroutine(Up2());
                                    }
                                    else if (transform.position.y <= -4)
                                    {
                                        //_isMove = false;
                                        _rb.velocity = new Vector2(0, 0);
                                        yield return new WaitForSeconds(1);
                                        StartCoroutine(Up2());
                                    }
                                }
                                IEnumerator Up2()
                                {
                                    _isMove2 = false;
                                    _rb.velocity = new Vector2(0, 3);
                                    yield return new WaitForSeconds(3);
                                    _rb.velocity = new Vector2(0, 0);
                                    yield return new WaitForSeconds(2);
                                    //StartCoroutine(Vertical());
                                    yield break;
                                    //Debug.Log("are");
                                }
                            }
                        }
                    }                  
                }
                
            }
        }
    }
}
