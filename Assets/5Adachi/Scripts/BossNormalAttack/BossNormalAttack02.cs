using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack02 : MonoBehaviour
{
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    /// <summary>弾の見た目の種類</summary>
    int _firstPattern = 0;
    /// <summary>弾の見た目の種類</summary>
    int _secondPattern = 0;
    /// <summary>プレイヤーのタグ</summary>
    [SerializeField, Header("playerのtag")] string _playerTag = null;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.5f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _firstBullet;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _secondBullet;
    /// <summary>対項角</summary>
    const float OPPOSITE_ANGLE = 180f;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(_playerTag);//プレイヤーのタグをとってくる
        StartCoroutine(Attack());
    }

    //Attack関数に入れる通常攻撃
    IEnumerator Attack()
    {
        while (true)
        {
            //弾の見た目をランダムで変える
            _firstPattern = Random.Range(0, _firstBullet.Length);
            _secondPattern = Random.Range(0, _secondBullet.Length);

            ///プレイヤーの向きにマズルが向く///

            //ターゲット（プレイヤー）の方向を計算
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

            ///プレイヤーがいる方向と逆(Xが逆方向)にマズルが向く///
            //マズルを回転する
            Vector3 localAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得           
            localAngle.z = -localAngle.z;//向きを逆側にする
            _muzzles[0].localEulerAngles = localAngle;//回転する
            //弾をマズルの向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_secondPattern]).transform.rotation = _muzzles[0].rotation;

            ///プレイヤーがいる方向と逆(XとYが逆方向）///
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, -_dir);

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_secondPattern]).transform.rotation = _muzzles[0].rotation;

            ///プレイヤーがいる方向と逆(Yが逆方向///
            //マズルを回転する  
            localAngle.z = -localAngle.z + OPPOSITE_ANGLE;//向きを逆側にする
            _muzzles[0].localEulerAngles = -localAngle;//回転する
            //弾をマズルの向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_firstPattern]).transform.rotation = _muzzles[0].rotation;

            yield return new WaitForSeconds(_attackInterval);//攻撃頻度(秒)
        }
    }
}
