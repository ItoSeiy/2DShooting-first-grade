using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

/// <summary>
/// Playerの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerBase : MonoBehaviour
{
    Rigidbody2D _rb;
    AudioSource _audioSource;
    Animation _anim;
    Vector2 _dir;

    [SerializeField, Header("リスポーンするポジション")] public Transform _playerRespawn = default;
    [SerializeField, Header("弾を発射するポジション")] public Transform _muzzle = default;

    [SerializeField, Header("精密操作時の発射する間隔(ミリ秒)")] public int _superAttackDelay = default;
    [SerializeField, Header("発射する間隔(ミリ秒)")] public int _attackDelay = default;
    [SerializeField, Header("チャージショットの発射する間隔（ミリ秒）")] int _chargeAttackDelay = default;
    [SerializeField, Header("ボムのクールタイム（ミリ秒）")] public int _bombCoolTime = default;
    [SerializeField, Header("無敵モードのクールタイム")] public int _invincibleCoolTime = default;

    [SerializeField, Header("Enemyのタグ")] string _enemyTag = default;
    [SerializeField, Header("EnemyのBulletのタグ")] string _enemyBulletTag = default;
    [SerializeField, Header("Powerのタグ")] string _powerTag = default;
    [SerializeField, Header("Pointのタグ")] string _pointTag = default;
    [SerializeField, Header("1upのタグ")] string _1upTag = default;
    [SerializeField, Header("ボムを増やすアイテムのタグ")] string _bombItemTag = default;
    [SerializeField, Header("Invincibleのタグ")] string _invincibleTag = default;

    [SerializeField, Header("動くスピード")] float _moveSpeed = default;
    [SerializeField, Header("精密操作のスピード")] float _lateMove = default;

    [SerializeField, Header("チャージショット時のチャージ時間")] float _chargeTime = default;

    [SerializeField, Header("この数値以上なら、一定時間無敵モードになる変数")] int _invincibleMax = default;
    [SerializeField, Header("Playerのパワーの上限")] int _playerPowerMax = default;
    [SerializeField, Header("リスポーン中の無敵時間")] public int _respawnTime = default;
    [SerializeField, Header("リスポーン後の無敵時間")] int _afterRespawnTime = default;

    [SerializeField, Header("弾を撃つときの音")] AudioClip _bulletShootingAudio = default;
    [SerializeField, Header("精密操作時に弾を撃つときの音")] AudioClip _superBulletShootingAudio = default;
    [SerializeField, Header("チャージショットを撃つときの音")] AudioClip _chargeBulletShootingAudio = default;
    [SerializeField, Header("被弾したときの音")] AudioClip _onBulletAudio = default;
    [SerializeField, Header("ボムを撃ったときの音")] AudioClip _shootingBombAudio = default;
    [SerializeField, Header("BGM")] AudioClip _bgm = default;

    [SerializeField, Header("死亡時のアニメーション")] Animation _dead = default;

    [SerializeField, Header("この数値未満ならレベル１")] int _level1 = default;
    [SerializeField, Header("この数値以上ならレベル３")] int _level3 = default;

    [SerializeField, Header("音量を調節する変数")] float _musicVolume = default;

    int _playerResidue = default;//プレイヤーの残機を入れておく変数
    int _bombCount = default;//プレイヤーの所持するボムの数を入れておく変数
    int _playerScore = default;//プレイヤーのスコアを入れておく変数
    int _playerPower = default;//プレイヤーのパワーを入れておく変数
    int _invincibleObjectCount = default;//一定数集めると無敵モードになるアイテムの数を入れておく変数

    /// <summary>連続で弾を撃てないようにするフラグ</summary>
    public bool _isBulletStop = default;
    /// <summary>精密操作時のフラグ</summary>
    bool _isLateMode = default;
    /// <summary>無敵モードのフラグ</summary>
    public bool _godMode = default;
    /// <summary>ボムの使用時に立つフラグ</summary>
    public bool _isBomb = default;
    /// <summary>コントロールが効かないようにするフラグ</summary>
    bool _isNotControll = default;
    /// <summary>チャージしているかどうか判定するフラグ</summary>
    bool _isChargeNow = default;

    /// <summary>カウントアップする定数</summary>
    const int _defaultUp = 1;
    /// <summary>カウントダウンする定数</summary>
    const int _defaultDown = -1;
    /// <summary>初期化する定数</summary>
    const int _default = 0;
    
    public int PlayerResidue => _playerResidue;//残機を入れておくプロパティ
    public int BombCount => _bombCount;//ボムの数を入れておくプロパティ
    public int PlayerScore => _playerScore;//プレイヤーのスコアを入れておくプロパティ
    public int PlayerPower => _playerPower;//パワーを入れておくプロパティ
    public int PlayerInvincible => _invincibleObjectCount;//InvincibleObjectの所持数を入れておくプロパティ

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animation>();

        transform.position = _playerRespawn.position;//リスポーン地点に移動

        _playerResidue = GameManager.Instance.Residue;
        _bombCount = GameManager.Instance.PlayerBombCount;
        _playerScore = GameManager.Instance.PlayerScore;
        _playerPower = GameManager.Instance.PlayerPower;
        _invincibleObjectCount = GameManager.Instance.PlayerInvincibleObjectCount;
    }

    private void Update()
    {
        switch (_isLateMode)//移動時に精密操作モードかどうか判定する
        {
            case false:
                _rb.velocity = _dir * _moveSpeed;
                break;
            case true:
                _rb.velocity = _dir * _lateMove;
                break;
        }
    }
    public void OnMove(InputAction.CallbackContext context)//通常の移動
    {
        if (_isNotControll) return;
        Vector2 inputMoveMent = context.ReadValue<Vector2>();
        _dir = new Vector2(inputMoveMent.x, inputMoveMent.y);
    }

    public void OnLateMove(InputAction.CallbackContext context)//精密操作時の移動
    {
        if (_isNotControll) return;
        if (context.started)//LeftShiftKeyが押された瞬間の処理
        {
            _isLateMode = true;
            Debug.Log(_isLateMode);
        }
        if (context.canceled)//LeftShiftKeyが離された瞬間の処理
        {
            _isLateMode = false;
            Debug.Log(_isLateMode);
        }
    }

    public void OnJump(InputAction.CallbackContext context)//SpaceKeyが押された瞬間の処理
    {
        if (_isNotControll) return;
        if (BombCount <= _default)
        {
            Bom();
            _isBomb = true;
            GameManager.Instance.PlayerBombCountChange(_defaultDown);
        }
    }

    public void OnFire(InputAction.CallbackContext context)//Mouceの左クリックまたは、GamePadのZRボタンで弾を出す
    {
        if (_isLateMode && !context.canceled)//精密操作時の処理
        {
            PlayerSuperAttack();
            _isChargeNow = true;
            _isBulletStop = true;
        }
        else if (!_isLateMode && !context.canceled)//通常時の処理
        {
            PlayerAttack();
            _isChargeNow = true;
            _isBulletStop = true;
        }
    }

    /// <summary>通常の攻撃処理</summary>
    public virtual async void PlayerAttack()
    {
        if (_playerPower < _level1)//レベル1のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power1);
        }
        else if (_level1 <= _playerPower && _playerPower < _level3)//レベル2のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power2);
        }
        else if (_level3 <= _playerPower)//レベル3のとき
        {
         PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power3);
        }
        _audioSource.PlayOneShot(_bulletShootingAudio, _musicVolume);
        await Task.Delay(_attackDelay);
        _isChargeNow = false;
        _isBulletStop = false;
    }

    /// <summary>精密操作時の攻撃処理</summary>
    public virtual async void PlayerSuperAttack()
    {
        if (_playerPower < _level1)//レベル1のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power1);
        }
        else if (_level1 <= _playerPower && _playerPower < _level3)//レベル2のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power2);
        }
        else if (_level3 <= _playerPower)//レベル3のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power3);
        }
        _audioSource.PlayOneShot(_superBulletShootingAudio, _musicVolume);
        await Task.Delay(_superAttackDelay);
        _isChargeNow = false;
        _isBulletStop = false;
    }

    /// <summary>チャージショット時の攻撃処理</summary>
    public virtual async void PlayerChargeAttack()
    {
        if (_playerPower < _level1)//レベル1のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power1);
        }
        else if (_level1 <= _playerPower && _playerPower < _level3)//レベル2のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power2);
        }
        else if (_level3 <= _playerPower)//レベル3のとき
        {
            PlayerBulletPool.Instance.UseBullet(_muzzle.position, PlayerBulletType.Player01Power3);
        }
        _audioSource.PlayOneShot(_chargeBulletShootingAudio, _musicVolume);
        await Task.Delay(_chargeAttackDelay);
        _isChargeNow = false;
        _isBulletStop = false;
    }

    /// <summary>ボム使用時の処理</summary>
    public virtual async void Bom()
    {
        Debug.Log("ボム撃ったよー");
        _audioSource.PlayOneShot(_shootingBombAudio, _musicVolume);
        await Task.Delay(_bombCoolTime);
        _isBomb = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EnemyまたはEnemyBuletに当たった際行う残機を減らす処理　無敵モードであれば残機は減らない
        if (!_godMode && collision.gameObject.tag == _enemyTag || collision.gameObject.tag == _enemyBulletTag)
        {
            if (_godMode) return;
            _audioSource.PlayOneShot(_onBulletAudio, _musicVolume);
            GameManager.Instance.ResidueChange(_defaultDown);
            _playerResidue = GameManager.Instance.Residue;

            if (PlayerResidue > _default)//残機が残っている場合はリスポーンを行う
            {
                Respawn();
                Debug.Log("残り残機" + PlayerResidue);
            }
            else//残機が0であればゲームオーバー処理を呼び出す
            {
                Debug.LogError("おめぇーの残機ねえから！" + PlayerResidue);
                GameManager.Instance.GameOver();
            }
        }

        if (collision.gameObject.tag == _1upTag)//残機を増やす処理
        {
            GameManager.Instance.ResidueChange(_defaultUp);
            _playerResidue = GameManager.Instance.Residue;
            Debug.Log("残機ふえたよー" + PlayerResidue);
        }

        if(collision.gameObject.tag == _bombItemTag)//ボムの所持数を増やす処理
        {
            GameManager.Instance.PlayerBombCountChange(_defaultUp);
            _bombCount = GameManager.Instance.PlayerBombCount;
            Debug.Log("ボムふえたよー" + BombCount);
        }

        if (collision.gameObject.tag == _pointTag)//スコアを増やす処理
        {
            GameManager.Instance.PlayerScoreChange(_defaultUp);
            _playerScore = GameManager.Instance.PlayerScore;
            Debug.Log("スコアふえたよー" + PlayerScore);
        }

        if (collision.gameObject.tag == _powerTag && _playerPower < _playerPowerMax)//パワーを増やす処理
        {
            GameManager.Instance.PlayerPowerChange(_defaultUp);
            _playerPower = GameManager.Instance.PlayerPower;
            Debug.Log("パワーふえたよー" + PlayerPower);
        }

        if (collision.gameObject.tag == _invincibleTag)//取ると無敵になるアイテムの所持数を増やす処理
        {
            GameManager.Instance.PlayerInvicibleObjectValueChange(_defaultUp);
            _invincibleObjectCount = GameManager.Instance.PlayerInvincibleObjectCount;
            Debug.Log("アイテム名決まってない怪しいやつふえたよー" + PlayerInvincible);
            if (_invincibleObjectCount > _invincibleMax)//一定数アイテムを集めたら無敵モードに切り替わる
            {
                InvincibleMode();
                _invincibleObjectCount = _default;
            }
        }
    }

    public async void Respawn()//リスポーンの処理
    {
        _godMode = true;
        _isNotControll = true;
        //_anim = _dead;
        //_anim.Play();
        await Task.Delay(_respawnTime);
        _dir = Vector2.zero;
        transform.position = _playerRespawn.position;//ここでリスポーン地点に移動
        _isNotControll = false;
        await Task.Delay(_afterRespawnTime);
        _godMode = false;
    }

    public virtual async void InvincibleMode()//無敵モード
    {
        if (_godMode) return;
        _godMode = true;
        await Task.Delay(_invincibleCoolTime);
        _godMode = false;
    }
}