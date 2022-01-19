using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveCircle : MonoBehaviour
{
    float _totalTime;
    private int _speed = 3;
    private int _radius = 10;
    Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        _totalTime += Time.deltaTime;
        StartCoroutine("Circle");
    }

    IEnumerator Circle()
    {
        if(transform.position.y > 0)
        {
            _rb.velocity = new Vector2(0, 0);
            yield break; 
        }     
            float _x = _radius * Mathf.Sin(_totalTime * _speed);
            float _y = -_radius * Mathf.Cos(_totalTime * _speed);
            _rb.velocity = new Vector2(_x, _y);
            yield return new WaitForSeconds(5);
    }
}
