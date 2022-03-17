using Overdose.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class SuperAttackPrison : BossAttackAction
{
    /// <summary>方向</summary>
    Vector3 _dir;  
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
    /// <summary>縦方向</summary>
    float _verticalDir = 0f;
    /// <summary>通常時の被ダメージの割合を保存する</summary>
    float _saveDamageTakenRation = 1f;
    /// <summary>弾の見た目の種類</summary>
    int _firstPattern = 0;
    /// <summary>弾の見た目の種類</summary>
    int _secondPattern = 0;
    /// <summary>弾の見た目の種類</summary>
    int _thirdPattern = 0;
    /// <summary>回転方向</summary>
    bool _rotDir = false;
    /// <summary>必殺前に移動するポジション</summary>
    [SerializeField, Header("必殺前に移動するポジション")] Vector2 _superAttackPosition = new Vector2(0f, 4f);
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>速度</summary>
    [SerializeField, Header("スピード")] float _speed = 4f;  
    /// <summary>必殺技待機時間</summary>
    [SerializeField, Header("必殺技待機時間")] float _waitTime = 5f;
    /// <summary>必殺技発動時間</summary>
    [SerializeField, Header("必殺技発動時間")] float _activationTime = 30f;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.6f;
    /// <summary>マズルの角度間隔</summary>
    [SerializeField, Header("マズルの角度間隔")] float _angleInterval = 3f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _bullet;
    /// <summary>回転し始める時間</summary>
    [SerializeField,Header("回転し始める時間")] float _timeLimit = 5f;
    /// <summary>左回転の限界</summary>
    [SerializeField, Header("左回転の限界")] float _leftRotLimit = 270f;
    /// <summary>右回転の限界</summary>
    [SerializeField, Header("右回転の限界")] float _rightRotLimit = 180f;
    /// <summary>被ダメージの割合</summary>
    [SerializeField, Header("被ダメージの割合"), Range(0, 1)] float _damageTakenRationRange = 0.5f;
    /// <summary>ボスの必殺技のタイムライン</summary>
    [SerializeField, Header("ボスの必殺技のタイムライン")] PlayableDirector _Introduction = null;
    /// <summary>攻撃時の音</summary>
    [SerializeField, Header("攻撃時の音")] SoundType _superAttack;
    /// <summary>タイムラインを消す時間</summary>
    [SerializeField,Header("タイムラインを消す時間")]　float _introductionStopTime = 3f;
    /// <summary>修正値</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>リセットタイマー</summary>
    const float RESET_TIME = 0f;
   
    public override System.Action ActinoEnd { get; set; }
   
    public override void Enter(BossController contlloer)
    {
        contlloer.ItemDrop();
        //通常時の被ダメージの割合を保存する
        _saveDamageTakenRation = contlloer.DamageTakenRation;
        //被ダメージの割合を変更する
        contlloer.DamageTakenRation = _damageTakenRationRange;
        StartCoroutine(Prison(contlloer));//コルーチンを発動
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

    /// <summary>playerを閉じ込めてplayerがいる方向に弾を発射する</summary>
    IEnumerator Prison(BossController controller)
    {
        _timer = RESET_TIME;//タイムリセット

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
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                controller.Rb.velocity = Vector2.zero;//停止
                controller.transform.position = _superAttackPosition;//ボスの位置を修正
                break;//終わり
            }
        }

        _timer = RESET_TIME;//タイムリセット
        _secondPattern = Random.Range(0, _bullet.Length);
        _thirdPattern = Random.Range(0, _bullet.Length);

        if(_Introduction)
        {
            _Introduction.gameObject.SetActive(true);
        }
        

        //必殺技発動
        while (true)
        {
            //攻撃時のサウンド
            SoundManager.Instance.UseSound(_superAttack);

            if (_timer >= _introductionStopTime)
            {
                _Introduction.gameObject.SetActive(false);
            }

            //時間になったら回転
            if(_timer >= _timeLimit)//5f
            {
                
                Vector3 localAngle = _muzzles[1].localEulerAngles;// ローカル座標を基準に取得
                Debug.Log(localAngle.z);

                //制限まで来たら逆回転
                if(localAngle.z >= _leftRotLimit)
                {
                    _rotDir = true;
                    Debug.Log("R");
                }
                else if(localAngle.z <= _rightRotLimit)
                {
                    _rotDir = false;
                    Debug.Log("L");
                }
                if (!_rotDir)
                {
                    localAngle.z += _angleInterval;// 角度を設定
                    _muzzles[1].localEulerAngles = localAngle;//回転する
                }
                else if(_rotDir)
                {
                    localAngle.z -= _angleInterval;// 角度を設定
                    _muzzles[1].localEulerAngles = localAngle;//回転する
                }
            }
            //弾の見た目を変える
            _firstPattern = Random.Range(0, _bullet.Length);
            //動くマズル

            //マズルを回転する

            //ターゲット（プレイヤー）の方向を計算
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

            //動かないマズル

            //弾をマズル1の向きに合わせて弾を発射（動くマズルの弾より右側）
            ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_secondPattern]).transform.rotation = _muzzles[1].rotation;
            
            //弾をマズル2の向きに合わせて弾を発射（動くマズルより左側）
            ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_thirdPattern]).transform.rotation = _muzzles[2].rotation;

            yield return new WaitForSeconds(_attackInterval);//攻撃頻度

            //数秒経ったら
            if (_timer >= _activationTime)
            {
                break;//終了
            }
        }
        yield break;//終了
    }

}
