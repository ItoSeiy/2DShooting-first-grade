using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMove03 : MonoBehaviour
{
    float _totaleTime;
    private int _speed = 3;
    private int _radius = 10;
    Rigidbody2D _rb;
    [SerializeField] public string _playerTag = default;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindGameObjectWithTag(_playerTag);
    }

    private void Update()
    {
        _totaleTime += Time.deltaTime;
        StartCoroutine("Down");
    }

    IEnumerator Down()
    {
        float x =  
        //float y = 
        //_rb.velocity(x, y);
    }
}
