using System;
using UnityEngine;

/// <summary>
/// ボスの行動基底クラス
/// </summary>
public abstract class BossAction : MonoBehaviour
{
    public abstract Action ActinoEnd { get; set; }
    /// <summary> この行動に入って来た時の処理 </summary>
    public abstract void Enter(BossEnemyController contlloer);
    /// <summary> この行動Update処理 </summary>
    public abstract void ManagedUpdate(BossEnemyController contlloer);
    /// <summary> この行動から出る時の処理 </summary>
    public abstract void Exit(BossEnemyController contlloer);
}