using UnityEngine;
using System;

/// <summary>プールするオブジェクトを格納したスクリプタブルオブジェクト</summary>
[CreateAssetMenu(fileName = "PoolObjectParam")]
public class PoolObjectParamAsset : ScriptableObject
{
    public PoolObjectParam[] Params => poolObjectParams;

    [SerializeField]
    private PoolObjectParam[] poolObjectParams = default;
}

/// <summary>プールするオブジェクトのデータを格納したクラス</summary>
[Serializable]
public class PoolObjectParam
{
    public GameObject Prefab { get => objectPrefab;}
    public PoolObjectType Type { get => objectType;}
    public int MaxCount { get => objectMaxCount;}
    [SerializeField]
    private string Name;
    [SerializeField]
    private PoolObjectType objectType;
    [SerializeField]
    private GameObject objectPrefab;
    [SerializeField]
    private int objectMaxCount;
}

