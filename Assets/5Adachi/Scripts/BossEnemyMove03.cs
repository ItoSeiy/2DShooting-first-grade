using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove03 : MonoBehaviour
{
    float _totalTime;
    Rigidbody2D _rb;
    public GameObject _player = GameObject.FindGameObjectWithTag("Player");
    int count = 0;
    int count2 = 0;
    float x;

    private void Start()
    {
    }

    private void Update()
    {
        StartCoroutine(Vertical());
    }

    IEnumerator Vertical()
    {
        if (count != 2)
        {
            //if (_player.transform.position.x <= transform.position.x)
            //{
                //count2 = 1;
               float x = _player.transform.position.x;
            //}
            /*else if (_player.transform.position.x >= transform.position.x)
            {
                count2 = 2;
                x = -_player.transform.position.x;
            }*/

            //if (count2 == 1)
            //{
                _rb.velocity = new Vector2(x, 0);
                if (_player.transform.position.x >= transform.position.x)
                {
                    count = 1;
                    Debug.Log(count);
                }
                if (count == 1)
                {
                    _rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Down());
                }
            //}

            /*if (count2 == 2)
            {
                _rb.velocity = new Vector2(x, 0);
                if (x <= transform.position.x)
                {
                    count = 1;
                    Debug.Log(count);
                }
                if (count == 1)
                {
                    _rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Down());
                }
            }*/

            IEnumerator Down()
            {
                if (count != 2)
                {
                    float y = _player.transform.position.y;
                    _rb.velocity = new Vector2(0, y);
                    yield return new WaitForSeconds(0.5f);
                    if (_player.transform.position.y >= transform.position.y)
                    {
                        count = 2;
                        Debug.Log(count);
                    }
                    if (count == 2)
                    {
                        _rb.velocity = new Vector2(0, 0);
                        yield return new WaitForSeconds(1);
                        StartCoroutine(Up());
                    }
                }
                IEnumerator Up()
                {
                    _rb.velocity = new Vector2(0, 3);
                    yield return new WaitForSeconds(2);
                    _rb.velocity = new Vector2(0, 0);
                    yield break;
                }
            }
        }
    }
}
