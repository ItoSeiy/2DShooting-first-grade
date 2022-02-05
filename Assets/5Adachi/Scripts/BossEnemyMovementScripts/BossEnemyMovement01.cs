using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement01 : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;
    float x = 0;
    float y = 0;
    float _speed = 4f;
    [SerializeField,Header("待機時間")] public float _stopTime = default;
    Vector2 _dir;


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
        }
    }
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
}
