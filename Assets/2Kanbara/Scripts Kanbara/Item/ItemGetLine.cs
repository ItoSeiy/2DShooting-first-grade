using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetLine : MonoBehaviour
{
    [SerializeField, Header("アイテムのタグ")] string _itemTag = "Item";
    [SerializeField, Header("プレイヤーのタグ")] string _playerTag = "Player";
    bool _isGetItem = false;
    GameObject[] _item;
    Rigidbody2D _rb;
    GameObject _player;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isGetItem) return;
        if (collision.tag == _playerTag)
        {
            _isGetItem = true;
            _item = GameObject.FindGameObjectsWithTag(_itemTag);
            foreach (var item in _item)
            {
                var dir = _player.transform.position - item.transform.position;
                _rb.velocity = dir.normalized;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isGetItem = false;
    }
}
