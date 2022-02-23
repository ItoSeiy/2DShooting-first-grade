using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンドを管理するスクリプト
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField] SoundPoolParams _soundObjParam = default;

    List<Pool> _pool = new List<Pool>();

    int _poolCountIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        _poolCountIndex = 0;
        CreatePool();
        //デバッグ用
        //foreach(var pool in _pool)
        //{
        //    Debug.Log($"オブジェクト名:{pool.Object.name} 種類:{pool.Type}");
        //}
    }

    private void CreatePool()
    {
        if(_poolCountIndex >= _soundObjParam.Params.Count)
        {
            //Debug.Log("すべてのオーディオを生成しました。");
            return;
        }

        for (int i = 0; i < _soundObjParam.Params[_poolCountIndex].MaxCount; i++)
        {
            var sound = Instantiate(_soundObjParam.Params[_poolCountIndex].Prefab　, this.transform);
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
            if (pool.Object.activeSelf == false && pool.Type == soundType)
            {
                pool.Object.SetActive(true);
                return pool.Object;
            }
        }

        var newSound = Instantiate(_soundObjParam.Params.Find(x => x.Type == soundType).Prefab, this.transform);
        newSound.SetActive(true);
        _pool.Add(new Pool { Object = newSound, Type = soundType});
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

}

[System.Serializable]
public class SoundPoolParams
{
    [SerializeField] public List<SoundPoolParam> Params = new List<SoundPoolParam>();
}


[System.Serializable]
public class SoundPoolParam
{
    [SerializeField] public string Name;

    [SerializeField] public SoundType Type;

    [SerializeField] public GameObject Prefab;

    [SerializeField] public int MaxCount;
}


