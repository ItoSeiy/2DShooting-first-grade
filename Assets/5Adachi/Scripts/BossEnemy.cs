using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;
    [SerializeField, Header("BossEnemyの動き")] public Object[] _bossEnemyMove = default;
    private int _random = default;
    float _totalTime;
    [SerializeField,Header("BossEnemyのスピード")] float _speed = default;
    [SerializeField, Header("移動時間")] float _moveTime = default;
    [SerializeField, Header("停止時間")] float _stopTime = default;
    bool _isMove = false;
    //public int _appPos;// Random.Range(-1, 3);

    IEnumerator StationA_Move()
    {
        //transform.position = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(1f, 5f));
        Rb.velocity = new Vector2(0, -1f);
        yield return new WaitForSeconds(3);
        Rb.velocity = transform.up * 0;
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
            if (transform.position.x == Mathf.Clamp(transform.position.x, -1f, 1f))
            {
                Vector2 direction = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-8.0f, 8.0f));//(x, y);
                Rb.velocity = direction * _speed;
                yield return new WaitForSeconds(_moveTime);
                Rb.velocity = transform.up * 0;
                yield return new WaitForSeconds(_stopTime);
            }
            else if (transform.position.x < 1f)
            {
                Vector2 direction = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-8.0f, 8.0f));//(x, y);
                Rb.velocity = direction * _speed;
                yield return new WaitForSeconds(_moveTime);
                Rb.velocity = transform.up * 0;
                yield return new WaitForSeconds(_stopTime);
            }
        }

        
    }
    void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            StartCoroutine(StationA_Move());
        }



    protected override void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), Mathf.Clamp(transform.position.y,1f, 5f));
        base.Update();
        /*if (EnemyHp >= 0)
        {

            /*if (Timer == AttackInterval - 1)
            {
                /*Update();
                _random = Random.Range(0, 10);
                switch (_random)
                {
                    case 1:
                        Debug.Log("1");

                        break;

                    default:
                        Debug.Log("sry");

                        Debug.Log(b("aiueo"));
                        break;
                }
            }*/
        //}
    }
    
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }
    
    
    /*string b(string name)
    {
        return $"kakikukeko{name}";
    }*/

    /*private void Start()
    {
        _appPos = 11;// Random.Range(0, 2);
        Rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Move0(_appPos));
    }

    
    private void FixedUpdate()
    {
        Move(_appPos);
    }
    IEnumerator Move0(int n)
    {
        //transform.position = new Vector2(0f * n,7f); 
        Rb.velocity = new Vector2(0f * n, -1f);   
        //yield return new WaitForSeconds(5);                
        Rb.velocity = transform.up * 0;             
        yield return new WaitForSeconds(0.5f);             
        _isMove = true;
    }
    void Move(int n)
    {
        if (_isMove)
        {
            _totalTime += Time.deltaTime;
            float x = Mathf.Sin(_totalTime) * 1;
            float y = Mathf.Cos(_totalTime * 2)* 4;
            Rb.velocity = new Vector2(x, y);
        }
    }*/
}
