using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;

    int _random = default;
    Object _object;

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
    [SerializeField] public float _stopTime = default;

    //Move03
    private GameObject _player;
    public bool _isMove = true;
    int count2 = 0;
    [SerializeField] public string _playerTag = null;
    //public float x;

    void  RandomMove()
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
            StartCoroutine("Vertical");
        }
        else
        {
            Debug.Log("default");
            //StartCoroutine("StationA_Move");
            StartCoroutine("Vertical");
        }
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        //RandomMove();
        StartCoroutine("StationA_Move");
    }
    protected override void Update()
    {
        base.Update();
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        _totalTime += Time.deltaTime;
        //StartCoroutine("RandomMove");        
    }
    
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }

    /*IEnumerator Circle()
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
    }*/

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
            RandomMove();
        }
    }

    IEnumerator Vertical()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        if (_isMove)
        {
            x = _player.transform.position.x;
            if (count2 == 0)
            {
                if (_player.transform.position.x >= transform.position.x)
                {
                    Debug.Log("a");
                    count2 = 1;
                    Rb.velocity = new Vector2(x, 0);
                    yield return new WaitForSeconds(3);
                }
                else// if (_player.transform.position.x <= transform.position.x)
                {
                    Debug.Log("i");
                    count2 = 2;
                    Rb.velocity = new Vector2(x, 0);
                    yield return new WaitForSeconds(3);
                }
            }

            if (count2 == 1)
            {
                if (_player.transform.position.x <= transform.position.x)
                {
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Down());
                }
            }

            if (count2 == 2)
            {
                if (_player.transform.position.x >= transform.position.x)
                {
                    Debug.Log(_isMove);
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(Down());
                }
            }
        }

        IEnumerator Down()
        {
            if (_isMove)
            {
                float y = _player.transform.position.y;
                Rb.velocity = new Vector2(0, y * 3);
                yield return new WaitForSeconds(0.5f);

                if (_player.transform.position.y >= transform.position.y)
                {
                    _isMove = false;
                    Debug.Log(_isMove);
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(1);
                    StartCoroutine(Up());
                }
                else if (transform.position.y <= -4)
                {
                    _isMove = false;
                    Rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(1);
                    StartCoroutine(Up());
                }
            }

            IEnumerator Up()
            {
                Rb.velocity = new Vector2(0, 3);
                yield return new WaitForSeconds(3);
                Rb.velocity = new Vector2(0, 0);
                yield break;
            }
        }
    }
}
