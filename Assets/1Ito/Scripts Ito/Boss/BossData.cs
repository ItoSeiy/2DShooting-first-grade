using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ボスのデータが格納されたクラス
/// </summary>
[Serializable]
public class BossData
{
    /// <summary>ボスの行動</summary>
    public List<BossAcitionPattern> ActionPatterns => actionPatterns;
    /// <summary>必殺技の配列</summary>
    public List<BossAttackAction> BossSuperAttackActions => bossSuperAttackActons;

    [SerializeField,Header("BossAttackActionとBossAttackActionは同数にすること")]
    private List<BossAcitionPattern> actionPatterns = new List<BossAcitionPattern>();

    [SerializeField, Header("必殺技の行動(基本的に2つ)")]
    private List<BossAttackAction> bossSuperAttackActons = new List<BossAttackAction>(); 
}

/// <summary>
/// ボスの行動が格納されたクラス
/// </summary>
[Serializable]
public class BossAcitionPattern
{
    /// <summary>攻撃の配列</summary>
    public  List<BossAttackAction> BossAttackActions => bossAttackActions;
    /// <summary>移動の配列</summary>
    public List<BossMoveAction> BossMoveActions => bossMoveActions;

    [SerializeField, Header("攻撃の行動")]
    private List<BossAttackAction> bossAttackActions = new List<BossAttackAction>();

    [SerializeField, Header("移動の行動")]
    private List<BossMoveAction> bossMoveActions = new List<BossMoveAction>();

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