using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>ステージのデータを格納したスクリプタブルオブジェクトオブジェクト</summary>
[CreateAssetMenu(fileName = "PhaseParam")]
public class StageParamAsset : ScriptableObject
{
    //public List<StageParam> StageParams { get => stageParams; }

    [SerializeField] public List<StageParam> stageParams = new List<StageParam>();
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
    public float BoforeInterval { get => boforeInterval;}
    public float Interval { get => interval;}
    public float AfterInterval { get => afterInterval;}

    [SerializeField] private string PhaseName = "Phase";

    [SerializeField] private GameObject phasePrefab;
    [SerializeField] private float boforeInterval;
    [SerializeField] private float interval;
    [SerializeField] private float afterInterval;
}

