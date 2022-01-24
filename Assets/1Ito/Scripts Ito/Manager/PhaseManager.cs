using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    [SerializeField] StageState _stageState;
    [SerializeField] StageParamAsset paramAsset;

    private void Start()
    {
    }
    public void Test()
    {
    }
}

[System.Serializable]
public class StageState
{
    Stage Stage;
    Phase Phase;
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