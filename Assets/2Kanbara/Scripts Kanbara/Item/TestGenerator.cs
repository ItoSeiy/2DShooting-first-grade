using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerator : MonoBehaviour
{
    [SerializeField, Header("アイテムのインターバル")] float _intarval;
    [SerializeField, Header("生成するアイテム")] GameObject _items;
    float _time;

    void Update()
    {
        _time += Time.deltaTime;
        if(_time > _intarval)
        {
            Instantiate(_items);
            _time = 0;
        }
    }
}
