using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] StageParamAsset paramAsset;

    public Stage _stageState;
    Phase _phaseState;

    private void Start()
    {
        while(_stageState != Stage.End)
        {
            Debug.Log("Test");
        }
    }
}

public enum Stage
{
    Stage01, 
    Stage02,
    Stage03,
    Stage04,
    Stage05,

    End
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