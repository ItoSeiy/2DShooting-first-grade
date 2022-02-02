using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : SingletonMonoBehaviour<PhaseManager>
{
    public Phase PhaseState => _phaseState;

    [SerializeField] NovelManager _novelManager;
    [SerializeField] GSSReader _gssReader;
    [SerializeField] Canvas _novelCanvas;
    [SerializeField] Transform _generatePos;
    [SerializeField] Phase _phaseState;
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
        switch (_phaseState)
        {
            case Phase.Boss:
                Debug.Log("ボス開始");
                BossStage();
                break;

            case Phase.End:
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
                ChangePhase((Phase)_phaseIndex + 1);
            }
        }

    }

    void BossStage()
    {
        _novelManager.gameObject.SetActive(true);
        _novelCanvas.gameObject.SetActive(true);

        if(_novelManager.NovelFinish)
        {
            _novelManager.gameObject.SetActive(false);
            _novelCanvas.gameObject.SetActive(false);


        }

        _phaseState = Phase.End;
    }

    /// <summary>
    ///　
    /// </summary>
    public void SetUp()
    {
        _isFirstTime = true;
        _timer = 0;
        _intervalTimer = 0;

        _phaseState = Phase.Phase01;
        _phaseIndex = (int)_phaseState;
    }
    
    void ChangePhase(Phase phase)
    {
        _phaseState = phase;

        _phaseIndex = (int)_phaseState;
    }

}

public enum Phase
{
    Phase01,
    Phase02,
    Phase03,
    Phase04,
    Phase05,
    Boss,
    End
}
