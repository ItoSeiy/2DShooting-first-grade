using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBese
{
    [SerializeField, Header("Bomb‚Ìƒ^ƒO")] public string _bombTag = null;
    [SerializeField, Header("BossEnemy‚Ì“®‚«")] public Object[] _bossEnemyMove = default;
    private int _random = default;

    float radius = 0.01f;

    /*private void Start()
    {
        speed = 5;
    }*/
    protected override void Update()
    {
        base.Update();
        if (EnemyHp >= 0)
        {
            if (Timer == AttackInterval - 1)
            {
                /*float x = transform.position.x + 0.1f * Mathf.Sin(Time.time * 5);
                transform.position = new Vector3(x, transform.position.y, transform.position.z);*/
                Update();
                _random = Random.Range(0, 10);
                switch (_random)
                {
                    case 1:
                        Debug.Log("1");

                        break;

                    default:
                        Debug.Log("sry");

                        Debug.Log(b("aiueo"));
                        break;
                }
            }
        }
    }
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }
    
    private void Update(float speed, float radius)
    {
        float x = transform.position.x + 1/*radius*/ * Mathf.Sin(Time.time * 5/*speed*/);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    string b(string name)
    {
        return $"kakikukeko{name}";
    }
}
