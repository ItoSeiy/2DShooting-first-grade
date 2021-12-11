using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02BulletFollow : BulletBese
{
    [SerializeField] GameObject _playerPrefab = null;

    private void Start()
    {
        if(_playerPrefab)
        {
            Vector2 v = _playerPrefab.transform.position - this.transform.position;
            v = v.normalized;
        }
    }
}
