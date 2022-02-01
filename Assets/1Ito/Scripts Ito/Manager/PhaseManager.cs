using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] NovelManager _novelManager;
    [SerializeField] Canvas _novelCanvas;
    [SerializeField] Transform _generatePos;
    [SerializeField] Stage _stageState;
    [SerializeField] Phase _phaseState;
    [SerializeField] List<StageParam> _stageParam = new List<StageParam>();

    private float _timer = 0;
    private float _middleTimer = 0;

    private int _stageIndex = default;
    private int _phaseIndex = default;

    private bool _isFirstTime = true;

    private void Start()
    {
        _isFirstTime = true;
        _timer = 0;
        _middleTimer = 0;
        ChangeState();
    }

    private void Update()
    {
        switch (_phaseState)
        {
            case Phase.Boss:
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

        if (_timer >= _stageParam[_stageIndex].PhaseParms[_phaseIndex].StartTime)
        {
            Debug.Log("生成待機");
            _middleTimer += Time.deltaTime;

            if (_middleTimer >= _stageParam[_stageIndex].PhaseParms[_phaseIndex].Interval || _isFirstTime)
            {
                Debug.Log("生成");
                Instantiate(_stageParam[_stageIndex].PhaseParms[_phaseIndex].Prefab).transform.position = _generatePos.position;
                Debug.Log(_stageParam[_stageIndex].PhaseParms[_phaseIndex].PhaseName);
                _middleTimer = 0;
                _isFirstTime = false;
            }

            if (_timer >= _stageParam[_stageIndex].PhaseParms[_phaseIndex].FinishTime)
            {
                Debug.Log("生成終了");
                _timer = 0;
                _isFirstTime = true;
                ChangeState((Phase)_phaseIndex + 1);
            }
        }

    }

    void BossStage()
    {
        _novelManager.gameObject.SetActive(true);
        _novelCanvas.gameObject.SetActive(true);
        if(_novelManager.NovelFinish)
        {

        }
    }

    void ChangeState()
    {
        _stageIndex = (int)_stageState;
        _phaseIndex = (int)_phaseState;
    }
    
    void ChangeState(Phase phase)
    {
        _phaseState = phase;

        _phaseIndex = (int)_phaseState;
    }

    void ChangeState(Stage stage, Phase phase)
    {
        _stageState = stage;
        _phaseState = phase;

        _stageIndex = (int)_stageState;
        _phaseIndex = (int)_phaseState;
    }
}

public enum Stage
{
    Stage01 = 0, 
    Stage02,
    Stage03,
    Stage04,
    Stage05,
}

public enum Phase
{
    Phase01 = 0,
    Phase02,
    Phase03,
    Phase04,
    Phase05,

    Boss,

    End
}