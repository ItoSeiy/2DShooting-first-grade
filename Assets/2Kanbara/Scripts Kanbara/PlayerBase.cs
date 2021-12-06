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
    [SerializeField, Header("動くスピード")] float _moveSpeed = default;
    [SerializeField, Header("精密操作のスピード")] float _lateMove = default;
    [SerializeField, Header("弾を発射するポジション")] Transform _muzzle = default;
    [SerializeField, Header("弾")] GameObject _bullet = default;
    [SerializeField, Header("残機")] int _playerLife = default;
    int[] _bulletIndex = new int[] { 0, 1, 2 };
    bool _bulletStop = default;
    bool _lateMode = default;
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
            _rb.velocity = go * _lateMove;
            _lateMode = true;
        }
        else
        {
            Vector2 go = new Vector2(h, v).normalized;
            _rb.velocity = go * _moveSpeed;
            _lateMode = false;
        }

        if (Input.GetButton("Fire1") && _lateMode && !_bulletStop)
        {
            PlayerSuperAttack();
            _bulletStop = true;
        }
        else if (Input.GetButton("Fire1") && !_bulletStop)
        {
            PlayerAttack();
            _bulletStop = true;
        }

        if(_playerLife == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public virtual async void PlayerAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
        //GameObject gob = Instantiate(_bullet, _muzzle); //テスト用
        await Task.Delay(100);
        _bulletStop = false;
    }

    public virtual async void PlayerSuperAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
        //GameObject gob = Instantiate(_bullet, _muzzle); //テスト用
        await Task.Delay(200);
        _bulletStop = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            _playerLife -= 1;
        }
    }
}
