using System.Collections;
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

    private void Awake()
    {
        _instance = this;
        CreatePool(BulletType.Player01Power1);
        CreatePool(BulletType.Player01Power2);
        CreatePool(BulletType.Player01Power3);
        CreatePool(BulletType.Player02Power1);
        CreatePool(BulletType.Player02Power2);
        CreatePool(BulletType.Player02Power3);
        foreach(var pool in _pool)
        {
            Debug.Log(pool.BulletObject.name + pool.BulletType.ToString());
        }
    }

    public void CreatePool(BulletType bulletType)
    {
        for (int i = 0; i < _poolObjectMaxCount[(int)bulletType]; i++)
        {
            var bullet = Instantiate(_bullets[(int)bulletType]);
            bullet.SetActive(false);
            _pool.Add(new BulletParameter { BulletObject = bullet, BulletType = bulletType });
        }
    }

    /// <summary>
    /// Bulltを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="position">Bulletの位置を指定する</param>
    /// <param name="bulletType">発射するBulletの種類</param>
    /// <returns></returns>
    public GameObject UseBullet(Vector2 position, BulletType bulletType)
    {
        foreach(var pool in _pool)
        {
            if (pool.BulletObject.activeSelf == false && pool.BulletType == bulletType)
            {
                pool.BulletObject.transform.position = position;
                pool.BulletObject.SetActive(true);
                return pool.BulletObject;
            }
        }

        var newBullet = Instantiate(_bullets[(int)bulletType]);
        newBullet.transform.position = position;
        newBullet.SetActive(true);
        _pool.Add(new BulletParameter { BulletObject = newBullet, BulletType = bulletType });
        return newBullet;
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

    public class BulletParameter
    {
        public BulletType BulletType { get; set; }
        public GameObject BulletObject { get; set; }
    }

}

