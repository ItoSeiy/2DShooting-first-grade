using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>ステージのデータを格納したクラス</summary>
[Serializable]
public class StageParam
{
    public List<PhaseParm> PhaseParms => phaseParms;
    [SerializeField] private List<PhaseParm> phaseParms = new List<PhaseParm>();

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

