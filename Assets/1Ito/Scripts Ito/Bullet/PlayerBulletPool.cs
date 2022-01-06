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
    Dictionary<BulletType, GameObject> _pool = null;

    Dictionary<int, string> _test;

    [SerializeField] int[] _poolObjectMaxCount = default;

    private void Awake()
    {
        _instance = this;

        _test = new Dictionary<int, string>();
        _test.Add(1, "あ");
        _test.Add(2, "い");
        _test.Add(3, "う");
        Debug.Log(_test[1]);
    }

    public void CreatePool(BulletType bulletType)
    {
        _pool = new Dictionary<BulletType, GameObject>();
        for (int i = 0; i < _poolObjectMaxCount[(int)bulletType]; i++)
        {
            var bullet = Instantiate(_bullets[(int)bulletType]);
            bullet.SetActive(false);
            _pool.Add(bulletType, bullet);
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
        for(int i = 0; i < _pool.Count; i++)
        {
            if(_pool[bulletType].activeSelf == false)
            {
                var bullet = _pool[bulletType];
                bullet.transform.position = position;
                bullet.SetActive(true);
                return bullet;
            }
        }

        var newBullet = Instantiate(_bullets[(int)bulletType]);
        newBullet.SetActive(false);
        _pool.Add(bulletType, newBullet);
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
}
