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
    Rigidbody2D _rb;


    void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            StartCoroutine(RandomMove());
        }

    IEnumerator RandomMove()
    {
        _isMove = true;
        while (_isMove)
        {
            _random = Random.Range(0, 2);
            switch (_random)
            {
                case 1:
                    Debug.Log("1");
                    _object = _bossEnemyMove[0];
                    yield return new WaitForSeconds(5);
                    break;
                default:
                    Debug.Log("default");
                    _object = _bossEnemyMove[1];
                    yield return new WaitForSeconds(5);
                    break;
            }
        }
    }

    protected override void Update()
    {
        
    }
    
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }
}
