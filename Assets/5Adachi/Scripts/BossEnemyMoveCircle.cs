using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveCircle : MonoBehaviour
{
    [SerializeField,Header("軌道が変わるタイミング")] float _resetTime = 0f;
    [SerializeField,Header("軌道が変わる回数")] float _count = 2;
    [SerializeField,Header("スピード")]private int _speed = 2;
    [SerializeField,Header("円の半径")] private float _radius = 5f;
    Rigidbody2D _rb;
    float _totalTime;
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
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -4f, 4f));
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

    void Circle(int sign)
    {       
        _x = _radius * Mathf.Sin(_totalTime * _speed * sign);
        _y = -_radius * Mathf.Cos(_totalTime  * _speed);

        //Debug.Log(_rb.velocity);
        //Debug.Log();
        _rb.velocity = new Vector2(_x, _y);
        if (_countCircle >= _count)
        {
            _rb.velocity = new Vector2(0, 0);
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
