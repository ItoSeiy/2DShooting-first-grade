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
    [SerializeField, Header("リスポーンするポジション")] public Transform _playerRespawn = default;
    [SerializeField, Header("弾を発射するポジション")] public Transform _muzzle = default;
    [SerializeField, Header("弾")] public GameObject[] _bullet = default;
    [SerializeField, Header("精密操作時の弾")] public GameObject[] _superBullet = default;
    [SerializeField, Header("精密操作時の発射する間隔(ミリ秒)")] public int _superAttackDelay = default;
    [SerializeField, Header("ボムのクールタイム（ミリ秒）")] public int _bomCoolTime = default;
    [SerializeField, Header("発射する間隔(ミリ秒)")] public int _attackDelay = default;
    [SerializeField, Header("無敵モードのクールタイム")] public int _invincibleCoolTime = default;
    [SerializeField, Header("動くスピード")] float _moveSpeed = default;
    [SerializeField, Header("精密操作のスピード")] float _lateMove = default;
    [SerializeField, Header("残機")] int _playerLife = default;
    [SerializeField, Header("Enemyのタグ")] string _enemyTag = default;
    [SerializeField, Header("EnemyのBulletのタグ")] string _enemyBulletTag = default;
    [SerializeField, Header("Powerのタグ")] string _powerTag = default;
    [SerializeField, Header("Pointのタグ")] string _pointTag = default;
    [SerializeField, Header("1upのタグ")] string _1upTag = default;
    [SerializeField, Header("Invincibleのタグ")] string _invincibleTag = default;
    [SerializeField, Header("この数値以上なら、一定時間無敵モードになる変数")] int _invincibleMax = default;
    [SerializeField, Header("Playerのパワーの上限")] int _playerPowerMax = default;
    [SerializeField, Header("リスポーン中の無敵時間")] public int _respawnTime = default;

    [SerializeField, Header("弾を撃つときの音")] AudioSource _bulletShootingAudio = default;
    [SerializeField, Header("被弾したときの音")] AudioSource _onBulletAudio = default;
    [SerializeField, Header("ボムを撃ったときの音")] AudioSource _shootingBomAudio = default;

    int _bomCount = default;//ボムの数を入れておく変数
    public int _playerPower = default;//プレイヤーのパワーを入れておく変数
    int _invincibleObjectCount = default;//一定数集めると無敵モードになるアイテムの数を入れておく変数
        
    /// <summary>連続で弾を撃てないようにするフラグ</summary>
    public bool _isBulletStop = default;
    /// <summary>精密操作時のフラグ</summary>
    bool _isLateMode = default;
    /// <summary>無敵モードのフラグ</summary>
    public bool _godMode = default;
    /// <summary>ボムの使用時に立つフラグ</summary>
    public bool _isBom = default;
    /// <summary>コントロールが効かないようにするフラグ</summary>
    bool _isNotControll = default;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.position = _playerRespawn.position;
        _bomCount = GameManager.Instance.BombCount;
        _playerPower = GameManager.Instance.Power;
        _invincibleObjectCount = GameManager.Instance.InvincibleObjectCount;
    }

    void Update()
    {
        if (_isNotControll) return;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Fire3") && !_isNotControll)//精密操作
        {
            Vector2 dir = new Vector2(h, v).normalized;
            _rb.velocity = dir * _lateMove;
            _isLateMode = true;
        }
        else//通常操作
        {
            Vector2 dir = new Vector2(h, v).normalized;
            _rb.velocity = dir * _moveSpeed;
            _isLateMode = false;
        }

        if (Input.GetButton("Fire1") && _isLateMode && !_isBulletStop && !_isNotControll)//精密操作時の攻撃
        {
            PlayerSuperAttack();
            _isBulletStop = true;
        }
        else if (Input.GetButton("Fire1") && !_isBulletStop && !_isNotControll)//通常攻撃
        {
            PlayerAttack();
            _isBulletStop = true;
        }

        if(Input.GetButtonDown("Jump") && _bomCount != 0 && !_isBom && !_isNotControll)//ボム使用
        {
            Bom();
            _isBom = true;
            _bomCount -= 1;
        }
    }
    /// <summary> </summary>
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
        Debug.Log("Bom");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == _enemyTag || collision.gameObject.tag == _enemyBulletTag && !_godMode)
        {
            if (_godMode) return;
            _playerLife -= 1;
            if(_playerLife != 0)
            {
                Respawn();
                Debug.Log("yes");
            }
            else
            {
                //ゲームマネージャーからGameOverの関数を呼び出す
                Debug.LogError("なんだろう、ゲームオーバーの関数を呼び出してもらっていいすか？");
            }
        }
        if (collision.gameObject.tag == _powerTag && _playerPower < _playerPowerMax)
        {
            GameManager.Instance.GetPower(1);
        }
        if (collision.gameObject.tag == _pointTag)
        {
            GameManager.Instance.GetScore(1);        
        }
        if (collision.gameObject.tag == _1upTag)
        {
            //1upを取ったら1upが増える処理を書く
        }
        if(collision.gameObject.tag == _invincibleTag)
        {
            GameManager.Instance.GetInvicibleObjectCount(1);
            if(_invincibleObjectCount > _invincibleMax)//一定数アイテムを集めたら無敵モードに切り替わる
            {
                InvincibleMode();
                _invincibleObjectCount = 0;
            }
        }
    }

    public async void Respawn()
    {
        _godMode = true;
        _isNotControll = true;
        await Task.Delay(_respawnTime);
        transform.position = _playerRespawn.position;
        _godMode = false;
        _isNotControll = false;
    }
    public virtual async void InvincibleMode()
    {
        _godMode = true;
        await Task.Delay(_invincibleCoolTime);
        _godMode = false;
    }
}
