using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Bulletのオブジェクトプールを管理するスクリプト
/// </summary>
public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
{
    [SerializeField] PoolObjectParamAsset _poolObjParam = default;

    List<ObjPool> _pool = new List<ObjPool>();
    int _poolCountIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        _poolCountIndex = 0;
        CreatePool();
        //デバッグ用
        _pool.ForEach(x => Debug.Log($"オブジェクト名:{x.Object.name} 種類:{x.Type}"));
    }

    /// <summary>
    /// 設定したオブジェクトの種類,数だけプールにオブジェクトを生成して追加する
    /// 再帰呼び出しを用いている
    /// </summary>
    private void CreatePool()
    {
        if(_poolCountIndex >= _poolObjParam.Params.Count)
        {
            //Debug.Log("すべてのオブジェクトを生成しました。");
            return;
        }

        for (int i = 0; i < _poolObjParam.Params[_poolCountIndex].MaxCount; i++)
        {
            var bullet = Instantiate(_poolObjParam.Params[_poolCountIndex].Prefab, this.transform);
            bullet.SetActive(false);
            _pool.Add(new ObjPool { Object = bullet, Type = _poolObjParam.Params[_poolCountIndex].Type } );
        }

        _poolCountIndex++;
        CreatePool();
    }

    /// <summary>
    /// オブジェクトを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="position">オブジェクトの位置を指定する</param>
    /// <param name="bulletType">オブジェクトの種類</param>
    /// <returns>生成したオブジェクト</returns>
    public GameObject UseObject(Vector2 position, PoolObjectType bulletType)
    {
        foreach(var pool in _pool)
        {
            if (pool.Object.activeSelf == false && pool.Type == bulletType)
            {
                pool.Object.SetActive(true);
                pool.Object.transform.position = position;
                return pool.Object;
            }
        }

        var newBullet = Instantiate(_poolObjParam.Params.Find(x => x.Type == bulletType).Prefab, this.transform);
        newBullet.transform.position = position;
        newBullet.SetActive(true);
        _pool.Add(new ObjPool { Object = newBullet, Type = bulletType});
        return newBullet;
    }

    private class ObjPool
    {
        public GameObject Object { get; set; }
        public PoolObjectType Type { get; set; }
    }
}

public enum PoolObjectType
{
    Player01Power1 = 0,
    Player01Power2,
    Player01Power3,

    Player02Power1,
    Player02Power2,
    Player02Power3,

    Player01Bomb01,
    Player02Bomb01,

    Player01BombChild,
    Player02BombChild,

    Player01SuperPower1,
    Player01SuperPower2,
    Player01SuperPower3,

    Player02SuperPower1,
    Player02SuperPower2,
    Player02SuperPower3,

    Player01ChargePower1,
    Player01ChargePower2,
    Player01ChargePower3,

    Player02ChargePower1,
    Player02ChargePower2,
    Player02ChargePower3,

    //ボスの弾

    //ボス共通の弾
    BossDefaultBullet1,
    BossDefaultBullet2,
    BossDefaultBullet3,
    BossDefaultBullet4,
    BossDefaultBullet5,
    BossDefaultBullet6,
    BossDefaultBullet7,
    //ボス5専用の弾
    Boss05DefaultBullet1,

    //ボス1の必殺技の弾
    Boss01SuperAttackBullet1,
    Boss01SuperAttackBullet2,
    Boss01SupetAttackBullet3,
    
    //ボス2の必殺技の弾
    Boss02SuperAttackBullet1,
    Boss02SuperAttackBullet2,
    Boss02SupetAttackBullet3,

    //ボス3の必殺技の弾
    Boss03SuperAttackBullet1,
    Boss03SuperAttackBullet2,
    Boss03SupetAttackBullet3,

    //ボス4の必殺技の弾
    Boss04SuperAttackBullet1,
    Boss04SuperAttackBullet2,
    Boss04SupetAttackBullet3,

   
    //ボス5の必殺技の弾
    Boss05SuperAttackBullet1,
    Boss05SuperAttackBullet2,
    Boss05SupetAttackBullet3,

    OneUpItem,
    BombItem,
    Invincible,
    ScoreItem,
    PowerItem,

}

