using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Overdose.Data;
using Overdose.Calculation;

public class SuperAttackRestriction: BossAttackAction
{
    
    /// <summary>右側の範囲</summary>
    bool _rightRange;
    /// <summary>左側の範囲</summary>
    bool _leftRange;
    /// <summary>上側の範囲</summary>
    bool _upperRange;
    /// <summary>下側の範囲</summary>
    bool _downRange;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>横方向</summary>
    float _horizontalDir = 0f;
    /// <summary>通常時の被ダメージの割合を保存する</summary>
    float _saveDamageTakenRation = 1f;
    /// <summary>縦方向</summary>
    float _verticalDir = 0f;
    /// <summary>マズル回転時に必要な修正値</summary>
    float _rotOffset = 0f;
    /// <summary>弾の見た目の種類</summary>
    int _pattern = 0;
    /// <summary>回転方向</summary>
    int _rotDir = 1;
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
    [SerializeField, Header("マズルの角度間隔")] float _angleInterval = 10f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _bullet;
    /// <summary>この行動から出る時間</summary>
    [SerializeField, Header("この行動から出る時間")] float _endingTime = 30f;
    /// <summary>被ダメージの割合</summary>
    [SerializeField, Header("被ダメージの割合"), Range(0, 1)] float _damageTakenRationRange = 0.5f;
    /// <summary>ボスの必殺技のタイムライン</summary>
    [SerializeField, Header("ボスの必殺技のタイムライン")] PlayableDirector _Introduction = null;
    /// <summary>攻撃時の音</summary>
    [SerializeField, Header("攻撃時の音")] SoundType _superAttack;
    /// <summary>タイムラインを消す時間</summary>
    [SerializeField, Header("タイムラインを消す時間")] float _introductionStopTime = 3f;
    /// <summary>修正値</summary>
    const float PLAYER_POS_OFFSET = 0.7f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>リセットタイマー</summary>
    const float RESET_TIME = 0f;
    /// <summary>半分の時間</summary>
    const float HALF_TIME = 2;
    /// <summary>最小の回転値</summary>
    const float MINIMUM_ROT_RANGE = 0f;
    /// <summary>最大の回転値</summary>
    const float MAXIMUM_ROT_RANGE = 360f;
    /// <summary>50%の確率</summary>
    const int FIFTY_PERCENT_PROBABILITY = 50;

    public override System.Action ActinoEnd { get; set; }

    
    public override void Enter(BossController contlloer)
    {
        //マズルの回転方向を変更
        if (Calculator.RandomBool(FIFTY_PERCENT_PROBABILITY))
        {
            _rotDir = -_rotDir;
        }

        contlloer.ItemDrop();
        //通常時の被ダメージの割合を保存する
        _saveDamageTakenRation = contlloer.DamageTakenRation;
        //被ダメージの割合を変更する
        contlloer.DamageTakenRation = _damageTakenRationRange;
        StartCoroutine(Restriction(contlloer)); //コルーチンを発動
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _timer += Time.deltaTime;//タイマー
        
        if (_timer >= _activationTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        //被ダメージの割合割合を元に戻す
        contlloer.DamageTakenRation = _saveDamageTakenRation;
        StopAllCoroutines();
    }

    /// <summary>Firework×Windmillのような軌道、必殺技残り時間が半分を切ると逆回転になる
    /// <para>Firework＝花火のような軌道,全方位に発射</para>
    /// <para>Windmill＝渦巻のような軌道,反時計回りに発射</para></summary>
    IEnumerator Restriction(BossController controller)
    {
        _timer = RESET_TIME;//タイムリセット

        //必殺を放つときはBOSSは放つ前にｘを0、Ｙを2の位置に、移動する
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
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                controller.Rb.velocity = Vector2.zero;//停止
                controller.transform.position = _superAttackPosition;//ボスの位置を修正
                break;//終わり
            }
        }

        _timer = RESET_TIME;//タイムリセット

        if (_Introduction)
        {
            _Introduction.gameObject.SetActive(true);
        }

        //必殺技発動
        while (true)
        {
            if (_timer >= _introductionStopTime)
            {
                _Introduction.gameObject.SetActive(false);
            }

            //攻撃時のサウンド
            SoundManager.Instance.UseSound(_superAttack);

            _pattern = Random.Range(0, _bullet.Length);
            //必殺技発動時間の後半になったら反時計回りに全方位発射
            if (_timer >= _activationTime / HALF_TIME)
            {
                _rotOffset -= _rotDir;
            }

            //必殺技発動時間の前半までは反時計回りに全方位発射
            else
            {
                _rotOffset += _rotDir;
            }

            //360度全方位に発射
            for (float angle = MINIMUM_ROT_RANGE + _rotOffset; angle <= MAXIMUM_ROT_RANGE + _rotOffset; angle += _angleInterval)
            {
                //マズルを回転する
                Vector3 localAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得
                localAngle.z = angle;// 角度を設定
                _muzzles[0].localEulerAngles = localAngle;//回転する

                //弾をマズルの向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_pattern]).transform.rotation = _muzzles[0].rotation;
            }

            yield return new WaitForSeconds(_attackInterval);//攻撃頻度(秒)

            //数秒経ったら
            if (_timer >= _activationTime)
            {
                break;//終了
            }
        }
        yield break;//終了
    }

}
