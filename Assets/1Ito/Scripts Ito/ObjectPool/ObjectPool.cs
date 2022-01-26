using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerBulletのオブジェクトプールを管理するスクリプト
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public static global::ObjectPool Instance => _instance;
    static global::ObjectPool _instance;

    //[SerializeField] GameObject[] _bullets = null;
    [SerializeField] PoolObjectParamAsset _poolObjParam = default;

    List<ObjPool> _pool = new List<ObjPool>();

    //[SerializeField] int[] _poolObjectMaxCount = default;
    int _poolCountIndex = 0;

    private void Awake()
    {
        _instance = this;
        _poolCountIndex = 0;
        CreatePool();
        foreach(var pool in _pool)
        {
            Debug.Log($"オブジェクト名:{pool.Object.name} 種類:{pool.Type}");
        }
    }

    private void CreatePool()
    {
        if(_poolCountIndex >= _poolObjParam.Params.Count /*_bullets.Length*/)
        {
            Debug.Log("すべてのPlayerBulletを生成しました。");
            return;
        }

        for (int i = 0; i < _poolObjParam.Params[_poolCountIndex].MaxCount/*_poolObjectMaxCount[_poolObjCountIndex]*/; i++)
        {
            var bullet = Instantiate(_poolObjParam.Params[_poolCountIndex].Prefab /*_bullets[_poolObjCountIndex]*/, this.transform);
            bullet.SetActive(false);
            _pool.Add(new ObjPool { Object = bullet, Type = _poolObjParam.Params[_poolCountIndex].Type } );
        }

        _poolCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// Bulletを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="position">Bulletの位置を指定する</param>
    /// <param name="bulletType">発射するBulletの種類</param>
    /// <returns></returns>
    public GameObject UseBullet(Vector2 position, PoolObjectType bulletType)
    {
        foreach(var pool in _pool)
        {
            if (pool.Object.activeSelf == false && pool.Type == bulletType)
            {
                pool.Object.transform.position = position;
                pool.Object.SetActive(true);
                return pool.Object;
            }
        }

        var newBullet = Instantiate(_poolObjParam.Params.Find(x => x.Type == bulletType).Prefab /*_bullets[(int)bulletType]*/, this.transform);
        newBullet.transform.position = position;
        newBullet.SetActive(true);
        _pool.Add(new ObjPool { Object = newBullet, Type = bulletType});
        return newBullet;
    }

    private class ObjPool
    {
        public GameObject Object { get; set; }
        public PoolObjectType Type { get; set; }
    }
}

public enum PoolObjectType
{
    Player01Power1 = 0,
    Player01Power2,
    Player01Power3,

    Player02Power1,
    Player02Power2,
    Player02Power3,

    Player01Bomb01,
    Player02Bomb01,

    Player01BombChild,
    Player02BombChild,

    Player01SuperPower1,
    Player01SuperPower2,
    Player01SuperPower3,

    Player02SuperPower1,
    Player02SuperPower2,
    Player02SuperPower3,

    Player01ChargePower1,
    Player01ChargePower2,
    Player01ChargePower3,

    Player02ChargePower1,
    Player02ChargePower2,
    Player02ChargePower3
}

