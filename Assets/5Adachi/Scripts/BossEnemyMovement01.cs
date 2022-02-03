using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement01 : EnemyBese
{
    [SerializeField, Header("Bomb‚Ìƒ^ƒO")] public string _bombTag = null;

    bool _isMove = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField,Header("‘Ò‹@ŽžŠÔ")] public float _stopTime = default;

    IEnumerator RandomMovement()
    {
        Rb.velocity = transform.up * 0;
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
            Vector2 dir = new Vector2(Random.Range(-3.0f, 3.0f) - x, Random.Range(-1.0f, 1.0f) - y);
            Rb.velocity = dir * _speed;
            yield return new WaitForSeconds(0.5f);
            Rb.velocity = transform.up * 0;
            yield return new WaitForSeconds(_stopTime);
            x += dir.x;
            y += dir.y;
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
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, 0f, 4.5f));
    }
}
