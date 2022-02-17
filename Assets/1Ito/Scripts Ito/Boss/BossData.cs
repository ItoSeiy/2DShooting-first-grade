using System;
using UnityEngine;

[Serializable]
public class BossData
{
    /// <summary> パターンの配列実態 </summary>
    [SerializeField]
    private Actions[] _actionPattern = default;

    /// <summary> パターンの参照 </summary>
    public Actions[] ActionPattern => _actionPattern;
}

[Serializable]
public class Actions
{
    /// <summary> 行動の配列 </summary>
    [SerializeField]
    private BossAction[] _bossActions = default;
    /// <summary> 行動配列の参照 </summary>
    public BossAction[] BossActions => _bossActions;
}