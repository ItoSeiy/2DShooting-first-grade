using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackRandom : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;    
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
    /// <summary>必殺前に移動するポジション</summary>
    [SerializeField, Header("必殺前に移動するポジション")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>必殺前に移動するときのスピード</summary>
    [SerializeField, Header("必殺前に移動するときのスピード")] float _speed = 4f;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;    
    /// <summary>必殺技待機時間</summary>
    [SerializeField, Header("必殺技待機時間")] float _waitTime = 5f;
    /// <summary>必殺技発動時間</summary>
    [SerializeField, Header("必殺技発動時間")] float _activationTime = 30f;
    /// <summary>マズルの角度間隔</summary>
    [SerializeField, Header("マズルの角度間隔")] float _rotationInterval = 2f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType _bullet;
    /// <summary>1回の処理で弾を発射する回数</summary>
    [SerializeField,Header("1回の処理で弾を発射する回数")] int _maximumCount = 2;
    /// <summary>修正値</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>リセットタイマー</summary>
    const float RESET_TIME = 0f;
    /// <summary>最小の回転値</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>最大の回転値</summary>
    const float MAXIMUM_ROTATION_RANGE = 360f;
    /// <summary>1回の処理で弾を発射する回数の初期値</summary>
    const int INITIAL_COUNT = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//《スタート》でゲットコンポーネント
        StartCoroutine(Spiral()); //コルーチンを発動    
    }
    void Update()
    {
        _timer += Time.deltaTime;//タイマー
    }

    /// <summary>渦巻のような軌道、反時計回りに発射</summary>
    IEnumerator Spiral()
    {
        _timer = RESET_TIME;//タイムリセット

        //必殺を放つときはBOSSは放つ前にｘを0、Ｙを2をの位置(笑)に、移動する
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
            //横方向
            _horizontalDir = _superAttackPosition.x - transform.position.x;
            //縦方向
            _verticalDir = _superAttackPosition.y - transform.position.y;
            //横の範囲の条件式      
            _rightRange = transform.position.x < _superAttackPosition.x + PLAYER_POS_OFFSET;
            _leftRange = transform.position.x > _superAttackPosition.x - PLAYER_POS_OFFSET;
            //縦の範囲の条件式
            _upperRange = transform.position.y < _superAttackPosition.y + PLAYER_POS_OFFSET;
            _downRange = transform.position.y > _superAttackPosition.y - PLAYER_POS_OFFSET;
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
                transform.position = _superAttackPosition;//ボスの位置を修正
                break;//終わり
            }
        }
 
        _timer = 0f;//タイムリセット

        //必殺技発動
        while (true)
        {
            //同じ処理を数回(_maximumCount)繰り返す
            for (int count = INITIAL_COUNT; count < _maximumCount; count++)
            {
                ///マズルを回転する///
                Vector3 localAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得
                // ランダムな角度を設定（（　0度　～　360度/マズルの角度間隔　）* マズルの角度間隔　)
                localAngle.z = Random.Range(MINIMUM_ROTATION_RANGE,MAXIMUM_ROTATION_RANGE / _rotationInterval) * _rotationInterval;
                _muzzles[0].localEulerAngles = localAngle;//回転する
                //弾をマズルの向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;
            }

            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の調整

            //数秒経ったら
            if (_timer >= _activationTime)
            {
                break;//終了
            }
        }
        yield break;//終了
    }
}
