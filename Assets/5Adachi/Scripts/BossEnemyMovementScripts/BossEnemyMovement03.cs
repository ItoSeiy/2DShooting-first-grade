using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMovement03 : EnemyBese
{
    [SerializeField, Header("Bombのタグ")] public string _bombTag = null;

    //bool _isMove02 = false;
    private float x = 0;
    private float y = 0;
    float _speed = 4f;
    [SerializeField, Header("待機時間")] public float _stopTime = default;
    Vector2 _dir;
    int _count = 0;
    private GameObject _player;
    [SerializeField] private string _playerTag = null;
    int _switch = 0;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);
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
    }
    IEnumerator RandomMovement()
    {
        yield return new WaitForSeconds(0.5f);


        Rb.velocity = new Vector2(0, 0);


        while (true)
        {
            Rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(_stopTime);

            if (transform.position.y > 2.5f)      //上に移動しすぎたら
            {
                y = Random.Range(-1.0f, -0.1f);
            }
            else if (transform.position.y < 1.5f)//下に移動しすぎたら
            {
                y = Random.Range(0.1f, 1.0f);
            }
            else                                //上下どっちにも移動しすぎてないなら
            {
                y = Random.Range(-1.0f, 1.0f);
            }
            if (transform.position.x > 4)         //右に移動しすぎたら
            {
                x = (Random.Range(-3.0f, -1.0f));
            }
            else if (transform.position.x < -4)   //左に移動しぎたら
            {
                x = (Random.Range(1.0f, 3.0f));
            }
            else                                //左右どっちにも移動しすぎてないなら
            {
                x = Random.Range(-3.0f, 3.0f);
            }

            _dir = new Vector2(x, y);

            Rb.velocity = _dir * _speed;
            yield return new WaitForSeconds(0.5f);

            Debug.Log(x);
            Debug.Log(y);
            _count = Random.Range(0, 10);
            Debug.Log(_count);
            if (_count >= 1)
            {
                StartCoroutine("Rush");
                break;
            }
        }
    }
    IEnumerator Rush()//プレイヤーの位置によって左右のどちらかに移動するかを決める
    {
        Debug.Log("huuuu");
        if (_player.transform.position.x >= transform.position.x)//プレイヤーが右にいたら
        {
            Debug.Log("Right");
            _switch = 1;
            Rb.velocity = new Vector2(4, 0);
        }
        else                                                     //左にいたら
        {
            Debug.Log("Left");
            _switch = 2;
            Rb.velocity = new Vector2(-4, 0);
        }

        while (true) //プレイヤーの辺りに着いたら
        {
            yield return new WaitForSeconds(0.1f);
            if (_switch == 1 && _player.transform.position.x <= transform.position.x)//右に移動したときに
            {
                Debug.Log("Right2");
                Rb.velocity = new Vector2(0, 0);
                break;
            }
            else if (_switch == 2 && _player.transform.position.x >= transform.position.x)//左に移動したときに
            {
                Debug.Log("Left2");
                Rb.velocity = new Vector2(0, 0);
                break;
            }
        }

        _switch = 0;

        yield return new WaitForSeconds(0.5f);

        while (true)//下に行く
        {
            Debug.Log("Down");
            yield return new WaitForSeconds(0.1f);
            Rb.velocity = new Vector2(0, -4);
            if (transform.position.y <= -3)//下まで来たら
            {
                Rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(1); ;
                break;
            }
        }



        while (true)//一定の場所まで上に上がる
        {
            Rb.velocity = new Vector2(0, 3);
            Debug.Log("Up");
            yield return new WaitForSeconds(0.1f);
            if (3 <= transform.position.y)//一定の場所まできたら
            {
                Debug.Log("Up2");
                Rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(2);
                StartCoroutine("RandomMovement");
                break;
            }
        }

    }
}
