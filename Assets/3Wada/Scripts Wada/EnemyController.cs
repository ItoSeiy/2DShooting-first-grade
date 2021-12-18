using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyBese
{
    [SerializeField] Transform[] _muzzle = null;
    [SerializeField] GameObject _bullet = null;
    [SerializeField] AudioSource _onDestroyAudio = null;
    [SerializeField] float _speed = 1f;
    [SerializeField] float _bottomposition = 0;
    [SerializeField] Vector2 _beforeDir;
    [SerializeField] Vector2 _afterDir;
    Rigidbody2D _rb = null;
    bool _isBttomposition = false;

    void Start()
    {
        // まずまっすぐ下に移動させる
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _beforeDir;
    }

     void Update()
    {
        if (_isBttomposition) return;


        if(this.transform.position.y ==  _bottomposition)
        {
            _rb.velocity = _afterDir;
            _isBttomposition = true;
        }
    }

    protected override void Attack()
    {
        for (int i = 0;i < _muzzle.Length; i++)
        {
            Instantiate(_bullet, _muzzle[i]);
        }
    }

    protected override void OnGetDamage()
    {
        if (EnemyHp == 0) 
        {
            AudioSource.PlayClipAtPoint(_onDestroyAudio.clip,transform.position);
            
        }
    }    
}
