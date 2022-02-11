using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialAttack : EnemyBese
{
    /// <summary>バレットのプレハブ</summary>
    [SerializeField, Header("Bulletのプレハブ")] List<GameObject> _enemyBulletPrefab = new List<GameObject>();
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform _muzzles = null;
    /// <summary>スプライト(スクライトじゃないよ)</summary>
    SpriteRenderer _sr;
    /// <summary>中央位置</summary>
    const float MIDDLE_POSITION = 0f;


    /// <summary>水平、横方向</summary>
    private float _horizontal = 0f;
    /// <summary>垂直、縦方向</summary>
    private float _veritical = 0f;
    /// <summary>速度</summary>
    [SerializeField, Header("スピード")] float _speed = 4f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>移動時間</summary>
    [SerializeField, Header("移動時間")] float _moveTime = 0.5f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 4f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -4f;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 2.5f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = 1.5f;
    /// <summary>方向</summary>
    Vector2 _dir;
    /// <summary>左方向</summary>
    const float LEFT_DIR = -1f;
    /// <summary>右方向</summary>
    const float RIGHT_DIR = 1f;
    /// <summary>上方向</summary>
    const float UP_DIR = 1f;
    /// <summary>下方向</summary>
    const float DOWN_DIR = -1;
    /// <summary>方向なし</summary>
    const float NO_DIR = 0f;


    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        /*if (_muzzles == null || _muzzles.Length == 0)
        {
            _muzzles = new Transform[1] { this.transform };
        }*/
        StartCoroutine(RandomMovement());
    }
    protected override void Update()
    {
        base.Update();
        if (Rb.velocity.x > MIDDLE_POSITION)//右に移動したら
        {
            _sr.flipX = true;//右を向く
        }
        else if (Rb.velocity.x < MIDDLE_POSITION)//左に移動したら
        {
            _sr.flipX = false;//左を向く
        }
        //0の時、停止時は何も行わない（前の状態のまま）
    }

    protected override void Attack()
    {
        //(弾の種類,muzzleの位置,良くわからん奴)
        //Instantiate(_enemyBulletPrefab[0], _muzzles.position, Quaternion.identity);
        //(muzzleの位置,enum.弾の種類)
        ObjectPool.Instance.UseBullet(_muzzles.position, PoolObjectType.Player01Power1);
    }

    /// <summary>作業中だからマジックナンバーについては何も言うなよ神原</summary>
    IEnumerator SpecialAttack()
    {

        int count = 0;
        while (true)
        {
            // 8秒毎に、間隔６度、速度１でmuzzleを中心として全方位弾発射。
            for (float rad = 0f; rad < 360f; rad += 6f)
            {
                //TestSpecialAttack.Add(transform.position.x, transform.position.y, rad, 1);
                //Instantiate(_enemyBulletPrefab[0] , _muzzles.position * rad, Quaternion.identity);
            }
            yield return new WaitForSeconds(8.0f);
            count++;
            
        }
    }


    protected override void OnGetDamage()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// ランダム方向に動く
    /// </summary>
    IEnumerator RandomMovement()
    {
        while (true)
        {
            //一定時間止まる
            Rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);

            //場所によって移動できる左右方向を制限する
            if (transform.position.x > _rightLimit)         //右に移動しすぎたら
            {
                _horizontal = Random.Range(LEFT_DIR, NO_DIR);//左移動可能
            }
            else if (transform.position.x < _leftLimit)   //左に移動しぎたら
            {
                _horizontal = Random.Range(NO_DIR, RIGHT_DIR);//右移動可能
            }
            else　　　　　　　　　　　　         //左右どっちにも移動しすぎてないなら
            {
                _horizontal = Random.Range(LEFT_DIR, RIGHT_DIR);//自由に左右移動可能          
            }

            //場所によって移動できる上下方向を制限する
            if (transform.position.y > _upperLimit)      //上に移動しすぎたら
            {
                _veritical = Random.Range(DOWN_DIR, NO_DIR);//下移動可能
            }
            else if (transform.position.y < _lowerLimit)//下に移動しすぎたら
            {
                _veritical = Random.Range(NO_DIR, UP_DIR);//上移動可能
            }
            else　　　　　　　　　　　　　　      //上下どっちにも移動しすぎてないなら
            {
                _veritical = Random.Range(DOWN_DIR, UP_DIR);//自由に上下移動可能
            }

            _dir = new Vector2(_horizontal, _veritical);//ランダムに移動
            //一定時間移動する
            Rb.velocity = _dir.normalized * _speed;
            yield return new WaitForSeconds(_moveTime);

            Debug.Log("x" + _horizontal);
            Debug.Log("y" + _veritical);
        }
    }
}