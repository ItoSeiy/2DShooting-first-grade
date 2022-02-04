using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement04 : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;

    bool _isMove02 = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("待機時間")] public float _stopTime = default;
    Vector2 _dir;
    int _count = 0;
    int _thunder = 0;
    float _xMove = 5f;
    float _yMove = 6f;
    private void Start()
    {
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
        _thunder++;
    }
    IEnumerator RandomMovement()
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
            else　　　　　　　　　　　　　　      //上下どっちにも移動しすぎてないなら
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
            else　　　　　　　　　　　　         //左右どっちにも移動しすぎてないなら
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
