using System;
using UnityEngine;

[Serializable]
public class BossData
{
    /// <summary>行動パターン</summary>
    public Actions[] ActionPattern => _actionPattern;

    [SerializeField]
    private Actions[] _actionPattern = default;

}

[Serializable]
public class Actions
{
    /// <summary>攻撃の配列</summary>
    public BossAttackAction[] BossActions => _bossActions;

    [SerializeField]
    private BossAttackAction[] _bossActions = default;
}

/// <summary>
/// ボスの"攻撃"の基底クラス
/// </summary>
public abstract class BossAttackAction : MonoBehaviour
{
    /// <summary></summary>
    public abstract Action ActinoEnd { get; set; }
    /// <summary> この行動に入って来た時の処理 </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> この行動Update処理 </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> この行動から出る時の処理 </summary>
    public abstract void Exit(BossEnemyController contlloer);
}

/// <summary>
/// ボスの"移動"の基底クラス
/// </summary>
public abstract class BossMoveAction : MonoBehaviour
{
    public abstract Action ActinoEnd { get; set; }
    /// <summary> この行動に入って来た時の処理 </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> この行動Update処理 </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> この行動から出る時の処理 </summary>
    public abstract void Exit(BossEnemyController contlloer);
}