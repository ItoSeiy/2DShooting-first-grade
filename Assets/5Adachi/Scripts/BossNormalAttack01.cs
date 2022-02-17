using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack01 : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    [SerializeField,Header("playerのtag")] string _playerTag = null;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>速度</summary>
    [SerializeField, Header("スピード")] float _speed = 4f;
    Vector3 _rotation;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }

    void Update()
    {
        _rotation = _player.transform.position - _muzzles[0].transform.position;

        _rotation.y = 0f;

        Quaternion quaternion = Quaternion.LookRotation(_rotation);
        // 算出した回転値をこのゲームオブジェクトのrotationに代入
        _muzzles[0].transform.rotation = quaternion;
    }
}
