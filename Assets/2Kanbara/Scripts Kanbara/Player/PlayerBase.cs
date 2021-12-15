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

    [SerializeField, Header("Enemyのタグ")] string _enemyTag = default;
    [SerializeField, Header("EnemyのBulletのタグ")] string _enemyBulletTag = default;
    [SerializeField, Header("Powerのタグ")] string _powerTag = default;
    [SerializeField, Header("Pointのタグ")] string _pointTag = default;
    [SerializeField, Header("1upのタグ")] string _1upTag = default;
    [SerializeField, Header("Invincibleのタグ")] string _invincibleTag = default;

    [SerializeField, Header("動くスピード")] float _moveSpeed = default;
    [SerializeField, Header("精密操作のスピード")] float _lateMove = default;
    [SerializeField, Header("残機")] int _playerLife = default;
    [SerializeField, Header("この数値以上なら、一定時間無敵モードになる変数")] int _invincibleMax = default;
    [SerializeField, Header("Playerのパワーの上限")] int _playerPowerMax = default;
    [SerializeField, Header("リスポーン中の無敵時間")] public int _respawnTime = default;
    [SerializeField, Header("リスポーン後の無敵時間")] int _afterRespawnTime = default;

    [SerializeField, Header("弾を撃つときの音")] AudioClip _bulletShootingAudio = default;
    [SerializeField, Header("被弾したときの音")] AudioClip _onBulletAudio = default;
    [SerializeField, Header("ボムを撃ったときの音")] AudioClip _shootingBomAudio = default;
    [SerializeField, Header("BGM")] AudioClip _bgm = default;
    [SerializeField, Header("死亡時のアニメーション")] Animation _isDie = default;

    [SerializeField, Header("この数値未満ならレベル１")] int _level1 = default;
    [SerializeField, Header("この数値以上ならレベル３")] int _level3 = default;

    int _bomCount = default;//ボムの数を入れておく変数
    int _playerPower = default;//プレイヤーのパワーを入れておく変数
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

    private AudioSource _audioSource = null;

    public int PlayerPower => _playerPower;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.position = _playerRespawn.position;
        _bomCount = GameManager.Instance.BombCount;
        _playerPower = GameManager.Instance.Power;
        _invincibleObjectCount = GameManager.Instance.InvincibleObjectCount;
        _audioSource = GetComponent<AudioSource>();
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
    public virtual async void PlayerAttack()
    {
        int levelIndex = default;

        if (_playerPower < _level1)//レベル1のとき
        {
            levelIndex = 0;
        }
        else if (_level1 <= _playerPower && _playerPower < _level3)//レベル2のとき
        {
            levelIndex = 1;
        }
        else if (_level3 <= _playerPower)//レベル3のとき
        {
            levelIndex = 2;
        }
        GameObject go = Instantiate(_bullet[levelIndex], _muzzle);
        //_audioSource.clip = _bulletShootingAudio;
        //_audioSource.Play();
        await Task.Delay(_attackDelay);
        _isBulletStop = false;
    }

    public virtual async void PlayerSuperAttack()
    {
        int levelIndex = default;
        if (_playerPower < _level1)//レベル1のとき
        {
            levelIndex = 0;
        }
        else if (_level1 <= _playerPower && _playerPower < _level3)//レベル2のとき
        {
            levelIndex = 1;
        }
        else if (_level3 <= _playerPower)//レベル3のとき
        {
            levelIndex = 2;
        }
        GameObject go = Instantiate(_superBullet[levelIndex], _muzzle);
        //_audioSource.clip = _bulletShootingAudio;
        //_audioSource.Play();
        await Task.Delay(_superAttackDelay);
        _isBulletStop = false;
    }
    public virtual void Bom()
    {
        Debug.Log("Bom");
        //_audioSource.clip = _shootingBomAudio;
        //_audioSource.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EnemyまたはEnemyBuletに当たった際行う残機を減らす処理　無敵モードであれば残機は減らない
        if(!_godMode && collision.gameObject.tag == _enemyTag || collision.gameObject.tag == _enemyBulletTag)
        {
            if (_godMode) return;
            //_audioSource.clip = _onBulletAudio;
            //_audioSource.Play();
            _playerLife -= 1;

            if(_playerLife != 0)//残機が残っている場合はリスポーンを行う
            {
                Respawn();
            }
            else//残機が0であればゲームオーバー処理を呼び出す
            {
                GameManager.Instance.GameOver();
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
        _isNotControll = false;
        await Task.Delay(_afterRespawnTime);
        _godMode = false;
    }
    public virtual async void InvincibleMode()
    {
        _godMode = true;
        await Task.Delay(_invincibleCoolTime);
        _godMode = false;
    }
}
