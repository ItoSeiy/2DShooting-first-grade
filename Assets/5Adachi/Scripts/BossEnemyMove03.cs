using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove03 : MonoBehaviour
{
    Rigidbody2D _rb;

    public bool _isMove = false;
    int _count = 0;
    public float x;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        _isMove = true;
    }

    private void Update()
    {
        StartCoroutine(Vertical());
    }

    IEnumerator Vertical()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        if (_isMove)
        {
            x = _player.transform.position.x;
            if (_count == 0)
            {
                if (_player.transform.position.x >= transform.position.x)
                {
                    Debug.Log("a");
                    _count = 1;
                    _rb.velocity = new Vector2(x, 0);
                    //yield return new WaitForSeconds();
                }
                else// if (_player.transform.position.x <= transform.position.x)
                {
                    Debug.Log("i");
                    _count = 2;
                    _rb.velocity = new Vector2(x, 0);
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
                    Debug.Log(_isMove);
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
                    float y = _player.transform.position.y;
                    _rb.velocity = new Vector2(0, y * 3);
                    yield return new WaitForSeconds(0.5f);

                    if (_player.transform.position.y >= transform.position.y)
                    {
                        _isMove = false;
                        Debug.Log(_isMove);
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(Up());
                    }
                    else if (transform.position.y <= -4)
                    {
                        _isMove = false;
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
                    //yield break;
                }
            }
        }
    
}
