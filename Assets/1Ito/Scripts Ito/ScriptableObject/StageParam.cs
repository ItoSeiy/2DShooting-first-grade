using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>ステージのデータを格納したクラス</summary>
[Serializable]
public class StageParam
{
    public List<PhaseParm> PhaseParms => phaseParms;

    [SerializeField]
    private List<PhaseParm> phaseParms = new List<PhaseParm>();
}


/// <summary>フェイズのデータを格納したクラス</summary>
[Serializable]
public class PhaseParm
{
    public string PhaseName => PhaseName;
    public GameObject Prefab => phasePrefab;
    public int LoopTime => loopTime; 
    public bool IsBoss => isBoss;

    [SerializeField] 
    private string phaseName = "Phase";
    [SerializeField]
    private GameObject phasePrefab;
    [SerializeField, Header("このプレハブの生成がループするモードであったら何回生成するか")]
    private int loopTime;
    [SerializeField, Header("ボスだったらチェックを付ける")]
    private bool isBoss = false;
}

