using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance => _instance;
    static EnemyPool _instance;

    [SerializeField] GameObject[] _enemies = null;
    [SerializeField] GameObject[] _enemyGroups = null;
    List<EnemyParameter> _pool = new List<EnemyParameter>();



    public class EnemyParameter
    {
        public EnemyType enemyType { get; set; }
        public PrefabType PrefabType { get; set; }
        public GameObject EnemtObject { get; set; }
    }
}

public enum EnemyType
{

}

public enum PrefabType
{

}

