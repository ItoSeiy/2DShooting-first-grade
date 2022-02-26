using System;
using UnityEngine;

/// <summary>
/// ボスのデータが格納されたクラス
/// </summary>
[Serializable]
public class BossData
{
    /// <summary>ボスの行動</summary>
    public Actions[] ActionPattern => actionPattern;

    [SerializeField,Header("BossAttackActionとBossAttackActionは同数にすること")]
    private Actions[] actionPattern = default;

}

/// <summary>
/// ボスの行動が格納されたクラス
/// </summary>
[Serializable]
public class Actions
{
    /// <summary>攻撃の配列</summary>
    public BossAttackAction[] BossAttackActions => bossAttackActions;
    /// <summary>移動の配列</summary>
    public BossMoveAction[] BossMoveActions => bossMoveActions;

    [SerializeField]
    private BossAttackAction[] bossAttackActions = default;

    [SerializeField]
    private BossMoveAction[] bossMoveActions = default;
}

/// <summary>
/// ボスの"攻撃"の基底クラス
/// </summary>
public abstract class BossAttackAction : MonoBehaviour
{
    /// <summary>
    /// やりたい行動の終了時にActonEnd?.Invoke();と記載する
    /// そうすることで次の動きに移行する
    /// </summary>
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
    /// <summary> この行動に入って来た時の処理 </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> この行動Update処理 </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> この行動から出る時の処理 </summary>
    public abstract void Exit(BossEnemyController contlloer);
}