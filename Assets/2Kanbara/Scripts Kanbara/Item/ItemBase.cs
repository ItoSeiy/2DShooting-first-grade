using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class ItemBase : MonoBehaviour
{
    [SerializeField, Header("プレイヤーのタグ")] string _playerTag = "Player";
    [SerializeField, Header("プレイヤーの持つアイテム回収用のコライダー")] string _playerTriggerTag = "PlayerTrigger";
    GameObject _player;
    Rigidbody2D _rb;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _playerTag)
        {
            Destroy(this.gameObject);
        }
        if(collision.tag == _playerTriggerTag)
        {
            ApproachPlayer();
        }
    }

    private void OnEnable()
    {
        _player = GameObject.Find("Player").GetComponent<GameObject>();
    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    protected virtual void ApproachPlayer()
    {
        _rb.velocity = this.gameObject.transform.position - _player.transform.position;
    }
}
