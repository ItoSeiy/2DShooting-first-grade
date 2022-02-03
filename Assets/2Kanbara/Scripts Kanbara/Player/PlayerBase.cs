using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using Cinemachine;
using DG.Tweening;


/// <summary>
/// Playerの基底クラス
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class PlayerBase : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの音")] protected AudioData[] _audioData;
    Rigidbody2D _rb;
    SpriteRenderer _sp;
    protected AudioSource _audioSource;
    Animation _anim;
    Vector2 _dir;

    [SerializeField, Header("リスポーンするポジション")] public Transform _playerRespawn = default;
    [SerializeField, Header("弾を発射するポジション")] public Transform _muzzle = default;

    [SerializeField, Header("精密操作時の発射する間隔(ミリ秒)")] public int _superAttackDelay = 200;
    [SerializeField, Header("発射する間隔(ミリ秒)")] public int _attackDelay = 200;
    [SerializeField, Header("ボムのクールタイム（ミリ秒）")] public int _bombCoolTime = default;
    [SerializeField, Header("無敵モードのクールタイム")] public int _invincibleCoolTime = 2800;

    [SerializeField, Header("この数値未満ならレベル１")] int _playerLevel1Denom = default;
    [SerializeField, Header("レベル１以上のとき、この数値未満ならレベル２")] int _playerLevel2Denom = default;
    [SerializeField, Header("この数値までがレベル３")] int _playerLevel3Denom = default;

    [SerializeField, Header("Enemyのタグ")] string _enemyTag = "Enemy";
    [SerializeField, Header("EnemyのBulletのタグ")] string _enemyBulletTag = "EnemyBullet";
    [SerializeField, Header("Powerのタグ")] string _powerTag = "Power";
    [SerializeField, Header("Pointのタグ")] string _pointTag = "Point";
    [SerializeField, Header("1upのタグ")] string _1upTag = "1UP";
    [SerializeField, Header("ボムを増やすアイテムのタグ")] string _bombItemTag = "BombItem";
    [SerializeField, Header("Invincibleのタグ")] string _invincibleTag = "Invincible";

    [SerializeField, Header("動くスピード")] float _moveSpeed = default;
    [SerializeField, Header("精密操作のスピード")] float _lateMove = default;

    [SerializeField, Header("この数値以上なら、一定時間無敵モードになる変数")] int _invincibleMax = default;
    [SerializeField, Header("Playerのパワーの上限")] int _playerPowerMax = default;
    [SerializeField, Header("リスポーン中の無敵時間")] protected int _respawnTime = 2800;
    [SerializeField, Header("リスポーン後の無敵時間")] int _afterRespawnTime = 1400;

    [SerializeField, Header("死亡時のアニメーション")] Animation _dead = default;

    [SerializeField, Header("音量を調節する変数")] protected float _musicVolume = default;

    [SerializeField, Header("揺らすカメラ")] CinemachineVirtualCamera _cmvcam1 = default;

    [SerializeField, Header("通常弾の音")] protected string _playerBulletAudio = "Bullet";
    [SerializeField, Header("精密操作時の弾の音")] protected string _playerSuperBulletAudio = "SuperBullet";
    [SerializeField, Header("チャージ中の音")] protected string _playerChargeBulletAudio = "Charge";
    [SerializeField, Header("チャージショットの音")] protected string _playerChargeShotBulletAudio = "ChargeShot";
    [SerializeField, Header("ボムの音")] protected string _playerBombShotAudio = "Bomb";
    [SerializeField, Header("プレイヤーの被弾時に流れる音")] protected string _playerDestroyAudio = "PlayerDestroy";

    [SerializeField] Color _color = default;
    [SerializeField] float _changeValueInterval = default;

    protected const int _level1 = 1;
    protected const int _level2 = 2;
    protected const int _level3 = 3;

    public int PlayerLevel1 => _playerLevel1Denom;
    public int PlayerLevel2 => _playerLevel2Denom;
    public int PlayerLevel3 => _playerLevel3Denom;

    protected int _playerResidue = default;//プレイヤーの残機を入れておく変数
    protected int _bombCount = default;//プレイヤーの所持するボムの数を入れておく変数
    protected int _playerScore = default;//プレイヤーのスコアを入れておく変数
    protected int _playerPower = default;//プレイヤーのパワーを入れておく変数
    protected int _invincibleObjectCount = default;//一定数集めると無敵モードになるアイテムの数を入れておく変数

    /// <summary>連続で弾を撃てないようにするフラグ</summary>
    protected bool _isBulletStop = default;
    /// <summary>精密操作時のフラグ</summary>
    bool _isLateMode = default;
    /// <summary>無敵モードのフラグ</summary>
    protected bool _godMode = default;
    /// <summary>ボムの使用時に立つフラグ</summary>
    protected bool _isBomb = default;
    /// <summary>コントロールが効かないようにするフラグ</summary>
    bool _isNotControll = default;
    /// <summary>チャージしているかどうか判定するフラグ</summary>
    bool _wasCharge = default;
    /// <summary>アタックしているかどうか判定するフラグ</summary>
    bool _isAttackMode = default;

    /// <summary>カウントアップする定数</summary>
    const int _defaultUp = 1;
    /// <summary>カウントダウンする定数</summary>
    const int _defaultDown = -1;
    /// <summary>0の入った定数</summary>
    const int _default = 0;
    /// <summary>InvincibleObjectを初期化する定数</summary>
    readonly int _returnDefault = -150;

    public int PlayerResidue => _playerResidue;//残機を入れておくプロパティ
    public int BombCount => _bombCount;//ボムの数を入れておくプロパティ            
    public int PlayerScore => _playerScore;//プレイヤーのスコアを入れておくプロパティ
    public int PlayerPower => _playerPower;//パワーを入れておくプロパティ
    public int PlayerInvincible => _invincibleObjectCount;//InvincibleObjectの所持数を入れておくプロパティ

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animation>();
        _audioData = GetComponent<AudioData[]>();

        _cmvcam1.Priority = -1;

        transform.position = _playerRespawn.position;//リスポーン地点に移動

        _playerResidue = GameManager.Instance.PlayerResidue;
        _bombCount = GameManager.Instance.PlayerBombCount;
        _playerScore = GameManager.Instance.PlayerScore;
        _playerPower = GameManager.Instance.PlayerPower;
        _invincibleObjectCount = GameManager.Instance.PlayerInvincibleObjectCount;
    }

    private async void Update()
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
        switch (_isAttackMode)
        {
            case false:
                break;
            case true:
                switch (_isLateMode)
                {
                    case false:
                        if (_isBulletStop) return;
                        PlayerAttack();
                        await Task.Delay(_attackDelay);
                        _isBulletStop = false;
                        break;
                    case true:
                        if (_isBulletStop) return;
                        PlayerSuperAttack();
                        await Task.Delay(_superAttackDelay);
                        _isBulletStop = false;
                        break;
                }
                break;
        }
        if (_dir.x > _default)
        {
            _sp.flipX = false;
        }
        else if (_dir.x < _default)
        {
            _sp.flipX = true;
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

    public async void OnJump(InputAction.CallbackContext context)//SpaceKeyが押された瞬間の処理
    {
        if(context.started && BombCount > _default && !_isBomb && !_isNotControll)
        {
            Bom();
            await Task.Delay(_bombCoolTime);
            _isBomb = false;
            GameManager.Instance.PlayerBombCountChange(_defaultDown);
            _bombCount = GameManager.Instance.PlayerBombCount;
        }
    }

    public void OnInputChargeShotButton(InputAction.CallbackContext context)//ChargeShotの処理
    {
        if (context.started && !_wasCharge && !_isAttackMode)
        {
            _cmvcam1.Priority = 10;
            _audioSource.Stop();
            Play(_playerChargeBulletAudio);
            _wasCharge = true;
        }
        if(context.performed && _wasCharge)
        {
            PlayerChargeAttack();
            _wasCharge = false;
            _cmvcam1.Priority = -1;
        }
        if(context.canceled)
        {
            _audioSource.Stop();
            _wasCharge = false;
            _cmvcam1.Priority = -1;
        }
    }

    public void OnFire(InputAction.CallbackContext context)//Mouceの左クリックまたは、GamePadのZRボタンで弾を出す
    {
        if (context.started)//攻撃時の処理
        {
            if (_wasCharge) return;
            _isAttackMode = true;
        }
        if(context.canceled)
        {
            _isAttackMode = false;
        }
    }

    /// <summary>通常の攻撃処理</summary>
    public virtual void PlayerAttack()
    {
        _isBulletStop = true;
    }

    /// <summary>精密操作時の攻撃処理</summary>
    public virtual void PlayerSuperAttack()
    {
        _isBulletStop = true;
    }

    /// <summary>チャージショット時の攻撃処理</summary>
    public virtual void PlayerChargeAttack()
    {

    }

    /// <summary>ボム使用時の処理</summary>
    public virtual void Bom()
    {
        _isBomb = true;
        Debug.Log("ボム撃ったよー");
        Play(_playerBombShotAudio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //EnemyまたはEnemyBuletに当たった際行う残機を減らす処理　無敵モードであれば残機は減らない
        if (!_godMode && collision.gameObject.tag == _enemyTag || collision.gameObject.tag == _enemyBulletTag)
        {
            if (_godMode) return;
            Play(_playerDestroyAudio);
            GameManager.Instance.ResidueChange(_defaultDown);
            _playerResidue = GameManager.Instance.PlayerResidue;

            if (PlayerResidue >= _defaultUp)//残機が残っている場合はリスポーンを行う
            {
                Respawn();
                Debug.Log("残り残機" + PlayerResidue);
            }
            else//残機が0であればゲームオーバー処理を呼び出す
            {
                Debug.LogError("おめぇーの残機ねえから！" + PlayerResidue);
                GameManager.Instance.GameOver();
                gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.tag == _1upTag)//残機を増やす処理
        {
            GameManager.Instance.ResidueChange(_defaultUp);
            _playerResidue = GameManager.Instance.PlayerResidue;
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

        if (collision.gameObject.tag == _invincibleTag)//一定数取得すると無敵になるアイテムの所持数を増やす処理
        {
            GameManager.Instance.PlayerInvicibleObjectValueChange(_defaultUp);
            _invincibleObjectCount = GameManager.Instance.PlayerInvincibleObjectCount;
            Debug.Log("アイテム名決まってない怪しいやつふえたよー" + PlayerInvincible);
            if (PlayerInvincible > _invincibleMax)//一定数アイテムを集めたら無敵モードに切り替わる
            {
                InvincibleMode();
            }
        }
    }

    public async void Respawn()//リスポーンの処理
    {
        _godMode = true;
        _isNotControll = true;
        //_anim = _dead;
        _anim.Play();
        //var sequence = DOTween.Sequence(DOTween.To(() => _color,
        //    (x) =>
        //    {
        //        Color c = _color;
        //        c.a = x;
        //        _color = c;
        //    },
        //    _color,
        //    _changeValueInterval));
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
        GameManager.Instance.PlayerInvicibleObjectValueChange(_returnDefault);
        await Task.Delay(_invincibleCoolTime);
        _godMode = false;
    }

    public void Play(string key)
    {
        var data = System.Array.Find(_audioData, e => e._key == key);

        if (data != null)
        {
            _audioSource.PlayOneShot(data._audio);
        }
        else
        {
            Debug.Log("AudioClipがありません");
        }
    }
}