using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveRandom : MonoBehaviour
{
    bool _isMove = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    float _stopTime = 3;
    Rigidbody2D _rb;

    IEnumerator StationA_Move()
    {       
        _rb.velocity = transform.up * 0;
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
            Vector2 dir = new Vector2(Random.Range(-3.0f, 3.0f) - x, Random.Range(-1.0f, 1.0f) - y);
            _rb.velocity = dir * _speed;
            yield return new WaitForSeconds(0.5f);
            _rb.velocity = transform.up * 0;
            yield return new WaitForSeconds(_stopTime);
            x += dir.x;
            y += dir.y;
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
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, 0f, 4.5f));
    }
}
