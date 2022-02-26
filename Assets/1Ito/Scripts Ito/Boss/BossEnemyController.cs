using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyController : EnemyBase
{
    /// <summary> 敵の行動データ </summary>
    public BossData Data => _data;
    /// <summary> アニメーター </summary>
    public Animator Animator { get; private set; } = default;

    /// <summary> ボス行動データ </summary>
    [SerializeField]
    BossData _data = null;

    /// <summary> 現在の行動 </summary>
    private BossAttackAction _currentBossAction = default;
    /// <summary> 最後に行った行動パターンインデックス </summary>
    private int _lastPattern = -1;
    /// <summary> プレイヤー参照本体 </summary>
    private GameObject _player = default;
    /// <summary> 敵のモデル </summary>
    public GameObject Model { get => gameObject; }

    public GameObject PlayerObj
    {
        get
        {
            if (_player == null)
            {
                Debug.LogWarning("Playerタグを持ったオブジェクトがありません。\n追加してください");
                return null;
            }
            return _player;
        }
    }

    protected override void Attack()
    {
        //ObjectPool.Instance.UseObject(pos, objType);
    }

    protected override void OnGetDamage()
    {
    }


}
