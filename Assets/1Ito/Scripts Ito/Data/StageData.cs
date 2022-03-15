using System.Collections.Generic;
using UnityEngine;
using System;

namespace Overdose.Data
{
    /// <summary>ステージのデータを格納したクラス</summary>
    [Serializable]
    public class StageData
    {
        public PhaseData[] PhaseParms => phaseParms;
        public AudioClip NormalBGM => _normalBGM;
        public AudioClip BossBGM => _bossBGM;

        [SerializeField]
        private PhaseData[] phaseParms = default;

        [SerializeField]
        private AudioClip _normalBGM;
        [SerializeField]
        private AudioClip _bossBGM;
    }


    /// <summary>フェイズのデータを格納したクラス</summary>
    [Serializable]
    public class PhaseData
    {
        public string PhaseName => PhaseName;
        public GameObject Prefab => phasePrefab;
        public int LoopTime => loopTime; 
        public bool IsBoss => isBoss;

        [SerializeField] 
        private string phaseName = "Phase";
        [SerializeField]
        private GameObject phasePrefab;
        [SerializeField, Header("このプレハブの生成がループするモードであったら何回生成するか")]
        private int loopTime;
        [SerializeField, Header("ボスだったらチェックを付ける")]
        private bool isBoss = false;
    }
}


