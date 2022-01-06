using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bulletのオブジェクトプールを管理するスクリプト
/// </summary>
public class BulletPool : MonoBehaviour
{
    enum BulletType
    {

    }

    public static BulletPool Instance => _instance;
    static BulletPool _instance;

    [SerializeField] GameObject[] _bullets = null;
    Dictionary<GameObject, BulletType> _pool = null;

    private void Awake()
    {
        _instance = this;
    }

    public void CreatePool()
    {
        _pool = new Dictionary<GameObject, BulletType>();
        for(int i = 0;)
        {

        }
    }
}
