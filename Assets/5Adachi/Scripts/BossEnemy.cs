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
    private int _radius = 10;
    float _totalTime;
    private int _speed = 3;

    IEnumerator RandomMove()
    {
        _isMove = true;
        while (_isMove)
        {
            _random = Random.Range(0, 2);
            switch (_random)
            {
                /*case 1:
                    Debug.Log("1");
                    // _bossEnemyMove[0];
                    yield return new WaitForSeconds(5);
                    break;*/
                default:
                    Debug.Log("default");
                    //_bossEnemyMove[1];
                    StartCoroutine("Circle");
                    yield return new WaitForSeconds(0);
                    break;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        StartCoroutine("RandomMove");
        _totalTime += Time.deltaTime;
        //StartCoroutine("Circle");
    }
    
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }

    IEnumerator Circle()
    {
        if (transform.position.y > 0)
        {
            Rb.velocity = new Vector2(0, 0);
            //yield break;
            StartCoroutine("RandomMove");
        }
        float _x = _radius * Mathf.Sin(_totalTime * _speed);
        float _y = -_radius * Mathf.Cos(_totalTime * _speed);
        Rb.velocity = new Vector2(_x, _y);
        yield return new WaitForSeconds(5);
    }
}
