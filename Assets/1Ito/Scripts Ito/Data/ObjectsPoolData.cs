using UnityEngine;
using System;

namespace Overdose.Data
{
    /// <summary>プールするオブジェクトを格納したスクリプタブルオブジェクト</summary>
    [CreateAssetMenu(fileName = "ObjectsPoolData")]
    public class ObjectsPoolData : ScriptableObject
    {
        public ObjectData[] Data => data;

        [SerializeField]
        private ObjectData[] data = default;

        /// <summary>プールするオブジェクトの一つ一つデータを格納したクラス</summary>
        [Serializable]
        public class ObjectData
        {
            public GameObject Prefab { get => objectPrefab; }
            public PoolObjectType Type { get => objectType; }
            public int MaxCount { get => objectMaxCount; }
            [SerializeField]
            private string Name;
            [SerializeField]
            private PoolObjectType objectType;
            [SerializeField]
            private GameObject objectPrefab;
            [SerializeField]
            private int objectMaxCount;
        }
    }
}