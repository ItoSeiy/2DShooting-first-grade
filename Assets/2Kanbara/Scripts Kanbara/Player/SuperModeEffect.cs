using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperModeEffect : MonoBehaviour
{
    [SerializeField, Header("ターゲット")] GameObject _target;
    [SerializeField, Header("速度")] float _speed = default;
    [SerializeField, Header("角度")] float _angle = 50;
    const float _delaySpeed = 100;

    void Update()
    {
        transform.RotateAround(_target.transform.position, Vector3.forward, _angle * _speed / _delaySpeed);
    }
}