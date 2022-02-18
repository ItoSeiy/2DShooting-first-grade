using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.InputSystem;
using Cinemachine;


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
    Animator _anim;
    Vector2 _dir;
    List<Rigidbody2D> _itemRbs = new List<Rigidbody2D>();

    [SerializeField, Header("リスポーンするポジション")] Transform _playerRespawn = default;
    [SerializeField, Header("弾を発射するポジション")] protected Transform _muzzle = default;

    [SerializeField, Header("精密操作時の発射する間隔(ミリ秒)")] int _superAttackDelay = 200;
    [SerializeField, Header("発射する間隔(ミリ秒)")] int _attackDelay = 200;
    [SerializeField, Header("ボムのクールタイム（ミリ秒）")] int _bombCoolTime = default;
    [SerializeField, Header("無敵モードのクールタイム(ミリ秒)")] int _invincibleTime = 2800;

    [SerializeField, Header("レベル2に上げるために必要なパワーアイテムの数")] int _playerPowerRequiredNumberLevel2 = 50;
    [SerializeField, Header("レベル3に上げるために必要なパワーアイテムの数")] int _playerPowerRequiredNumberLevel3 = 100;

    [SerializeField, Header("Enemyのタグ")] string _enemyTag = "Enemy";
    [SerializeField, Header("EnemyのBulletのタグ")] string _enemyBulletTag = "EnemyBullet";
    [SerializeField, Header("Powerのタグ")] string _powerTag = "Power";
    [SerializeField, Header("Pointのタグ")] string _pointTag = "Point";
    [SerializeField, Header("1upのタグ")] string _1upTag = "1UP";
    [SerializeField, Header("ボムを増やすアイテムのタグ")] string _bombItemTag = "BombItem";
    [SerializeField, Header("Invincibleのタグ")] string _invincibleTag = "Invincible";
    [SerializeField, Header("アイテムを回収するためのコライダーのタグ")] string _playerItemGetLineTag = "ItemGetLine";

    [SerializeField, Header("被弾時に再生するアニメーションのパラメータ名")] string _invicibleAnimParamName = "IsInvicible";

    [SerializeField, Header("動くスピード")] float _moveSpeed = default;
    [SerializeField, Header("精密操作のスピード")] float _lateMove = default;
    [SerializeField, Header("アイテムを回収するときにアイテムが集まる速度")] float _itemGetSpeed = default;

    [SerializeField, Header("Plaerの残機の上限")] int _playerResidueLimit = 99;
    [SerializeField, Header("Playerのボムの上限")] int _playerBombLimit = 99;
    [SerializeField, Header("Playerのスコアの上限")] int _playerScoreLimit = 999999999;
    [SerializeField, Header("この数値以上なら、一定時間無敵モードになる変数")] int _invincibleLimit = 150;
    [SerializeField, Header("Playerのパワーの上限")] int _playerPowerLimit = 150;
    [SerializeField, Header("リスポーン中の無敵時間")] int _respawnTime = 2800;
    [SerializeField, Header("リスポーン後の無敵時間")] int _afterRespawnTime = 1400;

    [SerializeField, Header("音量を調節する変数")] protected float _musicVolume = default;

    [SerializeField, Header("揺らすカメラ")] CinemachineVirtualCamera _cmvcam1 = default;

    [SerializeField, Header("通常弾の音")] protected string _playerBulletAudio = "Bullet";
    [SerializeField, Header("精密操作時の弾の音")] protected string _playerSuperBulletAudio = "SuperBullet";
    [SerializeField, Header("チャージ中の音")] protected string _playerChargeBulletAudio = "Charge";
    [SerializeField, Header("チャージショットの音")] protected string _playerChargeShotBulletAudio = "ChargeShot";
    [SerializeField, Header("ボムの音")] protected string _playerBombShotAudio = "Bomb";
    [SerializeField, Header("プレイヤーの被弾時に流れる音")] protected string _playerDestroyAudio = "PlayerDestroy";

    [SerializeField, Header("チャージショットのパーティカルシステム（溜め）")] GameObject _chargeps = default;

    [SerializeField, Header("精密操作時の演出R")] GameObject _parsR;
    [SerializeField, Header("精密操作時の演出B")] GameObject _parsB;
    [SerializeField, Header("精密操作時の演出G")] GameObject _parsG;

    [SerializeField, Header("精密操作時のPlayerの色をゲーミングにする変数")] int _gameingPlayerColorTime = default;

    [SerializeField, Header("パワーアイテムの数がカンストしたとき（レベルマックスのとき）の演出")] GameObject _fullPowerModeEffect = default;


    protected const int _level1 = 1;
    protected const int _level2 = 2;
    protected const int _level3 = 3;
    public int PlayerPowerRequiredNumberLevel2 => _playerPowerRequiredNumberLevel2;
    public int PlayerPowerRequiredNumberLevel3 => _playerPowerRequiredNumberLevel3;

    public int PlayerResidueLimit => _playerResidueLimit;
    public int PlayerBombLimit => _playerBombLimit;
    public int PlayerScoreLimit => _playerScoreLimit;
    public int PlayerPowerLimit => _playerPowerLimit;
    /// <summary>無敵モードになるために必要なInvicibleアイテムの数が入ったプロパティ</summary>
    public int InvicibleLimit => _invincibleLimit;

    protected int _playerResidue = default;//プレイヤーの残機を入れておく変数
    protected int _bombCount = default;//プレイヤーの所持するボムの数を入れておく変数
    protected int _playerScore = default;//プレイヤーのスコアを入れておく変数
    protected int _playerPower = default;//プレイヤーのパワーの数を入れておく変数
    protected int _invincibleObjectCount = default;//一定数集めると無敵モードになるアイテムの数を入れておく変数

    /// <summary>連続で弾を撃てないようにするフラグ</summary>
    bool _isBulletStop = default;
    /// <summary>精密操作時のフラグ</summary>
    bool _isLateMode = default;
    /// <summary>無敵モードのフラグ</summary>
    protected bool _godMode = default;
    /// <summary>ボムの使用時に立つフラグ</summary>
    protected bool _isBomb = default;
    /// <summary>コントロールが効かないようにするフラグ</summary>
    bool _isControll = true;
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
    const int _returnDefault = -150;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();

        _cmvcam1.Priority = -1;

        _chargeps.GetComponent<ParticleSystem>();

        _parsR.GetComponent<ParticleSystem>();
        _parsB.GetComponent<ParticleSystem>();
        _parsG.GetComponent<ParticleSystem>();

        _fullPowerModeEffect.GetComponent<ParticleSystem>();

        transform.position = _playerRespawn.position;//リスポーン地点に移動

        _playerResidue = GameManager.Instance.PlayerResidueCount;
        _bombCount = GameManager.Instance.PlayerBombCount;
        _playerScore = GameManager.Instance.PlayerScore;
        _playerPower = GameManager.Instance.PlayerPowerItemCount;
        _invincibleObjectCount = GameManager.Instance.PlayerInvincibleObjectCount;

        GamingFalse();
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
                if (!_isControll) return;
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
        //if(PhaseNovelManager.Instance.NovelePhaesState == NovelPhase.None)//もしノベルが再生されていなかったらコントロール不能にする
        //{
        //    _isControll = false;
        //}
        //else if(PhaseNovelManager.Instance.IsBeforeNovelFinish)
        //{
        //    _isControll = true;
        //}
    }
    public void OnMove(InputAction.CallbackContext context)//通常の移動
    {
        if (!_isControll) return;
        Vector2 inputMoveMent = context.ReadValue<Vector2>();
        _dir = new Vector2(inputMoveMent.x, inputMoveMent.y);
        Inversion();
    }

    public void OnLateMove(InputAction.CallbackContext context)//精密操作時の移動
    {
        if (!_isControll) return;
        if (context.started)//LeftShiftKeyが押された瞬間の処理
        {
            _isLateMode = true;
            GamingPlayer();
            Debug.Log(_isLateMode);
        }
        if (context.canceled)//LeftShiftKeyが離された瞬間の処理
        {
            _isLateMode = false;
            GamingFalse();
            Debug.Log(_isLateMode);
        }
    }


    public async void OnJump(InputAction.CallbackContext context)//SpaceKeyが押された瞬間の処理
    {
        if(context.started && _bombCount > _default && !_isBomb && !_isControll && !_wasCharge && !_isAttackMode)
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
        if (!_isControll) return;
        if (context.started && !_wasCharge && !_isAttackMode)
        {
            _cmvcam1.Priority = 10;
            _audioSource.Stop();
            Play(_playerChargeBulletAudio);
            _wasCharge = true;
            _chargeps.SetActive(true);
        }
        if(context.performed && _wasCharge)
        {
            _wasCharge = false;
            if (!_isControll) return;
            PlayerChargeAttack();
            _cmvcam1.Priority = -1;
            _chargeps.SetActive(false);
        }
        if(context.canceled)
        {
            _wasCharge = false;
            if (!_isControll) return;
            _audioSource.Stop();
            _cmvcam1.Priority = -1;
            _chargeps.SetActive(false);
        }
    }

    public void OnFire(InputAction.CallbackContext context)//Mouceの左クリックまたは、GamePadのZRボタンで弾を出す
    {
        if (context.started)//攻撃時の処理
        {
            if (_wasCharge) return;
            _isAttackMode = true;
        }
        if(context.performed || context.canceled)
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
        Debug.LogError("チャージアタックが実装されていません");
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
            _cmvcam1.Priority = -1;
            _wasCharge = false;
            _isAttackMode = false;
            Play(_playerDestroyAudio);
            GameManager.Instance.ResidueChange(_defaultDown);
            _playerResidue = GameManager.Instance.PlayerResidueCount;

            if (_playerResidue >= _defaultUp)//残機が残っている場合はリスポーンを行う
            {
                Respawn();
                Debug.Log("残り残機" + _playerResidue);
            }
            else//残機が0であればゲームオーバー処理を呼び出す
            {
                Debug.LogError("おめぇーの残機ねえから！" + _playerResidue);
                GameManager.Instance.GameOver();
                gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.tag == _1upTag)//残機を増やす処理
        {
            var item = collision.GetComponent<ItemBase>();
            if (item.IsTaking) return;
            GameManager.Instance.ResidueChange(_defaultUp);
            _playerResidue = GameManager.Instance.PlayerResidueCount;
            Debug.Log("残機ふえたよー" + _playerResidue);
        }

        if(collision.gameObject.tag == _bombItemTag)//ボムの所持数を増やす処理
        {
            var item = collision.GetComponent<ItemBase>();
            if (item.IsTaking) return;
            GameManager.Instance.PlayerBombCountChange(_defaultUp);
            _bombCount = GameManager.Instance.PlayerBombCount;
            Debug.Log("ボムふえたよー" + _bombCount);
        }

        if (collision.gameObject.tag == _pointTag)//スコアを増やす処理
        {
            var item = collision.GetComponent<ItemBase>();
            if (item.IsTaking) return;
            GameManager.Instance.PlayerScoreChange(_defaultUp);
            _playerScore = GameManager.Instance.PlayerScore;
            Debug.Log("スコアふえたよー" + _playerScore);
        }

        if (collision.gameObject.tag == _powerTag)//パワーを増やす処理
        {
            var item = collision.GetComponent<ItemBase>();
            if (item.IsTaking) return;
            if (_playerPower == _playerPowerLimit)
            {
                _fullPowerModeEffect.SetActive(true);
            }
            GameManager.Instance.PlayerPowerItemCountChange(_defaultUp);
            _playerPower = GameManager.Instance.PlayerPowerItemCount;
            Debug.Log("パワーふえたよー" + _playerPower);
        }

        if (collision.gameObject.tag == _invincibleTag)//一定数取得すると無敵になるアイテムの所持数を増やす処理
        {
            var item = collision.GetComponent<ItemBase>();
            if (item.IsTaking) return;
            GameManager.Instance.PlayerInvicibleObjectValueChange(_defaultUp);
            _invincibleObjectCount = GameManager.Instance.PlayerInvincibleObjectCount;
            Debug.Log("アイテム名決まってない怪しいやつふえたよー" + _invincibleObjectCount);
            if (_invincibleObjectCount > _invincibleLimit)//一定数アイテムを集めたら無敵モードに切り替わる
            {
                InvincibleMode();
            }
        }

        if(collision.tag == _playerItemGetLineTag)
        {
            string[] itemTags = new string[] {_1upTag, _bombItemTag, _pointTag, _powerTag, _invincibleTag};
            foreach(var itemTag in itemTags)
            {
                OnItemGetLine(itemTag);
            }
        }
    }

    void OnItemGetLine(string itemTag)
    {
        GameObject[]? items = GameObject.FindGameObjectsWithTag(itemTag);
        foreach(var item in items)
        {
            var itemBase = item.GetComponent<ItemBase>();
            itemBase.OnItemGetLine();
        }
    }

    public async void Respawn()//リスポーンの処理
    {
        _godMode = true;
        _isControll = false;
        _anim.SetBool(_invicibleAnimParamName, true);
        _chargeps.SetActive(false);
        _fullPowerModeEffect.SetActive(false);
        GamingFalse();
        await Task.Delay(_respawnTime);
        _dir = Vector2.zero;
        transform.position = _playerRespawn.position;//ここでリスポーン地点に移動
        _isControll = true;
        await Task.Delay(_afterRespawnTime);
        _godMode = false;
        _anim.SetBool(_invicibleAnimParamName, false);
    }

    public virtual async void InvincibleMode()//無敵モード
    {
        if (_godMode) return;
        _godMode = true;
        _anim.SetBool(_invicibleAnimParamName, true);
        GameManager.Instance.PlayerInvicibleObjectValueChange(_returnDefault);
        await Task.Delay(_invincibleTime);
        _godMode = false;
        _anim.SetBool(_invicibleAnimParamName, false);
    }

    void Inversion()
    {
        if (_dir.x > _default)
        {
            _sp.flipX = false;
        }
        else if (_dir.x < _default)
        {
            _sp.flipX = true;
        }
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
    async void GamingPlayer()
    {
        if (!_isLateMode) return;
        _parsR.SetActive(true);
        await Task.Delay(_gameingPlayerColorTime);
        if (!_isLateMode) return;
        _parsB.SetActive(true);
        await Task.Delay(_gameingPlayerColorTime);
        if (!_isLateMode) return;
        _parsG.SetActive(true);
    }

    void GamingFalse()
    {
        _parsR.SetActive(false);
        _parsB.SetActive(false);
        _parsG.SetActive(false);
    }
    private void OnParticleCollision(GameObject other)
    {
        if (_isLateMode || _godMode) return;
        Respawn();
    }
}