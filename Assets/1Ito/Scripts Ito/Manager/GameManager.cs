using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ゲームマネージャー
/// ゲーム内に一つのみ存在しなければならない
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    static GameManager _instance;

    const int _level1Index = 1;
    const int _level2Index = 2;
    const int _level3Index = 3;


    public int PlayerScore => _playerScore;
    public int PlayerPower => _playerPower;
    public int PlayerLevel => _playerLevel;
    public int PlayerBombCount => _playerBombCount;
    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数</summary>
    public int PlayerInvincibleObjectCount => _playerInvicibleObjectCount;
    ///<summary>プレイヤーの残基</summary>
    public int Residue => _residue;
    public float PlayerLevel1 { get => _pb.PlayerLevel1; }
    public float PlayerLevel2 { get => _pb.PlayerLevel2; }
    public float PlayerLevel3 { get => _pb.PlayerLevel3; }

    private int _playerScore = default;
    private int _playerPower = default;
    private int _playerLevel = default;
    private int _playerBombCount = default;
    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数///</summary>
    private int _playerInvicibleObjectCount = default;
    /// <summary>プレイヤーの残基</summary>
    private int _residue =　default;

    [SerializeField] UnityEvent _gameOverEvent;

    PlayerBase _pb;

    private void Awake()
    {
        Init();
        _instance = this;
        DontDestroyOnLoad(gameObject);
        _pb = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        PlayerLevelCheck();
    }
    
    /// <summary>
    /// ゲームスタート時に呼び出される関数
    /// 変数のリセットを行う
    /// </summary> 
    public void GameStart()
    {
        _playerScore = 0;
        _playerPower = 0;
        _playerBombCount = 0;
        _playerInvicibleObjectCount = 0;
        _playerLevel = _level1Index;
    }

    /// <summary>
    /// プレイヤースコアの値に変更を加える関数
    /// </summary>
    /// <param name="score">スコア加算 -> 引数,正の数 : スコア減算 -> 引数,負の数</param>
    public void PlayerScoreChange(int score)
    {
        _playerScore += score;
    }

    /// <summary>
    /// プレイヤーのパワーの値に変更を加える関数
    /// パワーを変えることにより発射する弾幕が強化される
    /// </summary>
    /// <param name="power"> パワー加算 -> 引数,正の数 : パワー減算 -> 引数,負の数</param>
    public void PlayerPowerChange(int power)
    {
        _playerPower += power;
        PlayerLevelCheck();
    }

    /// <summary>
    /// 
    /// </summary>
    public  void PlayerLevelCheck()
    {
        if (PlayerPower < _pb.PlayerLevel1)//レベル1のとき
        {
            _playerLevel = _level1Index;
        }
        else if (_pb.PlayerLevel1 <= PlayerPower && PlayerPower < _pb.PlayerLevel3)//レベル2のとき
        {
            _playerLevel = _level2Index;
        }
        else if (_pb.PlayerLevel3 <= PlayerPower)//レベル3のとき
        {
            _playerLevel = _level3Index;
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
        _residue += residue;
    }

    /// <summary>
    /// ゲームオーバー時に行いたいことを記述
    /// </summary>
    public void GameOver()
    {
        Init();
        _gameOverEvent.Invoke();
    }

    /// <summary>
    /// 変数を初期化する関数
    /// </summary>
    public void Init()
    {
        _playerScore = default;
        _playerPower = default;
        _playerBombCount = default;
        _playerInvicibleObjectCount = default;
        _residue = default;
    }
}