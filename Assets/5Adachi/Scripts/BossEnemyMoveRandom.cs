using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : MonoBehaviour
{
    bool _isMove = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField] public float _stopTime = 2;
    Rigidbody2D _rb;
    Vector2 _dir;

    IEnumerator StationA_Move()
    {       
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
            _rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);

            if(transform.position.y > 2.5f)      //上に移動しすぎたら
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
            else if(transform.position.x < -4)   //左に移動しぎたら
            {
                x = (Random.Range(1.0f, 3.0f));
            }
            else　　　　　　　　　　　　         //左右どっちにも移動しすぎてないなら
            {
                x = Random.Range(-3.0f, 3.0f);               
            }

            _dir = new Vector2(x, y);

            _rb.velocity = _dir * _speed;
            yield return new WaitForSeconds(0.5f);
            
            Debug.Log(x);
            Debug.Log(y);
        }
    }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StationA_Move());
    }
    private void Update()
    {

    }
}
