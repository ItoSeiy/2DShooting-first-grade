using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>ステージのデータを格納したクラス</summary>
[Serializable]
public class StageParam
{
    public int BossPhaseIndex => bossPhaseIndex;
    public List<PhaseParm> PhaseParms => phaseParms;
    [SerializeField, Tooltip("ボスのフェイズのIndexを指定する(0から始まる数字)")]
    private int bossPhaseIndex;
    [SerializeField]
    private List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>フェイズのデータを格納したクラス</summary>
[Serializable]
public class PhaseParm
{
    public GameObject Prefab => phasePrefab;
    public float StartTime => startTime;
    public bool UseInterval => useInterval;
    public float Interval => interval;
    public float FinishTime => finishTime;

    [SerializeField] 
    public string PhaseName = "Phase";
    [SerializeField]
    private GameObject phasePrefab;
    [SerializeField]
    private float startTime;
    [SerializeField, Tooltip("インターバルを用いて何度も生成するか")]
    private bool useInterval;
    [SerializeField, Tooltip("UseIntevalにチェックが入っていないと使用できない")]
    private float interval;
    [SerializeField]
    private float finishTime;
}

