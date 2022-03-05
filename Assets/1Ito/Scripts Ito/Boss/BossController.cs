using System.Collections;
using System.Linq;
using UnityEngine;

/// <summary>
/// ボス敵を管理するステートパターンのスクリプト
/// 
/// ボスには"アクション(Action)"という行動(攻撃や移動)
/// が一つ一つ作られている
/// 
/// それをまとめて"パターン(Pattern)"としているものがある
/// </summary>
public class BossController : EnemyBase
{
    //[SerializeField, Header("死亡時のパーティクル")]
    //ParticleSystem _
    /// <summary>ボスのデータ</summary>
    public BossData Data => _data;
    /// <summary>ボスのデータ</summary>
    [SerializeField]
    BossData _data = null;

    [SerializeField, Header("設定した数値以下になると必殺技を発動する(基本的に2つ)")]
    float[] _superAttackTimingHp = default;
    private int _timingIndex = 0;

    /// <summary>キャストの判定</summary>
    public bool IsCast { get; private set; } = default;

    /// <summary> アニメーター </summary>
    public Animator Animator { get; private set; } = default;

    /// <summary>現在の攻撃行動</summary>
    private BossAttackAction _currentAttackAction = default;
    /// <summary>現在の移動行動</summary>
    private BossMoveAction _currentMoveAction = default;
    /// <summary>現在の</summary>
    private BossAttackAction _currentSuperAttackAction = default;

    /// <summary>行動"パターン"のインデックス</summary>
    private int _patternIndex = -1;
    /// <summary>"アクション"のインデックス</summary>
    private int _actionIndex = 0;
    protected override void Awake()
    {
        base.Awake();
        //アクション終了時の処理を登録する
        _data.ActionPatterns.ForEach(x => x.BossAttackActions.ForEach(x => x.ActinoEnd = JudgeAction));

        //必殺技終了時の行動を登録する
        _data.BossSuperAttackActions.ForEach(x => x.ActinoEnd = () =>
        {
            //終了時に呼び出される関数を呼ぶ
            _currentSuperAttackAction.Exit(this);
            //中身を空にする(Updateの内容が実行されないようにするため)
            _currentSuperAttackAction = null;
            //普段の行動パターンに戻る
            RandomPatternChange();
        });

        //必殺技を発動するHPのタイミングの設定ミスを防ぐために降順(大きい順)に並べ替える
        _superAttackTimingHp = _superAttackTimingHp.OrderByDescending(x => x).ToArray();
    }

    void Start()
    {
        RandomPatternChange();    
    }

    protected override void Update()
    {
        if(IsCast)
        {
            Debug.Log($"{_currentAttackAction.gameObject.name},と{_currentMoveAction.gameObject.name}\nUpdateの実行はキャスト中のため行っていません");
            return;
        }
        _currentAttackAction?.ManagedUpdate(this);
        _currentMoveAction?.ManagedUpdate(this);
        _currentSuperAttackAction?.ManagedUpdate(this);
    }

    protected override void OnGetDamage()
    {
        if(_timingIndex >= _data.BossSuperAttackActions.Count)
        {
            //必殺技を必要以上に発動させないために配列の要素数以上の時にアクセスしたらReturnする
            return;
        }

        //必殺わざを発動するHpに到達したら必殺技を実行する
        if(_superAttackTimingHp[_timingIndex] >= EnemyHp) SuperAttack();
    }
    
    /// <summary>
    /// 必殺技の処理
    /// </summary>
    private void SuperAttack()
    {
        Debug.Log($"ボスのHPは{EnemyHp}\n{_timingIndex}番目の必殺技を実行します");

        /// 通常時のアクションと必殺技の最後に行う関数を呼ぶ
        _currentAttackAction?.Exit(this);
        _currentMoveAction?.Exit(this);
        _currentSuperAttackAction?.Exit(this);

        /// 通常時のアクションと必殺技を空にして
        _currentAttackAction = null;
        _currentMoveAction = null;
        _currentSuperAttackAction = null;

        /// 必殺技のアクションを追加する
        _currentSuperAttackAction = _data.BossSuperAttackActions[_timingIndex];
        //必殺技の初回の動きを実行する
        _currentSuperAttackAction.Enter(this);

        _timingIndex++;
    }

    /// <summary>
    /// 現在行っているアクションを判定する
    /// それに応じてパターン又はアクションを切り替える
    /// </summary>
    private void JudgeAction()
    {
        Debug.Log("アクションを判定します");
        _actionIndex++;

        if(_actionIndex >= _data.ActionPatterns[_patternIndex].BossAttackActions.Count)
        {
            //行動パターンを実行しきったら
            RandomPatternChange();//行動パターンを変える
        }
        else//行動パターンを実行しきっていなかったら
        {
            //アクションを次のものに切り替える
            ActionChange(_data.ActionPatterns[_patternIndex].BossAttackActions[_actionIndex],
                            _data.ActionPatterns[_patternIndex].BossMoveActions[_actionIndex]);
        }
    }

    /// <summary>
    /// 行動パターンをランダムに変える
    /// </summary>
    private void RandomPatternChange()
    {
        //行動パターンのインッデクスを決める
        _patternIndex = Random.Range(0, _data.ActionPatterns.Count);
        Debug.Log($"パターン{_patternIndex}を実行する");

        //アクションはリセットされるためIndexを0にする
        _actionIndex = 0;

        //アクションを変更し実行する
        ActionChange(_data.ActionPatterns[_patternIndex].BossAttackActions.FirstOrDefault(),
                     _data.ActionPatterns[_patternIndex].BossMoveActions.FirstOrDefault());
    }

    /// <summary>
    /// アクションを変更し実行する
    /// </summary>
    /// <param name="bossAttackAction"></param>
    /// <param name="bossMoveAction"></param>
    private void ActionChange(BossAttackAction bossAttackAction, BossMoveAction bossMoveAction)
    {
        //現在のアクションの最後に行う関数実行する
        _currentAttackAction?.Exit(this);
        _currentMoveAction?.Exit(this);

        //アクションの中身を切り替える
        _currentAttackAction = bossAttackAction;
        _currentMoveAction = bossMoveAction;

        Debug.Log($"{_currentAttackAction.gameObject.name},と{_currentMoveAction.gameObject.name}にアクションを切り替えます");

        //切り替えた後のアクションの最初に行う関数を実行する
        _currentAttackAction?.Enter(this);
        _currentMoveAction?.Enter(this);
    }

    protected override void OnGameZoneTag(Collider2D collision)
    {
        //ボスはゲームゾーンに触れても破棄してほしくないため記述をしない
    }

    protected override void OnKilledByPlayer()
    {

        GameManager.Instance.StageClear();
        base.OnKilledByPlayer();
    }

    /// <summary>
    /// 待つことを実行する関数
    /// アップデートの実行を待つ
    /// </summary>
    /// <param name="castTime">待つ時間</param>
    public void Cast(float castTime)
    {
        StopCoroutine(Casting(castTime));
        StartCoroutine(Casting(castTime));
    }

    private IEnumerator Casting(float castTime)
    {
        IsCast = true;

        float timer = 0;
        while(timer <= castTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        IsCast = false;
    }

}
