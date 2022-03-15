using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// サウンドを管理するスクリプト
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Header("BGMを流すAudioSource")]
    AudioSource _normalBgmAudioSource;

    [SerializeField]
    [Header("BossBGMを流すAudioSource")]
    AudioSource _bossBgmAudioSource;

    [SerializeField]
    [Header("BGMのフェードにかける時間")]
    float _bgmFadeTime = 2f;
    [SerializeField]
    [Header("BGMを小さく流す際の音量")]
    float _bgmFadeOutVol = 0.2f;

    [SerializeField]
    [Header("効果音が格納されたクラス")]
    SoundPoolParams _soundObjParam = default;

    List<SoundPool> _pool = new List<SoundPool>();

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

        //キャラ死亡時
        GameManager.Instance.OnGameOver += () =>
        {
            //BGM停止
            FadeBgm(_normalBgmAudioSource, 0f, _bgmFadeTime);
            //ボス戦闘中に死んだ際はノベルが流れるため少し音を残す
            FadeBgm(_bossBgmAudioSource, _bgmFadeOutVol, _bgmFadeTime);
        };

        //ボス死亡時                               ノベルが流れるため少し音を残す
        GameManager.Instance.OnStageClear += () => FadeBgm(_bossBgmAudioSource, _bgmFadeOutVol, _bgmFadeTime);

        PhaseNovelManager.Instance.OnEndAfterNovel += () => FadeBgm(_normalBgmAudioSource, 0f, _bgmFadeTime);
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
        if(_poolCountIndex >= _soundObjParam.Params.Length)
        {
            //Debug.Log("すべてのオーディオを生成しました。");
            return;
        }

        for (int i = 0; i < _soundObjParam.Params[_poolCountIndex].MaxCount; i++)
        {
            var sound = Instantiate(_soundObjParam.Params[_poolCountIndex].Audio, this.transform);
            sound.gameObject.SetActive(false);
            _pool.Add(new SoundPool { Sound = sound, Type = _soundObjParam.Params[_poolCountIndex].Type });
        }

        _poolCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// サウンドを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="soundType">流したいサウンドの種類</param>
    /// <returns></returns>
    public AudioSource UseSound(SoundType soundType)
    {
        if (PauseManager.Instance.PauseFlg == true)
        {
            Debug.LogWarning($"{soundType}を再生するリクエストを受けました\nポーズ中なので再生しません");
            return null;
        }

        foreach (var pool in _pool)
        {
            if(pool.Sound.gameObject.activeSelf == false && pool.Type == soundType)
            {
                pool.Sound.gameObject.SetActive(true);
                return pool.Sound;
            }
        }

        var newSound = Instantiate(Array.Find(_soundObjParam.Params, x => x.Type == soundType).Audio, this.transform);
        _pool.Add(new SoundPool { Sound = newSound, Type = soundType});
        newSound.gameObject.SetActive(true);

        Debug.LogWarning($"{soundType}のプールのオブジェクト数が足りなかったため新たにオブジェクトを生成します" +
            $"\nこのオブジェクトはプールの最大値が少ない可能性があります" +
            $"\n現在{soundType}の数は{_pool.FindAll(x => x.Type == soundType).Count}です");

        return newSound;
    }

    /// <summary>
    /// サウンドをプールするためのクラス
    ///</summary>
    private class SoundPool
    {
        public AudioSource Sound { get; set; }
        public SoundType Type { get; set; }
    }

    /// <summary>
    /// サウンドの情報をまとめたクラス
    /// </summary>
    [System.Serializable]
    public class SoundPoolParams
    {
        public SoundPoolParam[] Params => soundPoolParams;
        [SerializeField] public SoundPoolParam[] soundPoolParams;
    }

    /// <summary>
    /// サウンド一つ一つの情報が格納されたクラス
    /// </summary>
    [System.Serializable]
    public class SoundPoolParam
    {
        public SoundType Type => type;
        public AudioSource Audio => audio;
        public int MaxCount => maxCount;

        [SerializeField] 
        private string name;
        [SerializeField]
        private SoundType type;
        [SerializeField]
        private AudioSource audio;
        [SerializeField]
        private int maxCount;
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