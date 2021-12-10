using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombbChildBullet : MonoBehaviour
{
    [SerializeField] Vector2 _direction = Vector2.up;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //rb.velocity = transform.localPosition.normalized * 2f;
    }
}
