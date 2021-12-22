using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemyの派生クラス
/// </summary>
public class EnemyController : EnemyBese
{
    [SerializeField,Header("マズルの位置")] Transform[] _muzzle = null;
    [SerializeField, Header("バレット")] GameObject _bullet = null;
    [SerializeField, Header("倒された時の音")] AudioSource _onDestroyAudio = null;
    [SerializeField, Header("移動の向きの変えるY軸")] float _bottomposition = 0;
    [SerializeField, Header("出た時の移動方向")] Vector2 _beforeDir;
    [SerializeField, Header("移動変わった後の移動方向")] Vector2 _afterDir;
    Rigidbody2D _rb = null;
    bool _isBttomposition = false;
    [SerializeField, Header("何秒とどまるか")] float _stopcount = 0.0f;

    /// <summary>
    /// 出た時の移動
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = _beforeDir;
    }

     void Update()
    {   
        if (_isBttomposition) return;

        /// <summary>
        /// 途中で止まる時の処理
        /// </summary>
        if (this.transform.position.y <=  _bottomposition)
        {
            _rb.velocity = Vector2.zero;
            _stopcount -= Time.deltaTime;
            /// <summary>
            /// また動き出す時の処理
            /// </summary>
            if (_stopcount <= 0)
            {
                _rb.velocity = _afterDir;
                _isBttomposition = true;
            }
            
        }
    }

    /// <summary>
    /// 弾幕を出す処理
    /// </summary>
    protected override void Attack()
    {
        for (int i = 0;i < _muzzle.Length; i++)
        {
            Instantiate(_bullet, _muzzle[i]);
        }
    }
    /// <summary>
    /// 倒れた時に流す音の処理
    /// </summary>
    protected override void OnGetDamage()
    {
        if (EnemyHp == 0) 
        {
            AudioSource.PlayClipAtPoint(_onDestroyAudio.clip,transform.position);    
        }
    }    
}
