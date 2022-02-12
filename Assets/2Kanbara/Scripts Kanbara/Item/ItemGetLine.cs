using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetLine : MonoBehaviour
{
    [SerializeField, Header("アイテム")] GameObject[] _item;
    [SerializeField, Header("プレイヤーのタグ")] string _playerTag = "Player";
    GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag(_playerTag);
    }
}
