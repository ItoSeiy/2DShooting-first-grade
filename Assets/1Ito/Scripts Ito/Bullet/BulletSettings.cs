using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletSettings : MonoBehaviour
{
    public static BulletSettings Instance => _instance;
    private static BulletSettings _instance = null;

    private void Awake()
    {
        _instance = this;
    }


    [Serializable]
    public class BulletData
    {
        public GameObject Prefab;
        public string Name;
        public int Id;
        public float Power;
    }
}


