using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    Rigidbody2D _rb;

    [SerializeField, Header("プレイヤーのタグ")] string _playerTag = "Player";
    [SerializeField, Header("アイテムを回収するコライダーのタグ")] string _itemGetColiderTag = "PlayerTrigger";

    [SerializeField, Header("アイテム回収ラインにプレイヤーが接触したときのアイテム回収時のアイテムの速度")] float _itemSpeed = 10f;

    [SerializeField, Header("再生する演出")] GameObject _childrenPS = default;

    [SerializeField, Header("演出が再生されるタイミング")] StartPS _stratPS = StartPS.Contact;

    bool _isGetItemMode = false;
    public bool _isTaking = false;

    private void OnEnable()
    {
        _isTaking = false;
        if (_stratPS == StartPS.FirstTime)
        {
            _childrenPS.SetActive(true);
        }
    }

    private void OnDisable()
    {
        _isGetItemMode = false;
    }

    private void Update()
    {
        switch (_isGetItemMode)
        {
            case true:
                if (_stratPS == StartPS.Contact)
                {
                    _childrenPS.SetActive(true);
                }
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                var playerRb = GameObject.FindWithTag(_playerTag);
                var dir = playerRb.transform.position - this.gameObject.transform.position;
                rb.velocity = dir.normalized * _itemSpeed;
                break;
            case false:
                break;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _playerTag || collision.tag == "Finish")//プレイヤーに接触したら
        {
            _childrenPS.SetActive(false);
            gameObject.SetActive(false);
        }
        if(collision.tag == _itemGetColiderTag)
        {
            if(_stratPS == StartPS.Contact)
            {
                _childrenPS.SetActive(true);
            }
        }
    }

    public void OnItemGetLine()
    {
        _isGetItemMode = true;
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