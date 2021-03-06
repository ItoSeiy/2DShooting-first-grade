using Overdose.Calculation;
using Overdose.Data;
using System.Collections;
using UnityEngine;

public class BossNormalAttack03 : BossAttackAction
{
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>マズルの角度間隔</summary>
    float _rotationInterval = 360f;
    /// <summary>発射した回数</summary>
    int _attackCount = 0;
    /// <summary>弾の見た目の種類</summary>
    int _pattern = 0;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>発射する最大回数</summary>
    [SerializeField,Header("発射する最大回数")] int _maxAttackCount = 7;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.64f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _bullet;
    /// <summary>最大の弾数</summary>
    [SerializeField,Header("最大の弾数")] int _maximumBulletRange = 11;
    /// <summary>この行動から出る時間</summary>
    [SerializeField, Header("この行動から出る時間")] float _endingTime = 20f;
    /// <summary>アイテムを落とす確率</summary>
    [SerializeField, Header("アイテムを落とす確率")] int _probability = 50;
    /// <summary>攻撃時の音</summary>
    [SerializeField, Header("攻撃時の音")] SoundType _normalAttack;
        /// <summary>音を鳴らすタイミング</summary>
        [SerializeField, Header("音を鳴らすタイミング")] float _audioInterval = 2f;
    /// <summary>最小の回転値</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>最大の回転値</summary>
    const float MAX_ROTATION_RANGE = 360f;
    /// <summary>最小の弾数</summary>
    const int MINIMUM_BULLET_RANGE = 3;
    /// <summary>発射回数をリセット</summary>
    const int ATTACK_COUNT_RESET = 0;
    /// <summary>最大の玉数の調整</summary>
    const int MAX_BULLET_RANGE_OFFSET = 1;



    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        _timer = 0f;
        //攻撃時のサウンド
        SoundManager.Instance.UseSound(_normalAttack);
        Debug.Log(contlloer);
        StartCoroutine(Attack(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _timer += Time.deltaTime;

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
            //一定の回数発射したら
            if (_attackCount >= _maxAttackCount)
            {
                //弾の見た目をランダムで変える
                _pattern = Random.Range(0, _bullet.Length);
                //1回の攻撃で弾を飛ばす数(3〜?)
                _rotationInterval = MAX_ROTATION_RANGE / Random.Range(MINIMUM_BULLET_RANGE, _maximumBulletRange + MAX_BULLET_RANGE_OFFSET);
                _attackCount = ATTACK_COUNT_RESET;//発射回数をリセット
                
            }
            //ターゲット（プレイヤー）の方向を計算
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            Vector3 firstLocalAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得

            //攻撃時のサウンド
            SoundManager.Instance.UseSound(_normalAttack);

            //同じ処理を数回(_maximumCount)繰り返す
            for (float rotation = MINIMUM_ROTATION_RANGE + firstLocalAngle.z; rotation <= MAX_ROTATION_RANGE + firstLocalAngle.z; rotation += _rotationInterval)
            {
                Vector3 secondLocalAngle = _muzzles[1].localEulerAngles;// ローカル座標を基準に取得
                secondLocalAngle.z = rotation;// 角度を設定
                _muzzles[1].localEulerAngles = secondLocalAngle;//回転する
                //弾をマズルの向きに合わせて弾を発射（仮でBombにしてます）
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet[_pattern]).transform.rotation = _muzzles[1].rotation;
            }
            _attackCount++;//発射回数を１足す
            yield return new WaitForSeconds(_attackInterval);//攻撃頻度(秒)
        }
    }
}
