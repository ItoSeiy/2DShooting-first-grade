using System;
using UnityEngine;

namespace Overdose.Data
{
    /// <summary>ステージのデータを格納したクラス</summary>
    [CreateAssetMenu(fileName = "StageData")]
    public class StageData : ScriptableObject
    {
        public string SceneName => _sceneName;
        public AudioClip NormalBGM => _normalBGM;
        public AudioClip BossBGM => _bossBGM;
        public int PlayerNum => _playerNum;
        public int StageNum => _stageNum;
        public PhaseData[] PhaseParms => _phaseParms;

        [SerializeField]
        private string _sceneName;

        [SerializeField]
        private AudioClip _normalBGM;
        [SerializeField]
        private AudioClip _bossBGM;

        [SerializeField]
        private int _playerNum;
        [SerializeField]
        private int _stageNum;

        [SerializeField]
        private PhaseData[] _phaseParms = default;

    }


    /// <summary>フェイズのデータを格納したクラス</summary>
    [Serializable]
    public class PhaseData
    {
        public string PhaseName => PhaseName;
        public GameObject Prefab => _phasePrefab;
        public int LoopTime => _loopTime; 
        public bool IsBoss => _isBoss;

        [SerializeField] 
        private string _phaseName = "Phase";
        [SerializeField]
        private GameObject _phasePrefab;
        [SerializeField] 
        [Header("このプレハブの生成がループするモードであったら何回生成するか")]
        private int _loopTime;
        [SerializeField] 
        [Header("ボスだったらチェックを付ける")]
        private bool _isBoss = false;
    }
}


