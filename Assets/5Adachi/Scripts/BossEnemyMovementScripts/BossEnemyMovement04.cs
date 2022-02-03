using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement04 : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;

    bool _isMove = false;
    bool _isMove02 = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("待機時間")] public float _stopTime = default;
    int _count = 0;
    int _thunder = 0;
    float _xMove = 5f;
    float _yMove = 6f;
    private void Start()
    {
        _isMove = true;
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
        if (_isMove == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, 0f, 4f));
        }
        if (_isMove02 == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        }
        _thunder++;
    }
    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
            Vector2 dir = new Vector2(Random.Range(-3.0f, 3.0f) - x, Random.Range(-1.0f, 1.0f) - y);
            Rb.velocity = dir * _speed;
            yield return new WaitForSeconds(0.5f);
            Rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);
            x += dir.x;
            y += dir.y;
            Debug.Log(x);
            Debug.Log(y);
            _count = Random.Range(0, 10);
            Debug.Log(_count);
            if (_count == 9)
            {
                _isMove = false;
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
