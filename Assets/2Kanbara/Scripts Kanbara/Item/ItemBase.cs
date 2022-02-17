using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    [SerializeField, Header("プレイヤーのタグ")] string _playerTag = "Player";
    [SerializeField, Header("アイテムを回収するコライダーのタグ")] string _itemGetColiderTag = "PlayerTrigger";

    [SerializeField, Header("再生する演出")] GameObject _childrenPS = default;

    [SerializeField, Header("演出が再生されるタイミング")] StartPS _stratPS = StartPS.Contact;

    bool _isTaking = false;

    public bool IsTaking => _isTaking;

    private void OnEnable()
    {
        if (_stratPS == StartPS.FirstTime)
        {
            _childrenPS.SetActive(true);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == _playerTag)//プレイヤーに接触したら
        {
            _childrenPS.SetActive(false);
            Destroy(this.gameObject);
        }
        if(collision.tag == _itemGetColiderTag)
        {
            _isTaking = true;
        }
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(this.gameObject);
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