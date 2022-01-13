﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerのオブジェクトプールを管理するスクリプト
/// </summary>
public class PlayerBulletPool : MonoBehaviour
{
    public static PlayerBulletPool Instance => _instance;
    static PlayerBulletPool _instance;

    [SerializeField] GameObject[] _bullets = null;
    List<BulletParameter> _pool = new List<BulletParameter>();

    [SerializeField] int[] _poolObjectMaxCount = default;
    int _poolObjCountIndex = 0;

    private void Awake()
    {
        _instance = this;
        _poolObjCountIndex = 0;
        CreatePool();
        foreach(var pool in _pool)
        {
            Debug.Log($"オブジェクト名:{pool.Object.name} 種類:{pool.Type}");
        }
    }

    public void CreatePool()
    {
        if(_poolObjCountIndex >= _poolObjectMaxCount.Length)
        {
            Debug.Log("すべてのPlayerBulletを生成しました。");
            return;
        }

        for (int i = 0; i < _poolObjectMaxCount[_poolObjCountIndex]; i++)
        {
            var bullet = Instantiate(_bullets[_poolObjCountIndex], this.transform);
            bullet.SetActive(false);
            _pool.Add(new BulletParameter { Object = bullet, Type = (BulletType)_poolObjCountIndex});
        }

        _poolObjCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// Bulletを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="position">Bulletの位置を指定する</param>
    /// <param name="bulletType">発射するBulletの種類</param>
    /// <returns></returns>
    public GameObject UseBullet(Vector2 position, BulletType bulletType)
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

        var newBullet = Instantiate(_bullets[(int)bulletType], this.transform);
        newBullet.transform.position = position;
        newBullet.SetActive(true);
        _pool.Add(new BulletParameter { Object = newBullet, Type = bulletType});
        return newBullet;
    }

    public class BulletParameter
    {
        public GameObject Object { get; set; }
        public BulletType Type { get; set; }
    }
}

public enum BulletType
{
    Player01Power1 = 0,
    Player01Power2,
    Player01Power3,

    Player02Power1,
    Player02Power2,
    Player02Power3,
}

