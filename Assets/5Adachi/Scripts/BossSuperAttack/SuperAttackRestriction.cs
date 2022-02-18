using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackRestriction: MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>必殺前に移動するときのスピード</summary>
    [SerializeField, Header("必殺前に移動するときのスピード")] float _speed = 4f;
    /// <summary>必殺前に移動するポジション</summary>
    [SerializeField, Header("必殺前に移動するポジション")] Transform _superAttackPos = null;
    /// <summary>初期の攻撃割合</summary>
    float _initialDamageRatio;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>右側の範囲</summary>
    bool _rightRange;
    /// <summary>左側の範囲</summary>
    bool _leftRange;
    /// <summary>上側の範囲</summary>
    bool _upperRange;
    /// <summary>下側の範囲</summary>
    bool _downRange;
    /// <summary>横方向</summary>
    float _horizontalDir = 0f;
    /// <summary>縦方向</summary>
    float _verticalDir = 0f;
    float a = 0f;
    /// <summary>必殺技待機時間</summary>
    [SerializeField, Header("必殺技待機時間")] float _waitTime = 5f;
    /// <summary>必殺技発動時間</summary>
    [SerializeField, Header("必殺技発動時間")] float _activationTime = 30f;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.6f;
    /// <summary>修正値</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>リセットタイマー</summary>
    const float RESET_TIME = 0f;
    /// <summary>修正値</summary>
    const float MUZZLE_ROT_OFFSET = 4.9f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//《スタート》でゲットコンポーネント
        StartCoroutine(Restriction()); //コルーチンを発動    
    }
    void Update()
    {
        _timer += Time.deltaTime;//タイマー
    }

    /// <summary>Firework×Windmillのような軌道、必殺技残り時間が半分を切ると逆回転になる
    /// <para>Firework＝花火のような軌道,全方位に発射</para>
    /// <para>Windmill＝渦巻のような軌道,反時計回りに発射</para></summary>
    IEnumerator Restriction()
    {
        _timer = RESET_TIME;//タイムリセット

        //必殺を放つときはBOSSは放つ前にｘを0、Ｙを2をの位置(笑)に、移動する
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
            //横方向
            _horizontalDir = _superAttackPos.position.x - transform.position.x;
            //縦方向
            _verticalDir = _superAttackPos.position.y - transform.position.y;
            //横の範囲の条件式      
            _rightRange = transform.position.x < _superAttackPos.position.x + PLAYER_POS_OFFSET;
            _leftRange = transform.position.x > _superAttackPos.position.x - PLAYER_POS_OFFSET;
            //縦の範囲の条件式
            _upperRange = transform.position.y < _superAttackPos.position.y + PLAYER_POS_OFFSET;
            _downRange = transform.position.y > _superAttackPos.position.y - PLAYER_POS_OFFSET;
            //行きたいポジションに移動する
            //近かったら
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("結果は" + _rightRange + _leftRange + _upperRange + _downRange);
                //スムーズに移動
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //遠かったら
            else
            {
                Debug.Log("結果は" + _rightRange + _leftRange + _upperRange + _downRange);
                //安定して移動
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //数秒経ったら
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                _rb.velocity = Vector2.zero;//停止
                transform.position = _superAttackPos.position;//ボスの位置を修正
                break;//終わり
            }
        }
        //_initialDamageRatio = AddDamageRatio;//初期値を設定
        //AddDamageRatio = 0.5f;//必殺時は攻撃割合を変更
        _timer = RESET_TIME;//タイムリセット

        //必殺技発動
        while (true)
        {
            //必殺技発動時間の後半になったら反時計回りに全方位発射
            if (_timer >= _activationTime / 2f)
            {
                //360度全方位に発射
                for (float angle = 0 + a + MUZZLE_ROT_OFFSET; angle <= 360f + a + MUZZLE_ROT_OFFSET; angle += 10f)
                {
                    Vector3 localAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得
                    localAngle.z = -angle;// 角度を設定
                    _muzzles[0].localEulerAngles = localAngle;//回転する
                    //弾を発射（仮でBombにしてます）
                    var bossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
                    //弾をマズルの向きに合わせる
                    bossEnemyBullet.transform.rotation = _muzzles[0].rotation;
                }

            }
            //必殺技発動時間の前半までは反時計回りに全方位発射
            else
            {
                //360度全方位に発射
                for (float angle = 0 + a; angle <= 360f + a; angle += 10)
                {
                    Vector3 localAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得
                    localAngle.z = angle;// 角度を設定
                    _muzzles[0].localEulerAngles = localAngle;//回転する
                                                              //弾を発射（仮でBombにしてます）
                    var bossEnemyBullet = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
                    //弾をマズルの向きに合わせる
                    bossEnemyBullet.transform.rotation = _muzzles[0].rotation;
                }
            }
            a++;

            yield return new WaitForSeconds(_attackInterval);//攻撃頻度(秒)
            //数秒経ったら
            if (_timer >= _activationTime)
            {
                /*localAngle.z = ZERO_DEGREE_ANGLE;// 角度を0度に設定
                _muzzles[0].localEulerAngles = localAngle;//停止*/
                break;//終了
            }
        }

        //AddDamageRatio = _initialDamageRatio;//攻撃割合を元に戻す
        yield break;//終了
    }
}
