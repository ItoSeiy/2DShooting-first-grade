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


/// <summary>ステージのデータを格納したクラス</summary>
[Serializable]
public class StageParam
{
    [SerializeField] string StageName = "Stage";

    [SerializeField] public List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>フェイズのデータを格納したクラス</summary>
[Serializable]
public class PhaseParm
{
    public GameObject Prefab { get => phasePrefab;}
    public float StartTime { get => startTime;}
    public float Interval { get => interval;}
    public float FinishTime { get => finishTime;}

    [SerializeField] public string PhaseName = "Phase";

    [SerializeField] private GameObject phasePrefab;
    [SerializeField] private float startTime;
    [SerializeField] private float interval;
    [SerializeField] private float finishTime;
}

