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

    //[SerializeField] int[] _poolObjectMaxCount = default;
    int _poolCountIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        _poolCountIndex = 0;
        CreatePool();
        foreach(var pool in _pool)
        {
            Debug.Log($"オブジェクト名:{pool.Object.name} 種類:{pool.Type}");
        }
    }

    private void CreatePool()
    {
        if(_poolCountIndex >= _soundObjParam.Params.Count)
        {
            Debug.Log("オーディオを生成しました。");
            return;
        }

        for (int i = 0; i < _soundObjParam.Params[_poolCountIndex].MaxCount; i++)
        {
            var bullet = Instantiate(_soundObjParam.Params[_poolCountIndex].Prefab　, this.transform);
            bullet.SetActive(false);
            _pool.Add(new Pool { Object = bullet, Type = _soundObjParam.Params[_poolCountIndex].Type } );
        }

        _poolCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// Bulletを使いたいときに呼び出す関数
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
    Wind,
    Sword
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


