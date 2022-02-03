using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement02 : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;

    bool _isMove = false;
    bool _isMove02 = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("待機時間")] public float _stopTime = default;
    int _count = 0;

    
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
        if (_isMove == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, 0f, 4f));
        }
        if(_isMove02 == true)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        }
        //int count = Random.Range(0, 2);
    }
    IEnumerator RandomMovement()
    {
        //Rb.velocity = transform.up * 0;
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
            if(_count == 9)
            {              
                _isMove = false;
                StartCoroutine(Down());
                break;
            }
        }
        
    }
    IEnumerator Down()
    {
        //int count = 0;
        //count++;
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
            Rb.velocity = new Vector2(0,-3);
        }

        yield return new WaitForSeconds(Random.Range(2f,4f));

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
            Rb.velocity = new Vector2(Random.Range(1,6), 0);
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
        _count = 0;
        StartCoroutine(RandomMovement());
    }
}
