using System;
using UnityEngine;

namespace Overdose.Data
{
    /// <summary>
    /// 効果音の情報をまとめたクラス
    /// </summary>
    [Serializable]
    public class SFXPoolData
    {
        public SFXData[] Data => soundPoolData;
        [SerializeField] public SFXData[] soundPoolData;
        /// <summary>
        /// サウンド一つ一つの情報が格納されたクラス
        /// </summary>
        [Serializable]
        public class SFXData
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
}


