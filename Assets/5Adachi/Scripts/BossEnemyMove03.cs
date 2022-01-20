using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove03 : MonoBehaviour
{
    float _totalTime;
    Rigidbody2D _rb;
    public GameObject _player = GameObject.FindGameObjectWithTag("Player");
    public bool _move = false;
    int count2 = 0;
    public float x;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        
        StartCoroutine(Vertical());
    }

    IEnumerator Vertical()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        if (!_move)
        {
            _totalTime += Time.deltaTime;
            x = _player.transform.position.x;
            if (count2 == 0)
            {
                if (_player.transform.position.x >= transform.position.x)
                {
                    Debug.Log("a");
                    count2 = 1;
                    _rb.velocity = new Vector2(x, 0);

                }
                else// if (_player.transform.position.x <= transform.position.x)
                {
                    Debug.Log("i");
                    count2 = 2;
                    _rb.velocity = new Vector2(x, 0);
                }
            }

            if (count2 == 1)
            {
                if (_player.transform.position.x <= transform.position.x)
                {
                    _rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Down());
                }
            }

            if (count2 == 2)
            {
                if (_player.transform.position.x >= transform.position.x)
                {
                    Debug.Log(_move);
                    _rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Down());
                }
            }
        }

            IEnumerator Down()
            {
                if (!_move)
                {
                        float y = _player.transform.position.y;
                        _rb.velocity = new Vector2(0, y * 3);
                        yield return new WaitForSeconds(0.5f);

                    if (_player.transform.position.y >= transform.position.y)
                    {
                        _move = true;
                        Debug.Log(_move);
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(Up());
                    }
                    else if(transform.position.y <= -4)
                    {
                        _move = true;
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(Up());
                    }
                }

                IEnumerator Up()
                {
                    _rb.velocity = new Vector2(0, 3);
                    yield return new WaitForSeconds(3);
                    _rb.velocity = new Vector2(0, 0);
                    yield break;
                }
            }
        }
    }
