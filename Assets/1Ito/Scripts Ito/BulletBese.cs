using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BulletBese : MonoBehaviour
{
    public float Damege { get => _damege;}

    [SerializeField] private float _damege;
}
