﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemyの基底クラス
/// </summary>
[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class EnemyBase : MonoBehaviour, IDamage
{
    public float Speed => _speed;
    public float EnemyHp { get => _enemyHp;}
    public float DamageTakenRation { get => _damageTakenRatio; set => _damageTakenRatio = value; }
    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public float Timer { get => _timer; }
    public float AttackInterval { get => _attackInterval; }
    public string PlayerBulletTag { get => _playerBulletTag; }

    [SerializeField, Header("動きのスピード")] private float _speed = 5f;
    [SerializeField, Header("体力")] private float _enemyHp = 10f;
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 1f;
    /// <summary>
    /// 攻撃力の割合
    /// </summary>
    [SerializeField, Header("被ダメージを何割にするか"), Range(0f, 1f)] float _damageTakenRatio = 1f;
    [SerializeField, Header("プレイヤーのBulletのタグ")] string _playerBulletTag = "PlayerBullet";
    [SerializeField] string _finishTag = "Finish";
    [SerializeField] DropItems _dropItems;
    [SerializeField] float _itemDropRangeX = 2f;
    [SerializeField] float _itemDropRangeY = 2f;
    private float _timer = default;
    Rigidbody2D _rb = null;
　　protected bool _isDeleteAble = false;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _attackInterval)
        {
            Attack();
            _timer = 0;
        }
    }

    /// <summary>
    /// 攻撃の処理を書いてください
    /// 例)Bulletプレハブを生成するなど
    /// </summary>
    protected abstract void Attack();

    /// <summary>
    /// ダメージを受けた際に行う処理を書いてください
    /// 例)ダメージを受けた際のアニメーションなど
    /// </summary>
    protected abstract void OnGetDamage();


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(EnemyHp <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }
        if(collision.tag == _finishTag)
        {
            if(!_isDeleteAble)
            {
                _isDeleteAble = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public void ItemDrop()
    {
        for(int i = 0; i < _dropItems.Items.Count; i++)
        {
            for(int i2 = 0; i2 < _dropItems.Items[i].Count; i2++)
            {
                float x = Random.Range(transform.position.x + _itemDropRangeX, transform.position.x - _itemDropRangeX);
                float y = Random.Range(transform.position.y + _itemDropRangeY, transform.position.y - _itemDropRangeY);
                ObjectPool.Instance.UseObject(new Vector2(x, y), _dropItems.Items[i].ItemType);
            }
        }
    }

    /// <summary>
    /// ダメージを喰らった際にBulletBaseから呼び出される関数
    /// Enemyがダメージを喰らう
    /// 攻撃力のデバフを行う
    /// ダメージを喰らった際の処理もここで呼ばれる
    /// 受けるダメージ量はBulletが指定する
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    public void AddDamage(float damage, Collider2D col)
    {
        //攻撃力を設定した分減らす処理
        damage *= _damageTakenRatio;

        _enemyHp -= damage;
        OnGetDamage();
    }
   
    public void ChangeAttackInterval(float interval)
    {
        _attackInterval = interval;
    }

    public void ChangeAttackIntervalRandom(float min, float max)
    {
        _attackInterval = Random.Range(min, max);
    }
}

[System.Serializable]
class DropItems
{
    public List<DropItem> Items => items;
    [SerializeField]
    private List<DropItem> items = new List<DropItem>();
}

[System.Serializable]
class DropItem
{
    public PoolObjectType ItemType => itemType;
    public int Count => count;
    [SerializeField]
    PoolObjectType itemType;
    [SerializeField]
    private int count = 5;
}
