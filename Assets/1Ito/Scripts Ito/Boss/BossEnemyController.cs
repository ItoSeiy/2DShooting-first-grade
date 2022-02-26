using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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
                        //行動パターンを実行しきったら
                        RandomPatternChange();//行動パターンを変える
                    }
                    else//行動パターンを実行しきっていなかったら
                    {
                        //アクションを次のものに切り替える
                        ChangeAction(_data.ActionPattern[_patternIndex].BossAttackActions[_actionIndex],
                                     _data.ActionPattern[_patternIndex].BossMoveActions[_actionIndex]);
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
        //行動パターンのインッデクスを決める
        _patternIndex = Random.Range(0, _data.ActionPattern.Length);
        Debug.Log($"パターン{_patternIndex}を実行する");

        //アクションを変更し実行する
        ChangeAction(_data.ActionPattern[_patternIndex].BossAttackActions[_actionIndex],
                     _data.ActionPattern[_patternIndex].BossMoveActions[_actionIndex]);
    }

    /// <summary>
    /// アクションを変更し実行する
    /// </summary>
    /// <param name="bossAttackAction"></param>
    /// <param name="bossMoveAction"></param>
    private void ChangeAction(BossAttackAction bossAttackAction, BossMoveAction bossMoveAction)
    {
        //現在のアクションの最後に行う関数を呼ぶ
        _currentAttackAction?.Exit(this);
        _currentMoveAction?.Exit(this);

        //アクションの中身を切り替える
        _currentAttackAction = bossAttackAction;
        _currentMoveAction = bossMoveAction;

        //切り替えた後のアクションを実行する
        _currentAttackAction.Enter(this);
        _currentAttackAction.Enter(this);
    }

    protected override void OnGetDamage()
    {
    }
}
