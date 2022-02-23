using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack01 : MonoBehaviour
{
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    /// <summary>プレイヤーのタグ</summary>
    [SerializeField,Header("playerのtag")] string _playerTag = null;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.6f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType _bullet;

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
            //ターゲット（プレイヤー）の方向を計算
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
     
            //親オブジェクトのマズル
            
            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //子オブジェクトのマズル

            //弾をマズル1の向きに合わせて弾を発射（親オブジェクトの弾より右側）
            ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet).transform.rotation= _muzzles[1].rotation;
            
            //弾をマズル2の向きに合わせて弾を発射（親オブジェクトの弾より左側）
            ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet).transform.rotation = _muzzles[2].rotation;

            //弾をマズル3の向きに合わせて弾を発射（親オブジェクトの弾より右側）
            ObjectPool.Instance.UseObject(_muzzles[3].position, _bullet).transform.rotation = _muzzles[3].rotation;

            //弾をマズル4の向きに合わせて弾を発射（親オブジェクトの弾より左側）
            ObjectPool.Instance.UseObject(_muzzles[4].position, _bullet).transform.rotation = _muzzles[4].rotation;

            yield return new WaitForSeconds(_attackInterval);
        }
    }
}
