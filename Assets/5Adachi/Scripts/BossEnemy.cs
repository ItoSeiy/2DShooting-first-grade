using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBese
{
    [SerializeField, Header("Bomb‚Ìƒ^ƒO")] public string _bombTag = null;
    [SerializeField, Header("BossEnemy‚Ì“®‚«")] public Object[] _bossEnemyMove = default;
    private int _random = default;
    float _totalTime;
    bool _isMove;
    Rigidbody2D _rb;
    public int _appPos;// Random.Range(-1, 3);

    /*private void Start()
    {
        speed = 5;
    }*/



    protected override void Update()
    {
        base.Update();
        if (EnemyHp >= 0)
        {

            /*if (Timer == AttackInterval - 1)
            {
                /*Update();
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
            }*/
        }
    }
    
    protected override void OnGetDamage()
    {

    }
    protected override void Attack()
    {

    }
    
    
    /*string b(string name)
    {
        return $"kakikukeko{name}";
    }*/

    private void Start()
    {
        _appPos = -1;// Random.Range(0, 2);
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Move0(_appPos));
    }

    
    private void FixedUpdate()
    {
        Move(_appPos);
    }
    IEnumerator Move0(int n)
    {
        transform.position = new Vector2(18f * n,10f); 
        _rb.velocity = new Vector2(-3f * n, -1f);   
        yield return new WaitForSeconds(5);                
        _rb.velocity = transform.up * 0;             
        yield return new WaitForSeconds(0.5f);             
        _isMove = true;
    }
    void Move(int n)
    {
        if (_isMove)
        {
            _totalTime += Time.deltaTime;
            float x = Mathf.Sin(_totalTime) * 7 * n;
            float y = Mathf.Cos(_totalTime * 2) * 10;
            _rb.velocity = new Vector2(x, y);
        }
    }
}
