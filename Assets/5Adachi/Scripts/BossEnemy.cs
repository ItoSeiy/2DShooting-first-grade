using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;
    [SerializeField, Header("BossEnemyの軌道")] public Object[] _bossEnemyMove = default;
    [SerializeField,Header("BossEnemyのスピード")] float _speed = default;
    [SerializeField, Header("停止時間")] float _stopTime;
    bool _isMove = false;
    private float x = 0;
    private float y = 0;
    
    IEnumerator StationA_Move()
    {
        yield return new WaitForSeconds(2);
        Rb.velocity = transform.up * 0;
        yield return new WaitForSeconds(0.5f);
        _isMove = true;
        while (true)
        {
                Vector2 dir = new Vector2(Random.Range(-1.0f, 1.0f) - x, Random.Range(-1.0f, 1.0f) - y);
                Rb.velocity = dir * _speed;
                yield return new WaitForSeconds(Random.Range(0.1f,0.5f));
                Rb.velocity = transform.up * 0;
                yield return new WaitForSeconds(_stopTime);
                x += dir.x;
                y += dir.y;
        }

        
    }
    void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            StartCoroutine(StationA_Move());
        }



    protected override void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), Mathf.Clamp(transform.position.y,-1f, 5f));
        base.Update();
    }
    
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }
}
