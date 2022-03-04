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
    public bool IsBoss => _isBoss;

    [SerializeField] 
    private string phaseName = "Phase";
    [SerializeField]
    private GameObject phasePrefab;
    [SerializeField, Header("ボスだったらチェックを付ける")]
    private bool _isBoss = false;
}

