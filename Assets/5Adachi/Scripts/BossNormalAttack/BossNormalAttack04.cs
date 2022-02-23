using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack04 : MonoBehaviour
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
    [SerializeField, Header("マズルの角度間隔")] float _rotationInterval = 20f;
    /// <summary>最小の回転値</summary>
    const float MINIMUM_ROT_RANGE = 0f;
    /// <summary>最大の回転値</summary>
    const float MAXIMUM_ROT_RANGE = 360f;

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


            Vector3 firstLocalAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //_rotationIntervalで設定した角度間隔で全方位に発射する
            for (float rotation = MINIMUM_ROT_RANGE + firstLocalAngle.z; rotation < MAXIMUM_ROT_RANGE + firstLocalAngle.z; rotation += _rotationInterval)
            {
                ///マズル1を回転する///
                Vector3 secondLocalAngle = _muzzles[1].localEulerAngles;// ローカル座標を基準に取得
                //角度を設定
                secondLocalAngle.z = rotation;
                _muzzles[1].localEulerAngles = secondLocalAngle;//回転する
                //弾をマズル1の向きに合わせて弾を発射
                ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet).transform.rotation = _muzzles[1].rotation;
            }

            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
