using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ボス敵を管理するステートパターンのスクリプト
/// 
/// ボスには"アクション(Action)"という行動(攻撃や移動)
/// が一つ一つ作られている
/// 
/// それをまとめて"パターン(Pattern)"としているものがある
/// </summary>
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

    /// <summary>行動"パターン"のインデックス</summary>
    private int _patternIndex = -1;
    /// <summary>"アクション"のインデックス</summary>
    private int _actionIndex = 0;

    /// <summary>ボスのオブジェクト</summary>
    public GameObject Model { get => gameObject; }

    protected override void Awake()
    {
        base.Awake();
        foreach (var pattern in _data.ActionPattern)
        {
            foreach (var attackAction in pattern.BossAttackActions)
            {
                attackAction.ActinoEnd = () =>
                {
                    Debug.Log("行動パターンを次に移ります");
                    _actionIndex++;

                    if(_actionIndex >= _data.ActionPattern[_patternIndex].BossAttackActions.Length)
                    {
                        //すべてのアクションを実行しきったら
                        RandomPatternChange();//行動パターンを変える
                    }
                    else//アクションを実行しきっていなかったら
                    {

                    }
                };
            }
        }
    }

    protected override void Update()
    {
        _currentAttackAction?.ManagedUpdate(this);
        _currentMoveAction?.ManagedUpdate(this);
    }

    /// <summary>
    /// 行動パターンをランダムに変える
    /// </summary>
    private void RandomPatternChange()
    {

    }

    private void ChangeAction()
    {
        //現在のアクションの最後に行う関数を呼ぶ
        _currentAttackAction?.Exit(this);
        _currentMoveAction?.Exit(this);

        //アクションの中身を切り替える
    }

    private void ExecutionAction()
    {

    }

    protected override void OnGetDamage()
    {
    }
}
