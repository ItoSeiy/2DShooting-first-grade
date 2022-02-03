using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseNovelManager : SingletonMonoBehaviour<PhaseNovelManager>
{
    public GamePhase GamePhaseState => _gamePhaseState;
    public NovelPhase NovelePhaesState => _novelPhaseState;

    [SerializeField] NovelRenderer _beforeNovelRenderer;
    [SerializeField] NovelRenderer _afterNovelRenderer;
    [SerializeField] NovelRenderer _loseNovelRenderer;

    [SerializeField] Canvas _beforeNovelCanvas;
    [SerializeField] Canvas _afterNovelCanvas;
    [SerializeField] Canvas _loseNovelCanvas;

    [SerializeField] Transform _generatePos;

    [SerializeField] GamePhase _gamePhaseState;
    [SerializeField] NovelPhase _novelPhaseState;

    [SerializeField] StageParam _stageParam;

    private float _timer = 0;
    private float _intervalTimer = 0;

    private int _phaseIndex = default;

    private bool _isFirstTime = true;

    protected override void Awake()
    {
        base.Awake();
        SetUp();
    }

    private void Update()
    {
        switch (_gamePhaseState)
        {
            case GamePhase.Boss:
                Debug.Log("ボス開始");
                BossStage();
                break;

            case GamePhase.End:
                Debug.Log("フェイズ終了");
                break;

            default:
                EnemyGenerate();
                break;
        }
    }

    void EnemyGenerate()
    {
        _timer += Time.deltaTime;
        Debug.Log("スタート待機");

        if (_timer >= _stageParam.PhaseParms[_phaseIndex].StartTime)
        {
            Debug.Log("生成待機");
            _intervalTimer += Time.deltaTime;

            if (_intervalTimer >= _stageParam.PhaseParms[_phaseIndex].Interval || _isFirstTime)
            {
                Debug.Log("生成");
                Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _generatePos.position;
                Debug.Log(_stageParam.PhaseParms[_phaseIndex].PhaseName);
                _intervalTimer = 0;
                _isFirstTime = false;
            }

            if (_timer >= _stageParam.PhaseParms[_phaseIndex].FinishTime)
            {
                Debug.Log("生成終了");
                _timer = 0;
                _isFirstTime = true;
                ChangePhase((GamePhase)_phaseIndex + 1);
            }
        }

    }

    void BossStage()
    {
        StartNovel(_novelPhaseState);

        if(_beforeNovelRenderer.NovelFinish && _isFirstTime)
        {
            Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab);
            _isFirstTime = false;
        }


        if(GameManager.Instance.IsGameOver && _isFirstTime)
        {
            _novelPhaseState = NovelPhase.Lose;
            _isFirstTime = false;
        }

        
    }

    /// <summary>
    ///　ステージが読み込まれるたびに呼ばれる
    /// </summary>
    public void SetUp()
    {
        _isFirstTime = true;
        _timer = 0;
        _intervalTimer = 0;

        _gamePhaseState = GamePhase.Phase01;
        _phaseIndex = (int)_gamePhaseState;

        _novelPhaseState = NovelPhase.Before;
    }
    
    void ChangePhase(GamePhase phase)
    {
        _gamePhaseState = phase;

        _phaseIndex = (int)_gamePhaseState;
    }

    void StartNovel(NovelPhase novelPhase)
    {
        switch (novelPhase)
        {
            case NovelPhase.None:
                return;

            case NovelPhase.Before:

                _beforeNovelRenderer.gameObject.SetActive(true);
                _beforeNovelCanvas.gameObject.SetActive(true);
                _novelPhaseState = NovelPhase.None;

                break;

            case NovelPhase.After:

                _afterNovelRenderer.gameObject.SetActive(true);
                _afterNovelCanvas.gameObject.SetActive(true);
                _novelPhaseState = NovelPhase.None;

                break;

            case NovelPhase.Lose:

                _loseNovelRenderer.gameObject.SetActive(true);
                _loseNovelCanvas.gameObject.SetActive(true);
                _novelPhaseState = NovelPhase.None;

                break;
        }
    }

}

public enum GamePhase
{
    Phase01,
    Phase02,
    Phase03,
    Phase04,
    Phase05,
    Boss,
    End
}

public enum NovelPhase
{
    None,
    Before,
    After,
    Lose
}