using Overdose.Calculation;
using Overdose.Data;
using System.Collections;
using UnityEngine;

public class BossNormalAttack05 : BossAttackAction
{
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>このスクリプトで使うタイマー</summary>
    float _defaultTimer = 0f;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>音に必要なタイマー</summary>
    float _audioTimer = 0f;
    /// <summary>弾の見た目の種類</summary>
    int _pattern = 0;
    /// <summary>完全な通常攻撃になるのが一回しかできないようにする</summary>
    bool _perfect = false;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.46f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _bullet;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定(Boss05DefaultBullet1)")] PoolObjectType _secondBullet;
    /// <summary>マズルの角度間隔</summary>
    [SerializeField, Header("マズルの角度間隔")] float _rotationInterval = 45f;
    /// <summary>この行動から出る時間</summary>
    [SerializeField, Header("この行動から出る時間")] float _endingTime = 20f;
    /// <summary>アイテムを落とす確率</summary>
    [SerializeField, Header("アイテムを落とす確率")] int _probability = 50;
    /// <summary>攻撃時の音</summary>
    [SerializeField, Header("攻撃時の音")] SoundType _normalAttack;
    /// <summary>音を鳴らすタイミング</summary>
    [SerializeField, Header("音を鳴らすタイミング")] float _audioInterval = 1f;
    /// <summary>完全な通常攻撃になり始める時間</summary>
    const float _perfectTime = 3f;
    /// <summary>最小の回転値</summary>
    const float MINIMUM_ROT_RANGE = 0f;
    /// <summary>最大の回転値</summary>
    const float MAXIMUM_ROT_RANGE = 360f;
   

    public override System.Action ActinoEnd { get; set; }
    
    public override void Enter(BossController contlloer)
    {
        _defaultTimer = 0f;
        _timer = 0f;
        SoundManager.Instance.UseSound(_normalAttack);
        StartCoroutine(Attack(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _defaultTimer += Time.deltaTime;//タイマー
        _timer += Time.deltaTime;
        _audioTimer += Time.deltaTime;

        if(_timer >= _endingTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {

        //一定の確率でアイテムを落とす
        if (Calculator.RandomBool(_probability))
        {
            contlloer.ItemDrop();
        }

        StopAllCoroutines();
    }

    //Attack関数に入れる通常攻撃
    IEnumerator Attack(BossController controller)
    {
        while (true)
        {      
            if (_defaultTimer >= _perfectTime)
            {
                //弾の見た目をランダムで変える
                _pattern = Random.Range(0, _bullet.Length);
                _defaultTimer = 0;//タイマーをリセット
                _perfect = true;//発動
            }

            if(_perfect)
            {
                //弾をマズル1の向きに合わせて弾を発射（親オブジェクトの弾より左側）
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_pattern]).transform.rotation = _muzzles[1].rotation;

                //弾をマズル2の向きに合わせて弾を発射（親オブジェクトの弾より右側）
                ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet[_pattern]).transform.rotation = _muzzles[2].rotation;
            }  

            if(_audioTimer >= _audioInterval)
            {
                //攻撃時の音
                SoundManager.Instance.UseSound(_normalAttack);
                _audioTimer = 0;
            }
            
            //ターゲット（プレイヤー）の方向を計算
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet[_pattern]).transform.rotation = _muzzles[0].rotation;

            //_rotationIntervalで設定した角度間隔で全方位に発射する（マズル１で）
            for (float rotation = MINIMUM_ROT_RANGE; rotation < MAXIMUM_ROT_RANGE; rotation += _rotationInterval)
            {
                ///マズル3を回転する///
                Vector3 secondLocalAngle = _muzzles[3].localEulerAngles;// ローカル座標を基準に取得
                //角度を設定
                secondLocalAngle.z = rotation;
                _muzzles[3].localEulerAngles = secondLocalAngle;//回転する
                //弾をマズル3の向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[3].position, _secondBullet).transform.rotation = _muzzles[3].rotation;
            }

            yield return new WaitForSeconds(_attackInterval);
        }
    }

}
