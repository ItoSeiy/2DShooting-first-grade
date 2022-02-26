using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpecialAttack : EnemyBase
{
    /// <summary>バレットのプレハブ</summary>
    [SerializeField, Header("Bulletのプレハブ")] List<GameObject> _enemyBulletPrefab = new List<GameObject>();
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Rigidbody2D _muzzle = null;
    /// <summary>スプライト(スクライトじゃないよ)</summary>
    SpriteRenderer _sr;
    /// <summary>中央位置</summary>
    const float MIDDLE_POSITION = 0f;
    /// <summary>初期の攻撃割合</summary>
     float _initialDamageRatio;
    /// <summary>必殺前に移動するポジション</summary>
    [SerializeField, Header("必殺前に移動するポジション")] Transform _spAttackPos = null;
    /// <summary>時間</summary>
    float _time = 0f;
    /// <summary>HPバーの本数</summary>
    [SerializeField,Header("HPバーの数")]
    float _hPBar = 0;
    /// <summary>HPバー１本分のHP</summary>
    float _hP = 0f;
    /// <summary>HPバーのカウント</summary>
    float _hPBarCount = 0f;
    /// <summary>-1する</summary>
    const float _minus = -1f;
    /// <summary>右の範囲</summary>
    bool _rightRange;
    /// <summary>左の範囲</summary>
    bool _leftRange;
    /// <summary>上の範囲</summary>
    bool _upperRange;    
    /// <summary>下の範囲</summary>
    bool _downRange;
    /// <summary>必殺待機時間</summary>
    [SerializeField,Header("必殺技待機時間")]
    float _waitTime = 5f;
    /// <summary>修正値</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1/60f;

    /// <summary>水平、横方向</summary>
    private float _horizontal = 0f;
    /// <summary>垂直、縦方向</summary>
    private float _veritical = 0f;
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
        _hPBarCount = _hPBar;
        _hP = EnemyHp / _hPBar;
        Debug.Log(_hPBar);
        /*if (_muzzles == null || _muzzles.Length == 0)
        {
            _muzzles = new Transform[1] { this.transform };
        }*/
        //StartCoroutine(RandomMovement());
        //StartCoroutine(SpecialAttack());

        
    }
    protected override void Update()
    {
        base.Update();
        _time += Time.deltaTime;

        if (Rb.velocity.x > MIDDLE_POSITION)//右に移動したら
        {
            _sr.flipX = true;//右を向く
        }
        else if (Rb.velocity.x < MIDDLE_POSITION)//左に移動したら
        {
            _sr.flipX = false;//左を向く
        }
        //0の時、停止時は何も行わない（前の状態のまま）

        //ボスのHPがHPバー１本分のHPより少なくなったら
        if (EnemyHp <= _hP * (_hPBarCount + _minus))
        {
            Debug.Log("領域展開");
            StartCoroutine(SpecialAttack());            
            _hPBarCount--;           
        }
    }

    
    protected override void Attack()
    {
        //Quaternion.AngleAxis(_muzzle.rotation, Vector3.up);
        //Quaternion.AngleAxis(_muzzle.rotation, Vector3.forward);
        //(弾の種類,muzzleの位置,回転値)
        //Instantiate(_enemyBulletPrefab[0], _muzzle.position, Quaternion.AngleAxis(_muzzle.rotation, Vector3.forward));
        _muzzle.rotation += 10f;
        //(muzzleの位置,enum.弾の種類)
        ObjectPool.Instance.UseObject(_muzzle.position, PoolObjectType.Player01Power1);
    }

    IEnumerator SpecialAttack()
    {
        _time = 0f;//タイムリセット
        /*//横の範囲
        _horizontalRange = transform.position.x >= _spAttackPos.position.x - PLAYER_POS_OFFSET && transform.position.x <= _spAttackPos.position.x + PLAYER_POS_OFFSET;
        ///縦の範囲
        _verticalRange = transform.position.y <= _spAttackPos.position.y + PLAYER_POS_OFFSET && transform.position.y >= _spAttackPos.position.y - PLAYER_POS_OFFSET;*/

        //必殺を放つときはBOSSは放つ前にｘを0、Ｙを2をの位置(笑)に、移動する
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
            //行きたいポジションに移動する
            //近くなったら
            //if (_horizontalRange && _verticalRange)
            if(transform.position.x >= _spAttackPos.position.x - PLAYER_POS_OFFSET && transform.position.x <= _spAttackPos.position.x + PLAYER_POS_OFFSET && transform.position.y <= _spAttackPos.position.y + PLAYER_POS_OFFSET && transform.position.y >= _spAttackPos.position.y - PLAYER_POS_OFFSET)
            {
                Debug.Log("b");
                Rb.velocity = new Vector2((_spAttackPos.position.x - transform.position.x), (_spAttackPos.position.y - transform.position.y)) * Speed;
            }
            //遠かったら
            else
            {
                Debug.Log("a");
               Rb.velocity = new Vector2((_spAttackPos.position.x - transform.position.x), (_spAttackPos.position.y - transform.position.y)).normalized * Speed;
            }

            //数秒経ったら
            if (_time >= _waitTime)
            {
                Debug.Log("stop");
                Rb.velocity = Vector2.zero;
                transform.position = new Vector2(_spAttackPos.position.x, _spAttackPos.position.y);
                break;//終わる
            }
        }
        _initialDamageRatio = DamageTakenRation;//初期値を設定
        //AddDamageRatio = 0.5f;//必殺時は攻撃割合を変更

        /*while (true)
        {
            // 8秒毎に、間隔６度、速度１でmuzzleを中心として全方位弾発射予定
            for (int rad = 0; rad < 360; rad += 6)
            {
                _muzzle.rotation += 10f;
            }
            yield return new WaitForSeconds(8.0f);
            if(0 == 0)
            {
                break;
            }
        }*/

        //AddDamageRatio = _initialDamageRatio;//元に戻す
        yield break;
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
            Rb.velocity = _dir.normalized * Speed;
            yield return new WaitForSeconds(_moveTime);

            Debug.Log("x" + _horizontal);
            Debug.Log("y" + _veritical);
        }
    }
}