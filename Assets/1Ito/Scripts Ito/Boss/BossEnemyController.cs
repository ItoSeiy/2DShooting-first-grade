using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyBase
{
    /// <summary>ボスのデータ</summary>
    public BossData Data => _data;
    /// <summary> アニメーター </summary>
    public Animator Animator { get; private set; } = default;
  
    /// <summary>ボスのデータ</summary>
    [SerializeField]
    BossData _data = null;

    /// <summary>現在の攻撃行動</summary>
    private BossAttackAction _currentAttackAction = default;
    /// <summary>現在の移動行動</summary>
    private BossMoveAction _currentMoveAction = default;
    /// <summary>行動パターンインデックス </summary>
    private int _patternIndex = -1;
    /// <summary>ボスのオブジェクト</summary>
    public GameObject Model { get => gameObject; }

    protected override void Awake()
    {
        base.Awake();
        foreach (var pattern in _data.ActionPattern)
        {
            foreach (var attackAction in pattern.BossAttackActions)
            {

            }
            foreach(var moveAction in pattern.BossMoveActions)
            {

            }
        }
    }

    protected override void Update()
    {
        _currentAttackAction?.ManagedUpdate(this);
        _currentMoveAction?.ManagedUpdate(this);
    }

    protected override void Attack()
    {
        //ObjectPool.Instance.UseObject(pos, objType);
    }

    protected override void OnGetDamage()
    {
    }
}
