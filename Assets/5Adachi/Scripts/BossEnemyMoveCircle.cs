using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveCircle : MonoBehaviour
{
    float _totalTime;
    private int _speed = 3;
    private int _radius = 10;
    Rigidbody2D _rb;

    public float _x;
    public float _y;
    public float y;
    public float y2;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        _totalTime += Time.deltaTime;
        StartCoroutine("Circle");
    }

    /*IEnumerator Circle()
    {
        float _x = _radius * Mathf.Sin(_totalTime * _speed);
        float _y = -_radius * Mathf.Cos(_totalTime * _speed);
        if (transform.position.y <= 0)
        {
            _rb.velocity = new Vector2(_x, _y);
            yield return new WaitForSeconds(5);
        }
        
        if(transform.position.y > 0)
        {
            _rb.velocity = new Vector2(0, 0);
        }     
    }*/
    IEnumerator Circle()
    {
        
        _x = _radius * Mathf.Sin(_totalTime * _speed);
        _y = -_radius * Mathf.Cos(_totalTime * _speed);

        Debug.Log(_rb.velocity);
        //Debug.Log();
        _rb.velocity = new Vector2(_x, _y);
        yield return new WaitForSeconds(1);
        _rb.velocity = new Vector2(0, 0);
        yield break;
        /*if
        {
            Debug.Log("d");
            /*
        }*/
    }
}
