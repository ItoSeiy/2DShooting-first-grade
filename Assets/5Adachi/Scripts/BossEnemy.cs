using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBese
{
    [SerializeField, Header("BossEnemyの軌道")] public Object[]_bossEnemyMove = default;
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;

    int _random = default;
    Object _object;
    bool _isMove = false;

    //Cirle
    public float _x;
    public float _y;
    public float y2;
    private int _radius = 10;
    [SerializeField] public float _totalTime;
    [SerializeField] public int _speed = 3;

    //Random
    private float x = 0;
    private float y = 0;
    float _stopTime = 3;

    IEnumerator RandomMove()
    {
        /*switch (_random)
        {
            case 1:
                Debug.Log("1");
                StartCoroutine("Circle");
                yield return new WaitForSeconds(0);
                break;
            default:
                Debug.Log("default");
                StartCoroutine("StationA_Move");
                yield return new WaitForSeconds(0);
                break;
        }*/
        if(_random == 1)
        {
            Debug.Log("1");
            StartCoroutine("Circle");
            yield return new WaitForSeconds(0);
        }
        else
        {
            Debug.Log("default");
            StartCoroutine("StationA_Move");
            yield return new WaitForSeconds(0);
        }
    }

    void Start()
    {
        StartCoroutine("RandomMove");
    }
    protected override void Update()
    {
        base.Update();
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        _totalTime += Time.deltaTime;
        
    }
    
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }

    IEnumerator Circle()
    {
        Debug.Log("a");
        if (_random == 1)
        {
            Debug.Log("b");
            y = transform.position.y;
            _x = _radius * Mathf.Sin(_totalTime * _speed);
            _y = -_radius * Mathf.Cos(_totalTime * _speed);
            if (transform.position.y <= y)
            {
                Debug.Log("c");
                Rb.velocity = new Vector2(_x, _y);
                yield return new WaitForSeconds(0);
            }

            if (transform.position.y > y)
            {
                Debug.Log("d");
                Rb.velocity = new Vector2(0, 0);
                StartCoroutine("RandomMove");
            }
        }
    }

    IEnumerator StationA_Move()
    {
        if (_random == 0)
        {
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, 0f, 4.5f));
            Rb.velocity = transform.up * 0;
            yield return new WaitForSeconds(0.5f);


            Vector2 dir = new Vector2(Random.Range(-3.0f, 3.0f) - x, Random.Range(-1.0f, 1.0f) - y);
            Rb.velocity = dir * _speed;
            yield return new WaitForSeconds(0.5f);
            Rb.velocity = transform.up * 0;
            yield return new WaitForSeconds(_stopTime);
            x += dir.x;
            y += dir.y;
            Debug.Log(x);
            Debug.Log(y);
            Rb.velocity = new Vector2(0, 0);
            _random = Random.Range(0, 2);
            StartCoroutine("RandomMove");
        }
    }
}
