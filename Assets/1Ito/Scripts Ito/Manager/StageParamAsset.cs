using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PhaseParam")]
public class StageParamAsset : ScriptableObject
{
    [SerializeField] List<StageParam> StageParams = new List<StageParam>();
}

[System.Serializable]
public class StageParam
{
    [SerializeField] string Stage = "Stage";
    [SerializeField] List<PhaseParm> PhaseParms = new List<PhaseParm>();
}


[System.Serializable]
public class PhaseParm
{
    [SerializeField] string Phase = "Phase";
    [SerializeField] public GameObject PhasePrefab;
    [SerializeField] public float BoforeInterval;
    [SerializeField] public float Interval;
    [SerializeField] public float AfterInterval;
}

