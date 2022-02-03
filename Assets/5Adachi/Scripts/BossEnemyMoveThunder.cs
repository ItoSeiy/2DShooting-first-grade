using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveThunder : MonoBehaviour
{
    Rigidbody2D _rb;
    int _thunder = 0;
     float _xMove = 5f;
     float _yMove = 6f;
    [SerializeField,Header("スピード")] float _speed;

    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Left());
    }
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), (Mathf.Clamp(transform.position.y, -4f, 4f)));
        _thunder++;
    }
    IEnumerator Left()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x <= -7.5f)
            {
                Debug.Log("nice!");
                _rb.velocity = new Vector2(0, 0);
                StartCoroutine(Thunder());
                break;
            }
            else//左に移動
            {
                Debug.Log("maane");
                _rb.velocity = new Vector2(-4, 0);
            }
        }
    }
    IEnumerator Thunder()
    {
        _thunder = 0;
        Debug.Log("dayone");
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x <= 7.5f)
            {
                _rb.velocity = new Vector2(_xMove, _yMove);
                if (_thunder >= 100)
                {
                    _thunder = 0;
                    _rb.velocity = new Vector2(_xMove, _yMove);
                    _yMove *= -1;
                }
            }
            else
            {
                _rb.velocity = new Vector2(0, 0);
                _xMove *= -1;
            }
        }
    }
}
