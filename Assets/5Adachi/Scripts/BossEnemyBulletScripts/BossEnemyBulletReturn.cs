using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBulletReturn : BulletBese
{
    /// <summary>BossのGameObject</summary>
    GameObject _bossEnemy;
    /// <summary>Bossがいた方向</summary>
    Vector2 _oldDir = Vector2.down;
    /// <summary>ボスのタグ</summary>
    [SerializeField,Header("BossEnemyのTag")] string _bossEnemyTag = null;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 7.5f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -7.5f;
    /// <summary>上限</summary>
    [SerializeField,Header("上限")] float _upperLimit = 4f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _downLimit = -4f;
    /// <summary>横限の条件式</summary>
    bool _horizontalLimit;
    /// <summary>縦限の条件式</summary>
    bool _verticalLimit;
    /// <summary>ボスの方向に一瞬追従することが可能かどうか(1回目)</summary>
    bool _firstReturn = false;
    /// <summary>ボスの方向に一瞬追従することが可能かどうか(2回目)</summary>
    bool _secondReturn = false;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>ボスの方向に一瞬追従する時間</summary>
    float _firstReturnTime = 0.5f;
    /// <summary>ボスの方向に一瞬追従する時間</summary>
    float _secondReturnTime = 1.2f;
    /// <summary>雪が解ける時間</summary>
    float _snowmeltTime = 4f;

    protected override void OnEnable()
    {
        _timer = 0f;//タイマーをリセット
        _bossEnemy = GameObject.FindWithTag(_bossEnemyTag);//BossのTagをとってくる
        base.OnEnable();
        _firstReturn = true;//ボスの方向に一瞬追従することが可能
        _secondReturn = true;//ボスの方向に一瞬追従することが可能
    }

    protected override void BulletMove()
    {
        _timer += Time.deltaTime;//タイマー
       
        if (_timer >= _firstReturnTime && _firstReturn)//ボスの方向に一瞬追従する時間になったら(1回目)
        {           
            Vector2 dir = _bossEnemy.transform.position - transform.position;//プレイヤーの方向を計算           
            dir = dir.normalized * Speed;//速度が変わらないようにし、スピードを加える           
            Rb.velocity = dir;//方向を変える            
            _oldDir = dir;//方向を保存            
            _firstReturn = false;//使えないようにする
        }
        
        else if (_timer >= _secondReturnTime && _secondReturn)//ボスの方向に一瞬追従する時間になったら(2回目)
        {            
            Vector2 dir = _bossEnemy.transform.position - transform.position;//プレイヤーの方向を計算            
            dir = dir.normalized * Speed;//速度が変わらないようにし、スピードを加える            
            Rb.velocity = dir;//方向を変える         
            _oldDir = dir;//方向を保存            
            _secondReturn = false;//使えないようにする
        }      
        
        else if (_timer >= _snowmeltTime && !_firstReturn && !_secondReturn)//雪が解ける時間になったら
        {            
            this.gameObject.SetActive(false);//弾が消える
        }
        
        else if (!_firstReturn && !_secondReturn)//雪が解ける時間になる前だったら
        {         
            Rb.velocity = _oldDir.normalized * Speed;//現在の方向に移動
        }

        else
        {
            Rb.velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);//マズルの向きに合わせて移動
        }
        
    }
}
