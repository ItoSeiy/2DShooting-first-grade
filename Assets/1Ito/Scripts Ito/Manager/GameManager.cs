using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ゲームマネージャー
/// ゲーム内に一つのみ存在しなければならない
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{

    const int LEVEL1 = 1;
    const int LEVEL2 = 2;
    const int LEVEL3 = 3;

    public int PlayerLevel => _playerLevel;
    /// <summary>プレイヤーが持っているパワーアイテムの数</summary>
    public int PlayerPowerItemCount => _playerPowerItemCount;
    /// <summary>プレイヤーのパワーアイテムの上限</summary>
    public int PlayerPowerLimit => _player.PlayerPowerLimit;
    /// <summary>レベル2にするために必要なパワーアイテムの数</summary>//UIManagerから参照すべきプロパティ
    public int PlayerPowerRequiredNumberLevel2 { get => _player.PlayerPowerRequiredNumberLevel2; }
    /// <summary>レベル3にするために必要なパワーアイテムの数</summary>//UIManagerから参照すべきプロパティ
    public int PlayerPowerRequiredNumberLevel3 { get => _player.PlayerPowerRequiredNumberLevel3; }

    /// <summary>プレイヤーのスコア</summary>
    public int PlayerScore => _playerScore;
    /// <summary>プレイヤーのスコアの上限</summary>
    public int PlayerScoreLimit => _player.PlayerScoreLimit;

    /// <summary>プレイヤーのボムの所持数</summary>
    public int PlayerBombCount => _playerBombCount;
    /// <summary>プレイヤーのボムの上限</summary>
    public int PlayerBombLimit => _player.PlayerBombLimit;

    ///<summary>プレイヤーの残機</summary>
    public int PlayerResidueCount => _playerResidue;
    /// <summary>プレイヤーの残機の上限</summary>
    public int PlayerResidueLimit => _player.PlayerResidueLimit;


    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数</summary>
    public int PlayerInvincibleObjectCount => _playerInvicibleObjectCount;
    /// <summary>無敵になるために必要な無敵オブジェクトアイテムの数</summary>
    public int PlayerInvicibleLimit => _player.InvicibleLimit;

    public bool IsGameOver => _isGameOver;
    public bool IsStageClear => _isStageClear;
    public bool IsGameStart => _isGameStart;

    private int _playerScore = default;
    private int _playerPowerItemCount = default;
    [SerializeField] private int _playerLevel = default;
    private int _playerBombCount = default;
    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数///</summary>
    private int _playerInvicibleObjectCount = default;
    /// <summary>プレイヤーの残機</summary>
    [SerializeField] private int _playerResidue =　default;

    private bool _isGameOver = false;
    private bool _isGameStart = false;
    private bool _isStageClear = false;
    PlayerBase _player;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// キャラクターを変えたときに呼び出される
    /// </summary>
    public void CharacterChange()
    {
        Init();
    }
    
    /// <summary>
    /// ゲームスタート時に呼び出される関数
    /// </summary> 
    public void GameStart()
    {
        _player = GameObject.FindWithTag("Player")?.GetComponent<PlayerBase>();
        Debug.Log("ゲームスタート");
        if(_player)
        {
            Debug.Log("プレイヤーセット完了");
            PlayerLevelSet();
            _isGameStart = true;
            _isGameOver = false;
            _isStageClear = false;
            UIManager.Instance.UISet();
        }
    }

    /// <summary>
    /// ゲームオーバー時に行いたいことを記述
    /// </summary>
    public void GameOver()
    {
        _isGameOver = true;
        _isGameStart = false;
        Init();
    }

    /// <summary>
    /// ステージをクリアした際にボスから呼び出される
    /// </summary>
    public void StageClear()
    {
        _isStageClear = true;
        _isGameStart = false;
    }

    /// <summary>
    /// プレイヤースコアの値に変更を加える関数
    /// </summary>
    /// <param name="score">スコア加算 -> 引数,正の数 : スコア減算 -> 引数,負の数</param>
    public void PlayerScoreChange(int score)
    {
        _playerScore += score;
        //UIManager.Instance.UIScoreChange(score);
    }

    /// <summary>
    /// プレイヤーのパワーアイテムの値に変更を加える関数
    /// </summary>
    /// <param name="itemCount"> パワーアイテム加算 -> 引数,正の数 : パワーアイテム減算 -> 引数,負の数</param>
    public void PlayerPowerItemCountChange(int itemCount)
    {
        _playerPowerItemCount += itemCount;
        PlayerLevelSet();
    }

    /// <summary>
    /// プレイヤーのレベルを決定する関数
    /// </summary>
    public  void PlayerLevelSet()
    {
        if (PlayerPowerItemCount < _player.PlayerPowerRequiredNumberLevel2)//レベル１のとき
        {
            //パワーアイテムの数が、レベル２になるために必要なパワーアイテム数よりも少なかったときの処理
            _playerLevel = LEVEL1;
        }
        else if (PlayerPowerItemCount >= _player.PlayerPowerRequiredNumberLevel2 && PlayerPowerItemCount < _player.PlayerPowerRequiredNumberLevel3)//レベル2のとき
        {
            //パワーアイテムの数が、レベル２になるために必要なパワーアイテム数よりも多く、レベル３になるために必要なパワーアイテム数よりも少なかったときの処理
            _playerLevel = LEVEL2;
        }
        else if (_player.PlayerPowerRequiredNumberLevel3 <= PlayerPowerItemCount)//レベル3のとき
        {
            //パワーアイテムの数が、レベル３になるために必要なパワーアイテム数よりも多かったときの処理
            _playerLevel = LEVEL3;
        }
    }

    /// <summary>
    /// プレイヤーのボムの所有数に変更を加える関数
    /// </summary>
    /// <param name="bombCount"> ボム加算 -> 引数,正の数 : ボム減算 -> 引数,負の数</param>
    public void PlayerBombCountChange(int bombCount)
    {
        _playerBombCount += bombCount;
    }

    /// <summary>
    /// 一定数獲得すると無敵になるオブジェクトを獲得した数に変更を加える関数
    /// </summary>
    /// <param name="invicibleObjectCount">無敵オブジェクト加算 -> 引数,正の数 : 無敵オブジェクト減算 -> 引数,負の数</param>
    public void PlayerInvicibleObjectValueChange(int invicibleObjectCount)
    {
        _playerInvicibleObjectCount += invicibleObjectCount;
    }

    /// <summary>
    /// 残基に変更を加える関数
    /// </summary>
    /// <param name="residue">残基加算 -> 引数,正の数 : 残基減算 -> 引数,負の数</param>
    public void ResidueChange(int residue)
    {
        _playerResidue += residue;
    }


    /// <summary>
    /// 変数を初期化する関数
    /// </summary>
    public void Init()
    {
        _playerScore = 0;
        _playerPowerItemCount = 0;
        _playerBombCount = 0;
        _playerInvicibleObjectCount = 0;
        _playerLevel = 1;
        _isGameOver = false;
        _isGameStart = false;
        _isStageClear = false;
    }
}