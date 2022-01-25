using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] StageParamAsset _paramAsset;
    [SerializeField] Transform _generatePos;
    [SerializeField] Stage _stageState;
    [SerializeField] Phase _phaseState;

    private float _timer = 0;
    private float _middleTimer = 0;

    private float _beforeInterval = default;
    private float _afterInterval = default;

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

        if (_timer >= _paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].StartTime)
        {
            Debug.Log("生成待機");
            _middleTimer += Time.deltaTime;

            if(_middleTimer >= _paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].Interval || _isFirstTime)
            {
                Debug.Log("生成");
                Instantiate(_paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].Prefab).transform.position = _generatePos.position;
                Debug.Log(_paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].PhaseName);
                _middleTimer = 0;
                _isFirstTime = false;
            }

            if (_timer >= _paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].FinishTime)
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
        Debug.Log("ボス");
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