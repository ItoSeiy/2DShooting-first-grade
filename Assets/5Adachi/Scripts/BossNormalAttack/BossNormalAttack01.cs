using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalAttack01 : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    /// <summary>プレイヤーのタグ</summary>
    [SerializeField,Header("playerのtag")] string _playerTag = null;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.6f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType _bullet;

    int _count = 0;

    /// <summary>水平、横方向</summary>
    private float _horizontal = 0f;
    /// <summary>垂直、縦方向</summary>
    private float _veritical = 0f;
    /// <summary>速度</summary>
    [SerializeField, Header("スピード")] float _speed = 4f;
    /// <summary>停止時間</summary>
    [SerializeField, Header("停止時間")] float _stopTime = 2f;
    /// <summary>移動時間</summary>
    [SerializeField, Header("移動時間")] float _moveTime = 0.5f;
    /// <summary>右限</summary>
    [SerializeField, Header("右限")] float _rightLimit = 4f;
    /// <summary>左限</summary>
    [SerializeField, Header("左限")] float _leftLimit = -4f;
    /// <summary>上限</summary>
    [SerializeField, Header("上限")] float _upperLimit = 2.5f;
    /// <summary>下限</summary>
    [SerializeField, Header("下限")] float _lowerLimit = 1.5f;
    /// <summary>左方向</summary>
    const float LEFT_DIR = -1f;
    /// <summary>右方向</summary>
    const float RIGHT_DIR = 1f;
    /// <summary>上方向</summary>
    const float UP_DIR = 1f;
    /// <summary>下方向</summary>
    const float DOWN_DIR = -1;
    /// <summary>方向なし</summary>
    const float NO_DIR = 0f;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag(_playerTag);
        StartCoroutine(Attack());
    }

    void Update()
    {
        //ターゲット（プレイヤー）の方向を計算
        _dir = (_player.transform.position - _muzzles[0].transform.position);
        //ターゲット（プレイヤー）の方向に回転
        _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
    }

    //Attack関数に入れる通常攻撃
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackInterval);
            //親オブジェクトのマズル
            
            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseBullet(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //子オブジェクトのマズル

            //弾をマズル1の向きに合わせて弾を発射（親オブジェクトの弾より右側）
            ObjectPool.Instance.UseBullet(_muzzles[1].position, _bullet).transform.rotation= _muzzles[1].rotation;
            
            //弾をマズル2の向きに合わせて弾を発射（親オブジェクトの弾より左側）
            ObjectPool.Instance.UseBullet(_muzzles[2].position, _bullet).transform.rotation = _muzzles[2].rotation;

            //弾をマズル3の向きに合わせて弾を発射（親オブジェクトの弾より右側）
            ObjectPool.Instance.UseBullet(_muzzles[3].position, _bullet).transform.rotation = _muzzles[3].rotation;

            //弾をマズル4の向きに合わせて弾を発射（親オブジェクトの弾より左側）
            ObjectPool.Instance.UseBullet(_muzzles[4].position, _bullet).transform.rotation = _muzzles[4].rotation;

            //一定時間止まる
            _rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(_stopTime);


            if (_count % 2 == 0)
            {
                //場所によって移動できる左右方向を制限する
                if (transform.position.x > _rightLimit)         //右に移動しすぎたら
                {
                    _horizontal = Random.Range(LEFT_DIR, NO_DIR);//左移動可能
                }
                else if (transform.position.x < _leftLimit)   //左に移動しぎたら
                {
                    _horizontal = Random.Range(NO_DIR, RIGHT_DIR);//右移動可能
                }
                else                     //左右どっちにも移動しすぎてないなら
                {
                    _horizontal = Random.Range(LEFT_DIR, RIGHT_DIR);//自由に左右移動可能          
                }

                //場所によって移動できる上下方向を制限する
                if (transform.position.y > _upperLimit)      //上に移動しすぎたら
                {
                    _veritical = Random.Range(DOWN_DIR, NO_DIR);//下移動可能
                }
                else if (transform.position.y < _lowerLimit)//下に移動しすぎたら
                {
                    _veritical = Random.Range(NO_DIR, UP_DIR);//上移動可能
                }
                else                    //上下どっちにも移動しすぎてないなら
                {
                    _veritical = Random.Range(DOWN_DIR, UP_DIR);//自由に上下移動可能
                }

                _dir = new Vector2(_horizontal, _veritical);//ランダムに移動
                                                            //一定時間移動する
                _rb.velocity = _dir.normalized * _speed;
                yield return new WaitForSeconds(_moveTime);
            }

            _count++;
        }
    }
}
