using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveCircle : MonoBehaviour
{
    [SerializeField] float _resetTime = 0f;
    float _totalTime;
    float _totalTime2;
    private int _speed = 1;
    private int _radius = 10;
    Rigidbody2D _rb;

    public float _x;
    public float _y;
    public float y;
    public float y2;

    private int _countCircle = 0;

    bool _isMoveCircle = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
        if (_isMoveCircle)
        {
            _totalTime += Time.deltaTime;
            if (transform.position.x >= 0)
            {
                Circle(1);
            }
            else
            {
                Circle(-1);
            }            
        }
        
        Debug.Log(_totalTime);
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
    void Circle(int number)
    {       
        _x = _radius * 10 * Mathf.Sin(_totalTime * _speed * number);
        _y = -_radius * 10 * Mathf.Cos(_totalTime  * _speed);

        //Debug.Log(_rb.velocity);
        //Debug.Log();
        _rb.velocity = new Vector2(_x, _y);
        if (_countCircle >= 2)
        {
            _rb.velocity = new Vector2(0, 0);
            _totalTime2 = 0;
            _isMoveCircle = false;
        }
        else if (_totalTime >= _resetTime)
        {
            _rb.velocity = new Vector2(0, 0);
            _totalTime = 0;
            _countCircle += 1;
        }
        
    }
}
