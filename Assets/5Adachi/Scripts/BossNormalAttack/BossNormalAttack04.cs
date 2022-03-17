using Overdose.Calculation;
using Overdose.Data;
using System.Collections;
using UnityEngine;

public class BossNormalAttack04 : BossAttackAction
{
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>弾の見た目の種類</summary>
    int _pattern = 0;
    /// <summary>攻撃回数</summary>
    int _attackCount = 0;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform _muzzle = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.64f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _bullet;
    /// <summary>マズルの角度間隔</summary>
    [SerializeField, Header("マズルの角度間隔")] float _rotationInterval = 1f;
    /// <summary>1回の処理で弾を発射する回数</summary>
    [SerializeField, Header("1回の処理で弾を発射する回数")] int _maximumCount = 7;
    /// <summary>この行動から出る時間</summary>
    [SerializeField, Header("この行動から出る時間")] float _endingTime = 20f;
    /// <summary>アイテムを落とす確率</summary>
    [SerializeField, Header("アイテムを落とす確率")] int _probability = 50;
    /// <summary>攻撃時の音</summary>
    [SerializeField, Header("攻撃時の音")] SoundType _normalAttack;    
    /// <summary>音を鳴らすタイミング</summary>
    [SerializeField, Header("音を鳴らすタイミング")] int _maxAttackCount = 2;
    /// <summary>最小の回転値</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>最大の回転値</summary>
    const float MAXIMUM_ROTATION_RANGE = 360f;
    /// <summary>1回の処理で弾を発射する回数の初期値</summary>
    const int INITIAL_COUNT = 0;

    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        _timer = 0f;

        SoundManager.Instance.UseSound(_normalAttack);
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
            //ターゲット（プレイヤー）の方向を計算
            _dir = (GameManager.Instance.Player.transform.position - _muzzle.transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzle.transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzle.position, _bullet[_pattern]).transform.rotation = _muzzle.rotation;

            if(_attackCount >= _maxAttackCount)
            {
                //攻撃時の音
                SoundManager.Instance.UseSound(_normalAttack);
                _attackCount = 0;
            }
            

            //同じ処理を数回(_maximumCount)繰り返す
            for (int count = INITIAL_COUNT; count < _maximumCount; count++)
            {
                //弾の見た目をランダムで変える
                _pattern = Random.Range(0, _bullet.Length);
                ///マズルを回転する///
                Vector3 localAngle = _muzzle.localEulerAngles;// ローカル座標を基準に取得
                // ランダムな角度を設定（（　0度　～　360度/マズルの角度間隔　）* マズルの角度間隔　)
                localAngle.z = Random.Range(MINIMUM_ROTATION_RANGE, MAXIMUM_ROTATION_RANGE / _rotationInterval) * _rotationInterval;
                _muzzle.localEulerAngles = localAngle;//回転する
                //弾をマズルの向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzle.position, _bullet[_pattern]).transform.rotation = _muzzle.rotation;
            }

            _attackCount++;

            yield return new WaitForSeconds(_attackInterval);
        }
    }

}
