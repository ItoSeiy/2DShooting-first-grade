using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overdose.Calculation;

public class BossNormalAttack02 : BossAttackAction
{
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>弾の見た目の種類</summary>
    int _pattern = 0;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.2f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType[] _firstBullet;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定(上と同じ数を設定してください")] PoolObjectType[] _secondBullet;
    /// <summary>この行動から出る時間</summary>
    [SerializeField, Header("この行動から出る時間")] float _endingTime = 20f;
    /// <summary>アイテムを落とす確率</summary>
    [SerializeField, Header("アイテムを落とす確率")] int _probability = 50;
    /// <summary>対項角</summary>
    const float OPPOSITE_ANGLE = 180f;
    

    public override System.Action ActinoEnd { get; set; }

    public override void Enter(BossController contlloer)
    {
        _timer = 0f;
        StartCoroutine(Attack(contlloer));
    }

    public override void ManagedUpdate(BossController contlloer)
    {
        _timer += Time.deltaTime;

        if(_timer >= _endingTime)
        {
            ActinoEnd?.Invoke();
        }
    }

    public override void Exit(BossController contlloer)
    {
        //一定の確率でアイテムを落とす
        if (Calculator.RandomBool(_probability))
        {
            contlloer.ItemDrop();
        }
        
        StopAllCoroutines();
    }

    //Attack関数に入れる通常攻撃
    IEnumerator Attack(BossController controller)
    {
        while (true)
        {
            //弾の見た目をランダムで変える
            _pattern = Random.Range(0, _firstBullet.Length);
            
            ///プレイヤーの向きにマズルが向く///

            //ターゲット（プレイヤー）の方向を計算
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_pattern]).transform.rotation = _muzzles[0].rotation;

            ///プレイヤーがいる方向と逆(Xが逆方向)にマズルが向く///
            //マズルを回転する
            Vector3 localAngle = _muzzles[0].localEulerAngles;// ローカル座標を基準に取得           
            localAngle.z = -localAngle.z;//向きを逆側にする
            _muzzles[0].localEulerAngles = localAngle;//回転する
            //弾をマズルの向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_pattern/**/]).transform.rotation = _muzzles[0].rotation;

            ///プレイヤーがいる方向と逆(XとYが逆方向）///
            _dir = (GameManager.Instance.Player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, -_dir);

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _secondBullet[_pattern/**/]).transform.rotation = _muzzles[0].rotation;

            ///プレイヤーがいる方向と逆(Yが逆方向///
            //マズルを回転する  
            localAngle.z = -localAngle.z + OPPOSITE_ANGLE;//向きを逆側にする
            _muzzles[0].localEulerAngles = -localAngle;//回転する
            //弾をマズルの向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _firstBullet[_pattern]).transform.rotation = _muzzles[0].rotation;

            yield return new WaitForSeconds(_attackInterval);//攻撃頻度(秒)
        }
    }

}
