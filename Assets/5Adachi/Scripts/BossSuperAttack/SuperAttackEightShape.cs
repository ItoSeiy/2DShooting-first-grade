using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackEightShape : BossAttackAction
{
    /// <summary>このスクリプトで使うタイマー</summary>
    float _defaultTimer = 0f;
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
    /// <summary>弾の見た目を変える間隔(秒)の修正値</summary>
    float _switchIntervalOffset = 0f;
    /// <summary>弾の見た目の種類</summary>
    int _firstPattern = 0;
    /// <summary>弾の見た目の種類</summary>
    int _secondPattern = 0;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>必殺前に移動するポジション</summary>
    [SerializeField, Header("必殺前に移動するポジション")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>必殺前に移動するときのスピード</summary>
    [SerializeField, Header("必殺前に移動するときのスピード")] float _speed = 4f;    
    /// <summary>必殺技待機時間</summary>
    [SerializeField, Header("必殺技待機時間")] float _waitTime = 5f;
    /// <summary>必殺技発動時間</summary>
    [SerializeField, Header("必殺技発動時間")] float _activationTime = 30f;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.6f;
    /// <summary>マズルの角度間隔</summary>
    [SerializeField, Header("マズルの角度間隔")] float _rotationInterval = 10f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _bullet;
    /// <summary>弾の見た目を変える間隔(秒)</summary>
    [SerializeField,Header("弾の見た目を変える間隔(秒)")] float _switchInterval = 2f;
    /// <summary>この行動から出る時間</summary>
    [SerializeField, Header("この行動から出る時間")] float _endingTime = 30f;
    /// <summary>修正値</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>リセットタイマー</summary>
    const float RESET_TIME = 0f;

    public override System.Action ActinoEnd { get; set; }

    
    public override void Enter(BossController contlloer)
    {
        StartCoroutine(EightShape(contlloer)); //コルーチンを発動
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _defaultTimer += Time.deltaTime;//タイマー
        _timer += Time.deltaTime;

        if(_timer >= _endingTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        StopAllCoroutines();
    }

    /// <summary>8の字のような軌道</summary>
    IEnumerator EightShape(BossController controller)
    {
        _defaultTimer = RESET_TIME;//タイムリセット

        //必殺を放つときはBOSSは放つ前にｘを0、Ｙを2をの位置(笑)に、移動する
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
            //横方向
            _horizontalDir = _superAttackPosition.x - controller.transform.position.x;
            //縦方向
            _verticalDir = _superAttackPosition.y - controller.transform.position.y;
            //横の範囲の条件式      
            _rightRange = controller.transform.position.x < _superAttackPosition.x + PLAYER_POS_OFFSET;
            _leftRange = controller.transform.position.x > _superAttackPosition.x - PLAYER_POS_OFFSET;
            //縦の範囲の条件式
            _upperRange = controller.transform.position.y < _superAttackPosition.y + PLAYER_POS_OFFSET;
            _downRange = controller.transform.position.y > _superAttackPosition.y - PLAYER_POS_OFFSET;
            //行きたいポジションに移動する
            //近かったら
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("結果は" + _rightRange + _leftRange + _upperRange + _downRange);
                //スムーズに移動
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //遠かったら
            else
            {
                Debug.Log("結果は" + _rightRange + _leftRange + _upperRange + _downRange);
                //安定して移動
                controller.Rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //数秒経ったら
            if (_defaultTimer >= _waitTime)
            {
                Debug.Log("stop");
                controller.Rb.velocity = Vector2.zero;//停止
                controller.transform.position = _superAttackPosition;//ボスの位置を修正
                break;//終わり
            }
        }

        _defaultTimer = RESET_TIME;//タイムリセット

        //必殺技発動
        while (true)
        {
            //数秒経つごとに弾の見た目を変える
            if (_defaultTimer >= _switchInterval + _switchIntervalOffset)
            {
                //弾の見た目を変える
                _firstPattern = Random.Range(0, _bullet.Length);
                _secondPattern = Random.Range(0, _bullet.Length);
                //経過した秒数を追加
                _switchIntervalOffset += _switchInterval;
            }

                //マズル0（親オブジェクト）を反時計回り（+）に回転する
                Vector3 upperLocalAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得
                upperLocalAngle.z += _rotationInterval;// 角度を設定
                _muzzles[0].localEulerAngles = upperLocalAngle;//回転する

                //弾をマズル0（親オブジェクト）の向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

                //弾をマズル1（子オブジェクト）の向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_secondPattern]).transform.rotation = _muzzles[1].rotation;

                //マズル2（親オブジェクト）を時計回り（-）に回転する
                Vector3 rightLocalAngle = _muzzles[2].localEulerAngles;// ローカル座標を基準に取得
                rightLocalAngle.z -= _rotationInterval;// 角度を設定
                _muzzles[2].localEulerAngles = rightLocalAngle;//回転する

                //弾をマズル2（親オブジェクト）の向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_firstPattern]).transform.rotation = _muzzles[2].rotation;
                
                //弾をマズル3（子オブジェクト）の向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[3].position, _bullet[_secondPattern]).transform.rotation = _muzzles[3].rotation;

            yield return new WaitForSeconds(_attackInterval);//攻撃頻度(秒)
            //数秒経ったら
            if (_defaultTimer >= _activationTime)
            {
                break;//終了
            }
        }
        yield break;//終了
    }

}
