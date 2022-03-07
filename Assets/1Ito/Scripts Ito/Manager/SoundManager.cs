using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// サウンドを管理するスクリプト
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    SoundPoolParams _soundObjParam = default;

    [SerializeField]
    [Header("BGMを流すAudioSource")]
    AudioSource _bgmAudioSource;
    [SerializeField]
    [Header("通常BGM")]
    AudioClip _normalBGM;
    [SerializeField]
    [Header("ボスBGM")]
    AudioClip _bossBGM;
    [SerializeField]
    float _bgmFadeTime = 2f;

    List<Pool> _pool = new List<Pool>();

    int _poolCountIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        _poolCountIndex = 0;
        CreatePool();
        //デバッグ用
        //_pool.ForEach(x => Debug.Log($"オブジェクト名:{x.Object.name}種類: {x.Type}"));
    }

    void Start()
    {
        _bgmAudioSource.PlayOneShot(_normalBGM);

        PhaseNovelManager.Instance.OnBoss += () => _bgmAudioSource.PlayOneShot(_bossBGM);

        GameManager.Instance.OnGameOver += () => FadeBgm(0, _bgmFadeTime);
        GameManager.Instance.OnStageClear += () => FadeBgm(0, _bgmFadeTime);
    }

    /// <summary>
    /// 現在流れているBGMのフェードを行う
    /// </summary>
    /// <param name="targetVol">設定したい音量</param>
    /// <param name="fadeTime">どのくらい時間をかけるか</param>
    public void FadeBgm(float targetVol, float fadeTime)
    {
        _bgmAudioSource.DOFade(targetVol, fadeTime);
    }

    /// <summary>
    /// 設定したオブジェクトの種類,数だけプールにオブジェクトを生成して追加する
    /// 再帰呼び出しを用いている
    /// </summary>
    private void CreatePool()
    {
        if(_poolCountIndex >= _soundObjParam.Params.Count)
        {
            //Debug.Log("すべてのオーディオを生成しました。");
            return;
        }

        for (int i = 0; i < _soundObjParam.Params[_poolCountIndex].MaxCount; i++)
        {
            var sound = Instantiate(_soundObjParam.Params[_poolCountIndex].Prefab, this.transform);
            sound.SetActive(false);
            _pool.Add(new Pool { Object = sound, Type = _soundObjParam.Params[_poolCountIndex].Type } );
        }

        _poolCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// サウンドを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="soundType">流したいサウンドの種類</param>
    /// <returns></returns>
    public GameObject UseSound(SoundType soundType)
    {
        foreach(var pool in _pool)
        {
            if(pool.Object.activeSelf == false && pool.Type == soundType)
            {
                pool.Object.SetActive(true);
                return pool.Object;
            }
        }

        var newSound = Instantiate(_soundObjParam.Params.Find(x => x.Type == soundType).Prefab, this.transform);
        _pool.Add(new Pool { Object = newSound, Type = soundType});
        newSound.SetActive(true);
        return newSound;
    }

    private class Pool
    {
        public GameObject Object { get; set; }
        public SoundType Type { get; set; }
    }
}

public enum SoundType
{
    /// <summary>音無し</summary>
    None,
    /// <summary>風</summary>
    Wind,
    /// <summary>剣</summary>
    Sword,
    /// <summary>キャッチ</summary>
    Catch,
    /// <summary>耳鳴り</summary>
    Tinnitus,
    /// <summary>銃</summary>
    Gun,
    /// <summary>ボス1,2,4,5の死亡サウンド</summary>
    BossKilled,
    /// <summary>ボスの死亡サウンド</summary>
    Boss3Killed,

    Click01,
    Click02,
    Click03,
    Click04,
    Click05,
    Click06,
    Click07,
    Click08,
    Click09,
    Click10,
    Novel,
    ScoreCount,
    EnemyKilled
}

[System.Serializable]
public class SoundPoolParams
{
    public List<SoundPoolParam> Params => soundPoolParams;
    [SerializeField] public List<SoundPoolParam> soundPoolParams = new List<SoundPoolParam>();
}


[System.Serializable]
public class SoundPoolParam
{
    public SoundType Type => type;
    public GameObject Prefab => prefab;
    public int MaxCount => maxCount;

    [SerializeField] 
    private string name;
    [SerializeField]
    private SoundType type;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int maxCount;
}


