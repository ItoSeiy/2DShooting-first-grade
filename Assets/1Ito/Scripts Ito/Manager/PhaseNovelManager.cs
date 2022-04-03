using Overdose.Novel;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;
using System;
using Overdose.Data;

public class PhaseNovelManager : SingletonMonoBehaviour<PhaseNovelManager>
{
    public NovelPhase NovelePhaesState => _novelPhase;

    [SerializeField, Header("ノベル前に待つ時間")]
    float _novelWaitTime = 5f;

    [SerializeField, Header("ゲームオーバー後にUI等の表示を待つ時間(ミリ秒)")]
    int _gameOverUIWaitTime = 1000;

    /// <summary>スプレッドシートシートを読み込むスクリプト(戦闘前)</summary>
    [SerializeField]
    GSSReader _beforeGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(戦闘前)</summary>
    [SerializeField]
    NovelRenderer _beforeNovelRenderer;

    /// <summary>スプレッドシートシートを読み込むスクリプト(勝利時)</summary>
    [SerializeField] 
    GSSReader _winGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(戦闘前)</summary>
    [SerializeField] 
    NovelRenderer _winNovelRenderer;

    /// <summary>スプレッドシートシートを読み込むスクリプト(敗北時)</summary>
    [SerializeField] 
    GSSReader _loseGSSReader;
    /// <summary>ノベルを書き出し、制御するスクリプト(敗北時)</summary>
    [SerializeField] 
    NovelRenderer _loseNovelRenderer;

    /// <summary>ノベルを映し出すキャンバス</summary>
    [SerializeField]
    Canvas _novelCanvas;

    /// <summary>ゲームオーバー時に出すUI</summary>
    [SerializeField]
    Canvas _gameOverCanavas;
    /// <summary>ゲームクリア時に出すUI</summary>
    [SerializeField]
    Canvas _gameClearCanvas;

    /// <summary>常にスコアなどが表示されているキャンバス</summary>
    [SerializeField] 
    Canvas _uiCanvas;

    /// <summary>モブ敵の出現場所</summary>
    [SerializeField] 
    Transform _enemyGeneretePos;
    /// <summary>ボスの出現位置</summary>
    [SerializeField] 
    Transform _bossGeneretePos;

    /// <summary>ノベルの状態</summary>
    [SerializeField]
    NovelPhase _novelPhase = NovelPhase.None;

    /// <summary>背景</summary>
    [SerializeField] 
    SpriteRenderer _backGround;
    /// <summary背景のコピー</summary>
    SpriteRenderer 
    _backGroundClone = null;
    
    /// <summary>背景の親オブジェクト</summary>
    [SerializeField]
    GameObject _backGroundParent;
    /// <summary>背景の単位ベクトル</summary>
    [SerializeField]
    Vector2 _backGroundDir = Vector2.down;
    /// <summary>背景のスクロールするスピード</summary>
    [SerializeField] float _scrollSpeed = 0.5f;
    /// <summary>背景のY座標の初期位置</summary>
    float _bgInitialPosY;

    /// <summary>ステージのデータ</summary>
    [SerializeField] 
    StageData _stageParam;

    /// <summary>ボス生成される時に呼びだされるデリゲート/// </summary>
    public event Action OnBoss;
    /// <summary>戦闘前ノベルが再生される際に呼び出されるデリゲート</summary>
    public event Action OnBeforeNovel;
    /// <summary>戦闘後(勝利敗北も含む)のノベルが再生されるときに呼び出されるデリゲート</summary>
    public event Action OnAfterNovel;

    public event Action OnEndAfterNovel;

