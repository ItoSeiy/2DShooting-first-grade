using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// Playerの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerBase : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField, Header("動くスピード")] float _movespeed = default;
    [SerializeField, Header("精密操作のスピード")] float _latemove = default;
    [SerializeField, Header("弾を発射するポジション")] Transform _muzzle = default;
    [SerializeField, Header("弾")] GameObject _bullet = default;
    [SerializeField, Header("残機")] int _playerlife = default;
    int[] _bulletindex = new int[] { 0, 1, 2 };
    bool _bulletstop = default;
    bool _latemode = default;
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
            _latemode = true;
        }
        else
        {
            Vector2 go = new Vector2(h, v).normalized;
            _rb.velocity = go * _movespeed;
            _latemode = false;
        }

        if (Input.GetButton("Fire1") && _latemode && !_bulletstop)
        {
            PlayerSuperAttack();
            _bulletstop = true;
        }
        else if (Input.GetButton("Fire1") && !_bulletstop)
        {
            PlayerAttack();
            _bulletstop = true;
        }

        if(_playerlife == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual async void PlayerAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
        //GameObject gob = Instantiate(_bullet, _muzzle); //テスト用
        await Task.Delay(100);
        _bulletstop = false;
    }

    public virtual async void PlayerSuperAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
        //GameObject gob = Instantiate(_bullet, _muzzle); //テスト用
        await Task.Delay(200);
        _bulletstop = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            _playerlife -= 1;
        }
    }
}
