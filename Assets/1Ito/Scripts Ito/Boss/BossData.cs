using System;
using UnityEngine;

/// <summary>
/// ボスのデータが格納されたクラス
/// </summary>
[Serializable]
public class BossData
{
    /// <summary>ボスの行動</summary>
    public BossAcitionPattern[] ActionPatterns => actionPatterns;
    /// <summary>必殺技の配列</summary>
    public BossAttackAction[] BossSuperAttackActions => bossSuperAttackActons;

    [SerializeField,Header("BossAttackActionとBossAttackActionは同数にすること")]
    private BossAcitionPattern[] actionPatterns = default;

    [SerializeField, Header("必殺技の行動(基本的に2つ)")]
    private BossAttackAction[] bossSuperAttackActons = default; 
}

/// <summary>
/// ボスの行動が格納されたクラス
/// </summary>
[Serializable]
public class BossAcitionPattern
{
    /// <summary>攻撃の配列</summary>
    public BossAttackAction[] BossAttackActions => bossAttackActions;
    /// <summary>移動の配列</summary>
    public BossMoveAction[] BossMoveActions => bossMoveActions;

    [SerializeField, Header("攻撃の行動")]
    private BossAttackAction[] bossAttackActions = default;

    [SerializeField, Header("移動の行動")]
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
    public abstract void Enter(BossController contlloer);
    /// <summary> この行動Update処理 </summary>
    public abstract void ManagedUpdate(BossController contlloer);
    /// <summary> この行動から出る時の処理 </summary>
    public abstract void Exit(BossController contlloer);
}

/// <summary>
/// ボスの"移動"の基底クラス
/// </summary>
public abstract class BossMoveAction : MonoBehaviour
{
    /// <summary> この行動に入って来た時の処理 </summary>
    public abstract void Enter(BossController contlloer);
    /// <summary> この行動Update処理 </summary>
    public abstract void ManagedUpdate(BossController contlloer);
    /// <summary> この行動から出る時の処理 </summary>
    public abstract void Exit(BossController contlloer);
}