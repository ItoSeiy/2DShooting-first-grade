using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerBulletのオブジェクトプールを管理するスクリプト
/// </summary>
public class PlayerBulletPool : MonoBehaviour
{
    public static PlayerBulletPool Instance => _instance;
    static PlayerBulletPool _instance;

    [SerializeField] GameObject[] _bullets = null;

    [SerializeField] List<BulletSettings> _bulletSettings = new List<BulletSettings>();

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

    private void CreatePool()
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
            _pool.Add(new BulletParameter { Object = bullet, Type = (PlayerBulletType)_poolObjCountIndex});
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
    public GameObject UseBullet(Vector2 position, PlayerBulletType bulletType)
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

    private class BulletParameter
    {
        public GameObject Object { get; set; }
        public PlayerBulletType Type { get; set; }
    }

    [System.Serializable]
    private class BulletSettings
    {
        [SerializeField] List<GameObject> BulletObject = new List<GameObject>();
        [SerializeField] List<PlayerBulletType> BulletType = new List<PlayerBulletType>();
    }

}

public enum PlayerBulletType
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

