using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public float PlayerScore => _playerScore;
    public float PlayerPower => _playerPower;
    public float PlayerLevel => _playerLevel;
    public float PlayerBombCount => _playerBombCount;
    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数</summary>
    public float PlayerInvincibleObjectCount => _playerInvicibleObjectCount;
    ///<summary>プレイヤーの残基</summary>
    public float PlayerResidue => _playerResidue;
    /// <summary>レベル2にするために必要なパワーアイテムの数</summary>//UIManagerから参照すべきプロパティ
    public float PlayerPowerRequiredNumberLevel2 { get => _player.PlayerPowerRequiredNumberLevel2; }
    /// <summary>レベル3にするために必要なパワーアイテムの数</summary>//UIManagerから参照すべきプロパティ
    public float PlayerPowerRequiredNumberLevel3 { get => _player.PlayerPowerRequiredNumberLevel3; }
    public bool IsGameOver => _isGameOver;
    public bool IsStageClear => _isStageClear;

    private float _playerScore = default;
    private float _playerPower = default;
    private float _playerLevel = default;
    private float _playerBombCount = default;
    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数///</summary>
    private float _playerInvicibleObjectCount = default;
    /// <summary>プレイヤーの残基</summary>
    [SerializeField] private float _playerResidue =　default;

    private bool _isGameOver = false;
    private bool _isStageClear = false;
    PlayerBase _player;

    private void Awake()
    {
        if(Instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            Init();
            DontDestroyOnLoad(gameObject);
        }

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
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        PlayerLevelCheck();
        _isGameOver = false;
    }

    /// <summary>
    /// ゲームオーバー時に行いたいことを記述
    /// </summary>
    public void GameOver()
    {
        _isGameOver = true;
        Init();
    }

    /// <summary>
    /// ステージをクリアした際にボスから呼び出される
    /// </summary>
    public void StageClear()
    {
        _isStageClear = true;
    }

    /// <summary>
    /// プレイヤースコアの値に変更を加える関数
    /// </summary>
    /// <param name="score">スコア加算 -> 引数,正の数 : スコア減算 -> 引数,負の数</param>
    public void PlayerScoreChange(float score)
    {
        _playerScore += score;
    }

    /// <summary>
    /// プレイヤーのパワーの値に変更を加える関数
    /// パワーを変えることにより発射する弾幕が強化される
    /// </summary>
    /// <param name="power"> パワー加算 -> 引数,正の数 : パワー減算 -> 引数,負の数</param>
    public void PlayerPowerChange(float power)
    {
        _playerPower += power;
        PlayerLevelCheck();
    }

    /// <summary>
    /// 
    /// </summary>
    public  void PlayerLevelCheck()
    {
        if (PlayerPower < _player.PlayerPowerRequiredNumberLevel2)//レベル１のとき
        {
            //パワーアイテムの数が、レベル２になるために必要なパワーアイテム数よりも少なかったときの処理
            _playerLevel = _level1Index;
        }
        else if (PlayerPower >= _player.PlayerPowerRequiredNumberLevel2 && PlayerPower < _player.PlayerPowerRequiredNumberLevel3)//レベル2のとき
        {
            //パワーアイテムの数が、レベル２になるために必要なパワーアイテム数よりも多く、レベル３になるために必要なパワーアイテム数よりも少なかったときの処理
            _playerLevel = _level2Index;
        }
        else if (_player.PlayerPowerRequiredNumberLevel3 <= PlayerPower)//レベル3のとき
        {
            //パワーアイテムの数が、レベル３になるために必要なパワーアイテム数よりも多かったときの処理
            _playerLevel = _level3Index;
        }
    }

    /// <summary>
    /// プレイヤーのボムの所有数に変更を加える関数
    /// </summary>
    /// <param name="bombCount"> ボム加算 -> 引数,正の数 : ボム減算 -> 引数,負の数</param>
    public void PlayerBombCountChange(float bombCount)
    {
        _playerBombCount += bombCount;
    }

    /// <summary>
    /// 一定数獲得すると無敵になるオブジェクトを獲得した数に変更を加える関数
    /// </summary>
    /// <param name="invicibleObjectCount">無敵オブジェクト加算 -> 引数,正の数 : 無敵オブジェクト減算 -> 引数,負の数</param>
    public void PlayerInvicibleObjectValueChange(float invicibleObjectCount)
    {
        _playerInvicibleObjectCount += invicibleObjectCount;
    }

    /// <summary>
    /// 残基に変更を加える関数
    /// </summary>
    /// <param name="residue">残基加算 -> 引数,正の数 : 残基減算 -> 引数,負の数</param>
    public void ResidueChange(float residue)
    {
        _playerResidue += residue;
    }


    /// <summary>
    /// 変数を初期化する関数
    /// </summary>
    public void Init()
    {
        _playerScore = 0;
        _playerPower = 0;
        _playerBombCount = 0;
        _playerInvicibleObjectCount = 0;
        _playerLevel = 1;
        _isGameOver = false;
    }
}