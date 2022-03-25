using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Overdose.Data;
using System.Linq;

/// <summary>
/// サウンドを管理するスクリプト
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Header("ステージのデータを格納したスクリプタブルオブジェクト")]
    private StageData[] _stageData;

    [SerializeField]
    [Header("BGMを流すAudioSource")]
    private AudioSource _normalBgmAudioSource;

    [SerializeField]
    [Header("BossBGMを流すAudioSource")]
    private AudioSource _bossBgmAudioSource;

    [SerializeField]
    [Header("AudioSourceの音量の初期値")]
    [Range(0f, 1f)]
    private float _initialAudioSourceVolme = 0.6f;

    [SerializeField]
    [Header("BGMのフェードにかける時間")]
    private float _bgmFadeTime = 2f;

    [SerializeField]
    [Header("BGMを小さく流す際の音量")]
    private float _bgmFadeOutVol = 0.1f;

    [SerializeField]
    [Header("効果音が格納されたクラス")]
    private SFXPoolData _sfxPoolData = default;


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
        SettingBGM();
    }

    /// <summary>BGMを流すタイミングをデリゲートに登録するなどの設定を行う関数</summary>
    private void SettingBGM()
    {
        SceneLoder.Instance.OnLoadEnd += () =>
        {
            _normalBgmAudioSource.volume = _initialAudioSourceVolme;
            _bossBgmAudioSource.volume = _initialAudioSourceVolme;

            TryPlayBGM();

            PhaseNovelManager.Instance.OnBeforeNovel += () =>
            {
                //ノベル時はBGMを小さく流す
                FadeBgm(_normalBgmAudioSource, _bgmFadeOutVol, _bgmFadeTime);
            };

            //ボス生成時
            PhaseNovelManager.Instance.OnBoss += () =>
            {
                //ボスが始まったら普通のBGMは消す
                _normalBgmAudioSource.Stop();
                //ボスのBGM再生
                _bossBgmAudioSource.Play();
            };


            PhaseNovelManager.Instance.OnEndAfterNovel += () => FadeBgm(_normalBgmAudioSource, 0f, _bgmFadeTime);
        };

        //キャラ死亡時
        GameManager.Instance.OnGameOver += () =>
        {
            Debug.Log("GameOver");
            //BGM停止
            FadeBgm(_normalBgmAudioSource, 0f, _bgmFadeTime);
            //ボス戦闘中に死んだ際はノベルが流れるため少し音を残す
            FadeBgm(_bossBgmAudioSource, _bgmFadeOutVol, _bgmFadeTime);
        };

        //ボス死亡時                               ノベルが流れるため少し音を残す
        GameManager.Instance.OnStageClear += () => FadeBgm(_bossBgmAudioSource, _bgmFadeOutVol, _bgmFadeTime);
    }

    /// <summary>BGMを流すステージかどうかを判別してBGMを流す関数</summary>
    private void TryPlayBGM()
    {
        _normalBgmAudioSource.clip = null;
        _bossBgmAudioSource.clip = null;

        Array.ForEach(_stageData, x =>
        {
            if (x.SceneName == SceneLoder.Instance.ActiveSceneName)
            {
                _normalBgmAudioSource.clip = x.NormalBGM;
                _bossBgmAudioSource.clip = x.BossBGM;

                _normalBgmAudioSource.Play();
            }
        });
    }

    /// <summary>
    /// 指定したオーディオソースのフェードを行う
    /// </summary>
    /// <param name="targetVolScale">設定したい音量
    /// min -> 0 max -> 1 </param>
    /// <param name="fadeTime">どのくらい時間をかけるか</param>
    private void FadeBgm(AudioSource targetAudioSouece, float targetVolScale, float fadeTime)
    {
        targetAudioSouece.DOFade(targetVolScale, fadeTime);
    }

    /// <summary>
    /// 設定したオブジェクトの種類,数だけプールにオブジェクトを生成して追加する
    /// 再帰呼び出しを用いている
    /// </summary>
    private void CreatePool()
    {
        if(_poolCountIndex >= _sfxPoolData.Data.Length)
        {
            //Debug.Log("すべてのオーディオを生成しました。");
            return;
        }

        for (int i = 0; i < _sfxPoolData.Data[_poolCountIndex].MaxCount; i++)
        {
            var sound = Instantiate(_sfxPoolData.Data[_poolCountIndex].Audio, this.transform);
            sound.gameObject.SetActive(false);
            _pool.Add(new Pool { Sound = sound, Type = _sfxPoolData.Data[_poolCountIndex].Type });
        }

        _poolCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// サウンドを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="soundType">流したいサウンドの種類</param>
    /// <returns>流すサウンドのオーディオソース</returns>
    public AudioSource UseSound(SoundType soundType)
    {
        foreach (var pool in _pool)
        {
            if(pool.Sound.gameObject.activeSelf == false && pool.Type == soundType)
            {
                pool.Sound.gameObject.SetActive(true);
                return pool.Sound;
            }
        }

        var newSound = Instantiate(Array.Find(_sfxPoolData.Data, x => x.Type == soundType).Audio, this.transform);
        _pool.Add(new Pool { Sound = newSound, Type = soundType});
        newSound.gameObject.SetActive(true);

        Debug.LogWarning($"{soundType}のプールのオブジェクト数が足りなかったため新たにオブジェクトを生成します" +
            $"\nこのオブジェクトはプールの最大値が少ない可能性があります" +
            $"現在{soundType}の数は{_pool.FindAll(x => x.Type == soundType).Count}です");

        return newSound;
    }

    /// <summary>
    /// サウンドを使いたいときに呼び出す関数
    /// 音量を調整できるオーバロード
    /// </summary>
    /// <param name="soundType">流したいサウンドの種類</param>
    /// <param name="volumeScale">流すサウンドの音量 min -> 0f max -> 1f</param>
    /// <returns>流すサウンドのオーディオソース</returns>
    public AudioSource UseSound(SoundType soundType, float volumeScale)
    {
        foreach (var pool in _pool)
        {
            if (pool.Sound.gameObject.activeSelf == false && pool.Type == soundType)
            {
                pool.Sound.volume = volumeScale;
                pool.Sound.gameObject.SetActive(true);
                return pool.Sound;
            }
        }

        var newSound = Instantiate(Array.Find(_sfxPoolData.Data, x => x.Type == soundType).Audio, this.transform);
        _pool.Add(new Pool { Sound = newSound, Type = soundType });
        newSound.volume = volumeScale;
        newSound.gameObject.SetActive(true);

        Debug.LogWarning($"{soundType}のプールのオブジェクト数が足りなかったため新たにオブジェクトを生成します" +
            $"\nこのオブジェクトはプールの最大値が少ない可能性があります" +
            $"現在{soundType}の数は{_pool.FindAll(x => x.Type == soundType).Count}です");

        return newSound;
    }

    /// <summary>
    /// サウンドをプールするためのクラス
    ///</summary>
    private class Pool
    {
        public AudioSource Sound { get; set; }
        public SoundType Type { get; set; }
    }
}