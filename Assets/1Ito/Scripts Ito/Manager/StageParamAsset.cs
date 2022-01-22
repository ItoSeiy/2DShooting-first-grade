using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>ステージのデータを格納したスクリプタブルオブジェクトオブジェクト</summary>
[CreateAssetMenu(fileName = "PhaseParam")]
public class StageParamAsset : ScriptableObject
{
    public List<StageParam> StageParams { get => stageParams; }

    [SerializeField] private List<StageParam> stageParams = new List<StageParam>();
}


/// <summary>フェイズのデータを格納したクラス</summary>
[Serializable]
public class StageParam
{
    public List<PhaseParm> PhaseParms { get => phaseParms;}

    [SerializeField] string StageName = "Stage";

    [SerializeField] private List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>フェイズそのもののデータを格納したクラス</summary>
[Serializable]
public class PhaseParm
{
    public GameObject PhasePrefab { get => phasePrefab;}
    public float BoforeInterval { get => boforeInterval;}
    public float Interval { get => interval;}
    public float AfterInterval { get => afterInterval;}

    [SerializeField] private string PhaseName = "Phase";

    [SerializeField] private GameObject phasePrefab;
    [SerializeField] private float boforeInterval;
    [SerializeField] private float interval;
    [SerializeField] private float afterInterval;
}

