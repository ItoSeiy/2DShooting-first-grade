using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement02 : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("待機時間")] public float _stopTime = default;
    int _count = 0;
    [SerializeField] Object attack = null; 
    Vector2 _dir;

    
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
                StartCoroutine(Down());
                //attack.Down();
                break;
            }
        }
    }

    IEnumerator Down()
    {
        if (transform.position.x < 0)//左にいたら
        {
            Rb.velocity = new Vector2(-7, 0);
            Debug.Log("a");
        }
        else                         //右にいたら
        {
            Rb.velocity = new Vector2(7, 0);
            Debug.Log("b");
        }

        while (true)//端に着くまで下がる
        {
            yield return new WaitForSeconds(0.1f);
            if (transform.position.x <= -7.5f)
            {
                Debug.Log("c");
                Rb.velocity = new Vector2(0, -3);
                break;
            }
            else if (transform.position.x >= 7.5f)
            {
                Debug.Log("d");
                Rb.velocity = new Vector2(0, -3);
                break;
            }
        }


        while (true)//反対側に着くまで移動する
        {
            yield return new WaitForSeconds(0.1f);

            if (transform.position.y <= -3 && transform.position.x <= -7.5f)
            {
                Debug.Log("e");
                Rb.velocity = new Vector2(7, 0);
                break;
            }
            else if (transform.position.y <= -3 && transform.position.x >= 7.5f)
            {
                Debug.Log("f");
                Rb.velocity = new Vector2(-7, 0);
                break;
            }
        }

        yield return new WaitForSeconds(2);

        while (true)//上に上がる
        {
            Debug.Log("g");

            yield return new WaitForSeconds(0.1f);
            Rb.velocity = new Vector2(0, 5);

            if (transform.position.y >= 3.5f)
            {
                Debug.Log("h");
                break;
            }

        }

        while (true)//いい感じの位置に
        {
            if (transform.position.x < 0)
            {
                Rb.velocity = new Vector2(3, 0);
                Debug.Log("a");
                break;
            }
            else
            {
                Rb.velocity = new Vector2(-3, 0);
                Debug.Log("b");
                break;
            }
        }
        yield return new WaitForSeconds(Random.Range(1, 3));
        Rb.velocity = new Vector2(0, 0);
        StartCoroutine(RandomMovement());
    }
}
