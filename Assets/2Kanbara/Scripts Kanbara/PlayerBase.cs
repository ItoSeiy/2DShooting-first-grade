using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerBase : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField,Header("動くスピード")] float _movespeed = default;
    [SerializeField,Header("精密操作のスピード")] float _latemove = default;
    [SerializeField, Header("弾のスプライト")] SpriteRenderer _sr = default;
    [SerializeField, Header("弾を発射するポジション")] Transform _muzzle = default;
    [SerializeField, Header("弾")] GameObject _bullet = default;
    [SerializeField,Header("攻撃力")] public int _playerpower = default;
    [SerializeField, Header("攻撃頻度")] public float _playerinterval = default;
    int[] _bulletindex = new int[] { 0, 1, 2 };
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Fire3"))
        {
            Vector2 go = new Vector2(h, v).normalized;
            _rb.velocity = go * _latemove;
        }
        else
        {
            Vector2 go = new Vector2(h, v).normalized;
            _rb.velocity = go * _movespeed;
        }

        if (Input.GetButton("Fire1"))
        {
            GameObject gob = Instantiate(_bullet, _muzzle);

        }
    }

    public float PlayerPower { get => _playerpower;}


    public virtual void PlayerAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (_playerpower < 50)
    //    {
    //        _bulletindex = ;
    //    }
    //}
}
