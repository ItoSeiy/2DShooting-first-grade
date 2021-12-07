using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField, Header("弾を発射するポジション")] public Transform _muzzle = default;
    [SerializeField, Header("弾")] public GameObject[] _bullet = default;
    [SerializeField, Header("精密操作時の弾")] public GameObject[] _superBullet = default;
    [SerializeField, Header("残機")] int _playerLife = default;
    [SerializeField, Header("発射する間隔(ミリ秒)")] public int _attackDelay = default;
    [SerializeField, Header("精密操作時の発射する間隔(ミリ秒)")] public int _superAttackDelay = default;
    [SerializeField, Header("ボムの個数")] public int _bomCount = default;
    /// <summary>連続で弾を撃てないようにするフラグ</summary>
    public bool _isBulletStop = default;
    /// <summary>精密操作時のフラグ</summary>
    bool _isLateMode = default;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bomCount = FindObjectOfType<GameManager>().GetComponent<GameManager>().BombCount;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Fire3"))
        {
            Vector2 dir = new Vector2(h, v).normalized;
            _rb.velocity = dir * _lateMove;
            _isLateMode = true;
        }
        else
        {
            Vector2 dir = new Vector2(h, v).normalized;
            _rb.velocity = dir * _moveSpeed;
            _isLateMode = false;
        }

        if (Input.GetButton("Fire1") && _isLateMode && !_isBulletStop)
        {
            PlayerSuperAttack();
            _isBulletStop = true;
        }
        else if (Input.GetButton("Fire1") && !_isBulletStop)
        {
            PlayerAttack();
            _isBulletStop = true;
        }

        if(Input.GetButtonDown("Jump") && _bomCount != 0)
        {
            Bom();
        }

        if(_playerLife == 0)
        {
            Destroy(this.gameObject);
            // ここにゲームマネージャーからGameOverの関数を呼び出す予定です
        }
    }

    public virtual void PlayerAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }

    public virtual void PlayerSuperAttack()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
    }
    public virtual void Bom()
    {
        Debug.LogError("派生クラスでメソッドをオーバライドしてください。");
        _bomCount -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            _playerLife -= 1;
            // ここにリスポーンの処理を呼び出すべきでしょう
        }
        if (collision.gameObject.tag == "Power")
        {
            //powerを取ったらpowerが増える処理を書く
        }
        if (collision.gameObject.tag == "Point")
        {
            //pointを取ったらpointが増える処理を書く
        }
        if (collision.gameObject.tag == "1up")
        {
            //1upを取ったら1upが増える処理を書く
        }
    }

    public int _power = default;
    public int GetPower()
    {
        return _power = FindObjectOfType<GameManager>().GetComponent<GameManager>().Power;
    }
}
