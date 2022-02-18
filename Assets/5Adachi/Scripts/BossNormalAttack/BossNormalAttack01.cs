using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack01 : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    [SerializeField,Header("playerのtag")] string _playerTag = null;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.6f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Attack());
    }

    void Update()
    {
        //ターゲット（プレイヤー）の方向を計算
        _dir = (_player.transform.position - _muzzles[0].transform.position);
        //ターゲット（プレイヤー）の方向に回転
        _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
    }

    //Attack関数に入れる通常攻撃
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackInterval);
            //親オブジェクトのマズル

            //弾を発射（仮でBombにしてます）
            var bossEnemyBulletStraight = ObjectPool.Instance.UseBullet(_muzzles[0].position, PoolObjectType.Player01BombChild);
            //弾をマズル0の向きに合わせる
            bossEnemyBulletStraight.transform.rotation = _muzzles[0].rotation;

            //子オブジェクトのマズル

            //弾を発射（親オブジェクトの弾より右側）
            var bossEnemyBulletRight = ObjectPool.Instance.UseBullet(_muzzles[1].position, PoolObjectType.Player01BombChild);
            //弾をマズル1の向きに合わせる
            bossEnemyBulletRight.transform.rotation = _muzzles[1].rotation;
            //弾を発射（親オブジェクトの弾より左側）
            var bossEnemyBulletLeft = ObjectPool.Instance.UseBullet(_muzzles[2].position, PoolObjectType.Player01BombChild);
            //弾をマズル2の向きに合わせる
            bossEnemyBulletLeft.transform.rotation = _muzzles[2].rotation;

            
        }
    }
}
