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

    private bool _finishStage = false;

    private void Start()
    {
        _timer = 0;
        _middleTimer = 0;
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

        EnemyGenerate();    
    }

    void EnemyGenerate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].BoforeInterval)
            return;

        _middleTimer += Time.deltaTime;
        if(_middleTimer >= _paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].Interval)
        {
            Instantiate(_paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].Prefab).transform.position = _generatePos.position;
            Debug.Log(_paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].PhaseName);
            _middleTimer = 0;
        }


        if (_timer >= _paramAsset.StageParams[_stageIndex].phaseParms[_phaseIndex].AfterInterval)
        {
            _timer = 0;
            ChangeState((Phase)_phaseIndex + 1);
        }
    }

    void BossStage()
    {

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
    Stage01, 
    Stage02,
    Stage03,
    Stage04,
    Stage05,
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