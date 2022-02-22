using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack02 : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    /// <summary>プレイヤーのタグ</summary>
    [SerializeField, Header("playerのtag")] string _playerTag = null;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.64f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType _bullet;
    /// <summary>マズルの角度間隔</summary>
    [SerializeField, Header("マズルの角度間隔")] float _rotationInterval = 1f;
    /// <summary>1回の処理で弾を発射する回数</summary>
    [SerializeField, Header("1回の処理で弾を発射する回数")] int _maximumCount = 15;
    /// <summary>最小の回転値</summary>
    const float MINIMUM_ROTATION_RANGE = 0f;
    /// <summary>最大の回転値</summary>
    const float MAXIMUM_ROTATION_RANGE = 360f;
    /// <summary>1回の処理で弾を発射する回数の初期値</summary>
    const int INITIAL_COUNT = 0;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Attack());
    }

    //Attack関数に入れる通常攻撃
    IEnumerator Attack()
    {
        while (true)
        {
            //ターゲット（プレイヤー）の方向を計算
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //親オブジェクトのマズル

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseBullet(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //同じ処理を数回(_maximumCount)繰り返す
            for (int count = INITIAL_COUNT; count < _maximumCount; count++)
            {
                ///マズルを回転する///
                Vector3 localAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得
                // ランダムな角度を設定（（　0度　～　360度/マズルの角度間隔　）* マズルの角度間隔　)
                localAngle.z = Random.Range(MINIMUM_ROTATION_RANGE, MAXIMUM_ROTATION_RANGE / _rotationInterval) * _rotationInterval;
                _muzzles[0].localEulerAngles = localAngle;//回転する
                //弾をマズルの向きに合わせて弾を発射
                ObjectPool.Instance.UseBullet(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;
            }

            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
