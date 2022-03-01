using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ゲームマネージャー
/// ゲーム内に一つのみ存在しなければならない
/// 
/// シングルトンパターン
/// プレイヤーのアイテム数やレベルを持っている
/// </summary>
public class GameManager : SingletonMonoBehaviour<GameManager>
{

    const int LEVEL1 = 1;
    const int LEVEL2 = 2;
    const int LEVEL3 = 3;

    /// <summary>プレイヤーの参照</summary>
    public PlayerBase Player
    {
        get
        {
            if(!_player)
            {
                Debug.LogError("Playerタグ,PlayerBaseがアタッチされたオブジェクトがありません\n追加してください");
                return null;
            }
            return _player;
        }
    }

    public int PlayerLevel => _playerLevel;
    /// <summary>プレイヤーが持っているパワーアイテムの数</summary>
    public int PlayerPowerItemCount => _playerPowerItemCount;

    /// <summary>プレイヤーのスコア</summary>
    public int PlayerScore => _playerScore;

    /// <summary>プレイヤーのボムの所持数</summary>
    public int PlayerBombCount => _playerBombCount;

    ///<summary>プレイヤーの残機</summary>
    public int PlayerResidueCount => _playerResidue;

    ///<summary>一定数獲得すると無敵になるオブジェクトを獲得した数</summary>
    public int PlayerInvincibleObjectCount => _playerInvicibleObjectCount;

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
        SettingScene();
        DontDestroyOnLoad(gameObject);
    }
    
    /// <summary>
    /// シーン遷移後に呼び出される関数
    /// プレイヤーがいたらゲームスタート関数を呼び出す
    /// </summary> 
    public void SettingScene()
    {
        _player = GameObject.FindWithTag("Player")?.GetComponent<PlayerBase>();
        Debug.Log("シーンが読み込まれました");
        if(_player)
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        Debug.Log("プレイヤーを参照できました\nUIとプレイヤーのセットを行います");
        PlayerLevelSet();
        UIManager.Instance.UISet();
        _isGameStart = true;
        _isGameOver = false;
        _isStageClear = false;
    }

    /// <summary>
    /// ゲームオーバー時に行いたいことを記述
    /// </summary>
    public void GameOver()
    {
        _isGameOver = true;
        _isGameStart = false;
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
        UIManager.Instance.UIScoreChange(score);
    }

    /// <summary>
    /// プレイヤーのパワーアイテムの値に変更を加える関数
    /// </summary>
    /// <param name="itemCount"> パワーアイテム加算 -> 引数,正の数 : パワーアイテム減算 -> 引数,負の数</param>
    public void PlayerPowerItemCountChange(int itemCount)
    {
        _playerPowerItemCount += itemCount;
        UIManager.Instance.UIPowerCountChange(itemCount);
        PlayerLevelSet();

    }

    /// <summary>
    /// プレイヤーのレベルを決定する関数
    /// </summary>
    public  void PlayerLevelSet()
    {
        if (_playerPowerItemCount < _player.PlayerPowerRequiredNumberLevel2)//レベル１のとき
        {
            //パワーアイテムの数が、レベル２になるために必要なパワーアイテム数よりも少なかったときの処理
            _playerLevel = LEVEL1;
            UIManager.Instance.UIPowerLimitChange(_player.PlayerPowerRequiredNumberLevel2);
        }
        else if (_playerPowerItemCount >= _player.PlayerPowerRequiredNumberLevel2 && _playerPowerItemCount < _player.PlayerPowerRequiredNumberLevel3)//レベル2のとき
        {
            //パワーアイテムの数が、レベル２になるために必要なパワーアイテム数よりも多く、レベル３になるために必要なパワーアイテム数よりも少なかったときの処理
            _playerLevel = LEVEL2;
            UIManager.Instance.UIPowerLimitChange(_player.PlayerPowerRequiredNumberLevel3);
        }
        else if (_player.PlayerPowerRequiredNumberLevel3 <= _playerPowerItemCount)//レベル3のとき
        {
            //パワーアイテムの数が、レベル３になるために必要なパワーアイテム数よりも多かったときの処理
            _playerLevel = LEVEL3;
            UIManager.Instance.UIPowerLimitChange(_player.PlayerPowerLimit);
        }
    }

    /// <summary>
    /// プレイヤーのボムの所有数に変更を加える関数
    /// </summary>
    /// <param name="bombCount"> ボム加算 -> 引数,正の数 : ボム減算 -> 引数,負の数</param>
    public void PlayerBombCountChange(int bombCount)
    {
        _playerBombCount += bombCount;
        UIManager.Instance.UIBombChange(bombCount);
    }

    /// <summary>
    /// 一定数獲得すると無敵になるオブジェクトを獲得した数に変更を加える関数
    /// </summary>
    /// <param name="invicibleObjectCount">無敵オブジェクト加算 -> 引数,正の数 : 無敵オブジェクト減算 -> 引数,負の数</param>
    public void PlayerInvicibleObjectValueChange(int invicibleObjectCount)
    {
        _playerInvicibleObjectCount += invicibleObjectCount;
        UIManager.Instance.UIInvisibleCountChange(invicibleObjectCount);
    }

    /// <summary>
    /// 残基に変更を加える関数
    /// </summary>
    /// <param name="residue">残基加算 -> 引数,正の数 : 残基減算 -> 引数,負の数</param>
    public void ResidueChange(int residue)
    {
        _playerResidue += residue;
        UIManager.Instance.UIResidueChange(residue);
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