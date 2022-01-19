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


    public int PlayerScore => _playerScore;
    public int PlayerPower => _playerPower;
    public int PlayerBombCount => _playerBombCount;
    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数</summary>
    public int PlayerInvincibleObjectCount => _playerInvicibleObjectCount;
    ///<summary>プレイヤーの残基</summary>
    public int Residue => _residue;

    private int _playerScore = default;
    private int _playerPower = default;
    private int _playerBombCount = default;
    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数///</summary>
    private int _playerInvicibleObjectCount = default;
    /// <summary>プレイヤーの残基</summary>
    private int _residue =　default;
    [SerializeField] UnityEvent _gameOverEvent;
    
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
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