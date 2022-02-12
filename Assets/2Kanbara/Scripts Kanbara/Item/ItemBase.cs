using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    [SerializeField, Header("アイテムがプレイヤーに近づく速度")] float _itemSpeed = 10f;
    [SerializeField, Header("プレイヤーがアイテム回収ラインに触れたときにアイテムがプレイヤーに近づく速度")] float _getItemSpeed = 100f;

    [SerializeField, Header("プレイヤーのタグ")] string _playerTag = "Player";
    [SerializeField, Header("プレイヤーの持つアイテム回収用のコライダー")] string _playerTriggerTag = "PlayerTrigger";

    [SerializeField, Header("再生する演出")] GameObject _childrenPS = default;

    [SerializeField, Header("演出が再生されるタイミング")] StartPS _stratPS = StartPS.Contact;

    Rigidbody2D _rb;
    GameObject _player;
    PlayerBase _playerBase;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _playerTag)//プレイヤーに接触したら
        {
            _childrenPS.SetActive(false);
            Destroy(this.gameObject);
        }
        if (collision.tag == _playerTriggerTag)//アイテム回収コライダーに接触したら
        {
            _player = GameObject.FindWithTag("Player");
            _playerBase = _player.GetComponent<PlayerBase>();
            if (_stratPS == StartPS.Contact)
            {
                _childrenPS.SetActive(true);
            }
            ApproachPlayer();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)//トリガー内にプレイヤーがいたら追い続ける
    {
        ApproachPlayer();
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        if(_stratPS == StartPS.FirstTime)
        {
            _childrenPS.SetActive(true);
        }
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
    /// <summary>
    /// プレイヤーに近づく関数
    /// </summary>
    public void ApproachPlayer()
    {
        var dir = _player.transform.position - this.gameObject.transform.position;
        _rb.velocity = dir.normalized * _itemSpeed;
    }

    void PlayerOnItemGetLine()
    {
        var dir = _player.transform.position - this.gameObject.transform.position;
        _rb.velocity = dir.normalized * _getItemSpeed;
    }

    /// <summary>
    /// 演出を再生させるタイミング
    /// </summary>
    enum StartPS
    {
        /// <summary>最初</summary>
        FirstTime,
        /// <summary>接触時</summary>
        Contact
    }
}