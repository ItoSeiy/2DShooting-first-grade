using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseNovelManager : SingletonMonoBehaviour<PhaseNovelManager>
{

    public NovelPhase NovelePhaesState => _novelPhaseState;

    /// <summary>スプレッドシートシートを読み込むスクリプト(戦闘前)</summary>
    [SerializeField]
    GSSReader _beforeGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(戦闘前)</summary>
    [SerializeField]
    NovelRenderer _beforeNovelRenderer;

    /// <summary>スプレッドシートシートを読み込むスクリプト(勝利時)</summary>
    [SerializeField] 
    GSSReader _winGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(戦闘前)</summary>
    [SerializeField] 
    NovelRenderer _winNovelRenderer;

    /// <summary>スプレッドシートシートを読み込むスクリプト(敗北時)</summary>
    [SerializeField] 
    GSSReader _loseGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(敗北時)</summary>
    [SerializeField] 
    NovelRenderer _loseNovelRenderer;

    /// <summary>ノベルを映し出すキャンバス</summary>
    [SerializeField]
    Canvas _novelCanvas;

    /// <summary>ゲームオーバー時に出すUI</summary>
    [SerializeField]
    Canvas _gameOverCanavas;
    /// <summary>ゲームクリア時に出すUI</summary>
    [SerializeField]
    Canvas _gameClearCanvas;

    /// <summary>常にスコアなどが表示されているキャンバス</summary>
    [SerializeField] 
    Canvas _uiCanvas;

    /// <summary>モブ敵の出現場所</summary>
    [SerializeField] 
    Transform _generateTransform;
    /// <summary>ボスの出現位置</summary>
    [SerializeField] 
    Transform _bossgenerateTransform;

    /// <summary>ノベルの状態</summary>
    [SerializeField]
    NovelPhase _novelPhaseState = NovelPhase.None;

    /// <summary>背景</summary>
    [SerializeField] 
    SpriteRenderer _backGround;
    /// <summary背景のコピー</summary>
    SpriteRenderer 
    _backGroundClone = null;
    
    /// <summary>背景の親オブジェクト</summary>
    [SerializeField]
    GameObject _backGroundParent;
    /// <summary>背景の単位ベクトル</summary>
    [SerializeField]
    Vector2 _backGroundDir = Vector2.down;
    /// <summary>背景のスクロールするスピード</summary>
    [SerializeField] float _scrollSpeed = 0.5f;
    /// <summary>背景のY座標の初期位置</summary>
    float _bgInitialPosY;

    /// <summary>ステージのデータ</summary>
    [SerializeField] 
    StageParam _stageParam;

    private int _phaseIndex = default;

    private bool _isGenerateFirstTime = true;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        _bgInitialPosY = _backGround.transform.position.y;

        _backGroundClone = Instantiate(_backGround, _backGroundParent.transform);
        _backGroundClone.transform.Translate(0f, _backGround.bounds.size.y, 0f);

        EnemyGenerate();
    }

    private void Update()
    {
        switch (_stageParam.PhaseParms[_phaseIndex].IsBoss)
        {
            case true:
                Debug.Log("ボス開始");
                _novelPhaseState = NovelPhase.Before;

                break;

            case false:
                BackGround();

                //ゲームオーバーを判定する
                if(GameManager.Instance.IsGameOver)
                {
                    //ゲームオーバUI表示
                    _gameOverCanavas.gameObject.SetActive(true);
                    //UIキャンバス
                    _uiCanvas.gameObject.SetActive(false);
                    return;
                }
                break;
        }
    }

    void EnemyGenerate()
    {

    }

    /// <summary>
    /// ボスステージの処理
    /// </summary>
    void BossStage()
    {
        if (GameManager.Instance.IsStageClear)
        {
            _novelPhaseState = NovelPhase.Win;
        }

        if (GameManager.Instance.IsGameOver)
        {
            _novelPhaseState = NovelPhase.Lose;
        }
    }


    void Novel(NovelPhase novelPhase)
    {
        switch (novelPhase)
        {
            case NovelPhase.Before:

                if (_beforeNovelRenderer.gameObject.activeSelf == false)
                {
                    _beforeNovelRenderer.gameObject.SetActive(true);
                }

                if (!_beforeGSSReader.IsLoading)
                {
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(_beforeNovelRenderer.IsNovelFinish)
                {
                    _novelCanvas.gameObject.SetActive(false);
                    _beforeNovelRenderer.gameObject.SetActive(false);
                    _uiCanvas.gameObject.SetActive(false);
                    _novelPhaseState = NovelPhase.None;
                    Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _bossgenerateTransform.position;
                }

                break;

            case NovelPhase.Win:

                if(_winNovelRenderer.gameObject.activeSelf == false)
                {
                    _winNovelRenderer.gameObject.SetActive(true);
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(!_winGSSReader.IsLoading)
                {
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(_winNovelRenderer.IsNovelFinish)
                {
                    _novelCanvas.gameObject.SetActive(false);
                    _winNovelRenderer.gameObject.SetActive(false);
                    _uiCanvas.gameObject.SetActive(false);
                    _gameClearCanvas.gameObject.SetActive(true);
                }

                break;

            case NovelPhase.Lose:

                if(_loseNovelRenderer.gameObject.activeSelf == false)
                {
                    _loseNovelRenderer.gameObject.SetActive(true);
                }

                if (!_loseGSSReader.IsLoading)
                {
                    _novelCanvas.gameObject.SetActive(true);
                }

                if(_loseNovelRenderer.IsNovelFinish)
                {
                    _novelCanvas.gameObject.SetActive(false);
                    _loseNovelRenderer.gameObject.SetActive(false);
                    _uiCanvas.gameObject.SetActive(false);
                    _gameOverCanavas.gameObject.SetActive(true);
                }

                break;
        }
    }

    void BackGround()
    {
        _backGround.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);
        _backGroundClone.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);

        if(_backGround.transform.position.y < _bgInitialPosY - _backGround.bounds.size.y)
        {
            _backGround.transform.Translate(0f, _backGround.bounds.size.y * 2, 0f);
        }

        if(_backGroundClone.transform.position.y < _bgInitialPosY - _backGroundClone.size.y)
        {
            _backGroundClone.transform.Translate(0f, _backGroundClone.size.y * 2, 0f);
        }
    }
}

public enum NovelPhase
{
    /// <summary>ノベルを読み込まない状態</summary>
    None,
    /// <summary>戦闘前ノベル</summary>
    Before,
    /// <summary>戦闘後ノベル</summary>
    Win,
    /// <summary>負けノベル</summary>
    Lose
}