    private int _phaseIndex = default;
    private int _loopCount = default;
    private bool _isNovelFirstTime = true;
    private float _timer = default;
    private bool _isBossStage;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnStageClear += () => _novelPhase = NovelPhase.Win;
    }

    void Start()
    {
        _bgInitialPosY = _backGround.transform.position.y;

        _backGroundClone = Instantiate(_backGround, _backGroundParent.transform);
        _backGroundClone.transform.Translate(0f, _backGround.bounds.size.y, 0f);

        //モブ敵の初回生成
        Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _enemyGeneretePos.position;
    }

    void Update()
    {
        if (PauseManager.Instance.PauseFlg == true) return;

        switch (_stageParam.PhaseParms[_phaseIndex].IsBoss)
        {
            case true:
                _timer += Time.deltaTime;
                if (_timer <= _novelWaitTime) return;
                Debug.Log("ボス開始");
                _isBossStage = true;
                Novel();
                break;

            case false:
                BackGround();
                break;
        }
    }

    /// <summary>
    /// ゲームオーバーの処理を行う
    /// </summary>
    private async void OnGameOver()
    {
        if (_isBossStage)
        {
            //ボスと戦っていればノベルのフェイズを変える
            _novelPhase = NovelPhase.Lose;
            return;
        }
        else
        {
            AllBulletEnemyDestroy();
            //UIキャンバス
            _uiCanvas.gameObject.SetActive(false);
            await Task.Delay(_gameOverUIWaitTime);
            //ゲームオーバUI表示
            _gameOverCanavas.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// すべての弾,敵を破棄する
    /// </summary>
    void AllBulletEnemyDestroy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var playerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        var enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");

        enemies.ToList().ForEach(x => Destroy(x));
        playerBullets.ToList().ForEach(x => Destroy(x));
        enemyBullets.ToList().ForEach(x => Destroy(x));
    }

    /// <summary>
    /// 次のフェイズのプレハブを生成する
    /// </summary>
    /// <param name="isLoop">現在のフェイズのプレハブをもう一度生成するかどうか</param>
    public void EnemyGenerate(bool isLoop)
    {
        //ゲームオーバー時は実行しない
        if (GameManager.Instance.IsGameOver) return;

        //ループをする場合はインデックスをカウントアップしない
        if (isLoop)
        {
            _loopCount++;
            //ループするべき回数を超えたらフェイズのインデックスをカウントアップする
            if(_loopCount >= _stageParam.PhaseParms[_phaseIndex].LoopTime)
            {
                _phaseIndex++;
            }
        }
        else
        {
            _phaseIndex++;
        }

        //ボスのフェイズだったらノベルを再生してから生成するため弾く
        if (_stageParam.PhaseParms[_phaseIndex].IsBoss) return;

        Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _enemyGeneretePos.position;
    }

    /// <summary>
    /// ノベルの処理を行う
    /// ボスの生成を行う
    /// 勝利,敗北等のUIの表示も行う
    /// </summary>
  　void Novel()
    {
        //ノベルの初回実行時にノベルのフェイズを戦闘前イベントにセットする
        if(_isNovelFirstTime)
        {
            _novelPhase = NovelPhase.Before;
            _isNovelFirstTime = false;
        }

        switch (_novelPhase)
        {
            case NovelPhase.Before://戦闘前のノベル
                if (_beforeGSSReader.gameObject.activeSelf == false)
                {   
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを有効化する
                    _beforeGSSReader.gameObject.SetActive(true);
                }

                if (!_beforeGSSReader.IsLoading)
                {
                    //ノベルのデータのロードが終わったら
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                    //ノベル中のデリゲート呼び出し
                    OnBeforeNovel?.Invoke();
                }

                if(_beforeNovelRenderer.IsNovelFinish)
                {
                    //ノベルの書き出しがすべて終わったら
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを無効化する
                    _beforeGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _uiCanvas.gameObject.SetActive(true);
                    _novelPhase = NovelPhase.None;
                    Instantiate(_stageParam.PhaseParms[_phaseIndex].Prefab).transform.position = _bossGeneretePos.position;
                    //ボス中のデリゲート呼び出し
                    OnBoss?.Invoke();
                }
                break;

            case NovelPhase.Win://勝利後のノベル
                if (_winGSSReader.gameObject.activeSelf == false)
                {
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを有効化する
                    _winGSSReader.gameObject.SetActive(true);
                }

                if(!_winGSSReader.IsLoading)
                {
                    //ノベルのデータのロードが終わったら
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                    AllBulletEnemyDestroy();
                    //戦闘後デリゲート呼び出し
                    OnAfterNovel?.Invoke();
                }

                if(_winNovelRenderer.IsNovelFinish)
                {
                    //ノベルの書き出しがすべて終わったら
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを無効化する
                    _winGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _gameClearCanvas.gameObject.SetActive(true);
                }
                break;

            case NovelPhase.Lose://敗北後のノベル
                if(_loseGSSReader.gameObject.activeSelf == false)
                {
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを有効化する
                    _loseGSSReader.gameObject.SetActive(true);
                }

                if (!_loseGSSReader.IsLoading)
                {
                    //ノベルのデータのロードが終わったら
                    _novelCanvas.gameObject.SetActive(true);
                    _uiCanvas.gameObject.SetActive(false);
                    AllBulletEnemyDestroy();
                    //戦闘後デリゲート呼び出し
                    OnAfterNovel?.Invoke();
                }

                if(_loseNovelRenderer.IsNovelFinish)
                {
                    //ノベルの書き出しがすべて終わったら
                    //ノベル関係のスクリプトがアタッチされているオブジェクトを無効化する
                    _loseGSSReader.gameObject.SetActive(false);
                    _novelCanvas.gameObject.SetActive(false);

                    _gameOverCanavas.gameObject.SetActive(true);
                }
                break;
        }
    }

    /// <summary>
    /// 背景の処理を行う
    /// </summary>
    void BackGround()
    {
        _backGround.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);
        _backGroundClone.transform.Translate(0f, _scrollSpeed * -Time.deltaTime, 0f);

        if(_backGround.transform.position.y < _bgInitialPosY - _backGround.bounds.size.y)
        {
            _backGround.transform.Translate(0f, _backGround.bounds.size.y * 2, 0f);
        }

        if(_backGroundClone.transform.position.y < _bgInitialPosY - _backGroundClone.size.y)
        {
            _backGroundClone.transform.Translate(0f, _backGroundClone.size.y * 2, 0f);
        }
    }
}