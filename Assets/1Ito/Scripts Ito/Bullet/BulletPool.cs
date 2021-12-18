using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool<T> where T : BulletSettings.BulletData
{
    GameObject _poolParent = null;
    List<GameObject> _bulletsPool = new List<GameObject>();
    List<T> _datas;

    public void SetUpPool(List<T> datas)
    {
        _datas = datas;
    }

    public void CreatePool(T data, int objCount)
    {
        if(!_poolParent)
        {
            GameObject poolParent = new GameObject("BulletPool");
            _poolParent = poolParent;
        }

        for(int i = 0; i < objCount; i++)
        {
            GameObject bullet = Object.Instantiate(data.Prefab);

            bullet.name = $"ID:{data.Id}.Name:{ data.Name}";
            
        }
    }
}
