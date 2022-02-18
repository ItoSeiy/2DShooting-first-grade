using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemGet : MonoBehaviour
{
    [SerializeField, Header("アイテムがプレイヤーに近づく速度")] float _itemSpeed = 10f;

    [SerializeField, Header("アイテムのタグ")] string[] _itemTags = default;

    Rigidbody2D _itemObjectrb;
    Rigidbody2D _playerRb;
    private void OnEnable()
    {
        _playerRb = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var itemTag in _itemTags)
        {
            if (collision.tag == itemTag)//アイテムに接触したら
            {
                if(_itemObjectrb == null)
                {
                    _itemObjectrb = collision.GetComponent<Rigidbody2D>();
                }
                ApproachPlayer();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)//トリガー内にプレイヤーがいたら追い続ける
    {
        foreach (var itemTag in _itemTags)
        {
            if (collision.tag == itemTag)
            {
                if (_itemObjectrb == null)
                {
                    _itemObjectrb = collision.GetComponent<Rigidbody2D>();
                }
                ApproachPlayer();
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        foreach(var itemTag in _itemTags)
        {
            if (collision.tag == itemTag)
            {
                if (_itemObjectrb == null)
                {
                    _itemObjectrb = collision.GetComponent<Rigidbody2D>();
                }
                ApproachPlayer();
            }
        }
    }

    /// <summary>
    /// プレイヤーに近づく関数
    /// </summary>
    void ApproachPlayer()
    {
        if(_playerRb == null)
        {
            _playerRb = GetComponentInParent<Rigidbody2D>();
        }
        var dir = _playerRb.transform.position - _itemObjectrb.transform.position;
        _itemObjectrb.velocity = dir.normalized * _itemSpeed;
    }
}
