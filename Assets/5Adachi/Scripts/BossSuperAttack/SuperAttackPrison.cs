using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttackPrison : MonoBehaviour
{
    /// <summary>形状や大きさの概念を持った物質</summary>
    Rigidbody2D _rb;
    /// <summary>方向</summary>
    Vector3 _dir;
    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;
    [SerializeField, Header("playerのtag")] string _playerTag = null;
    /// <summary>バレットを発射するポジション</summary>
    [SerializeField, Header("Bulletを発射するポジション")] Transform[] _muzzles = null;
    /// <summary>速度</summary>
    [SerializeField, Header("スピード")] float _speed = 4f;
    /// <summary>必殺前に移動するポジション</summary>
    [SerializeField, Header("必殺前に移動するポジション")] Transform _superAttackPos = null;
    /// <summary>タイマー</summary>
    float _timer = 0f;
    /// <summary>右側の範囲</summary>
    bool _rightRange;
    /// <summary>左側の範囲</summary>
    bool _leftRange;
    /// <summary>上側の範囲</summary>
    bool _upperRange;
    /// <summary>下側の範囲</summary>
    bool _downRange;
    /// <summary>横方向</summary>
    float _horizontalDir = 0f;
    /// <summary>縦方向</summary>
    float _verticalDir = 0f;
    /// <summary>必殺技待機時間</summary>
    [SerializeField, Header("必殺技待機時間")] float _waitTime = 5f;
    /// <summary>必殺技発動時間</summary>
    [SerializeField, Header("必殺技発動時間")] float _activationTime = 30f;
    /// <summary>攻撃頻度</summary>
    [SerializeField, Header("攻撃頻度(秒)")] private float _attackInterval = 0.6f;
    /// <summary>発射する弾を設定できる</summary>
    [SerializeField, Header("発射する弾の設定")] PoolObjectType _bullet;
    /// <summary>修正値</summary>
    const float PLAYER_POS_OFFSET = 0.5f;
    /// <summary>判定回数の制限</summary>
    const float JUDGMENT_TIME = 1 / 60f;
    /// <summary>リセットタイマー</summary>
    const float RESET_TIME = 0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();//《スタート》でゲットコンポーネント
        _player = GameObject.FindGameObjectWithTag(_playerTag);//プレイヤーのタグを探す
        StartCoroutine(Prison());//コルーチンを発動
    }
    void Update()
    {
        _timer += Time.deltaTime;//タイマー
    }

    /// <summary>playerを閉じ込めてplayerがいる方向に弾を発射する</summary>
    IEnumerator Prison()
    {
        _timer = RESET_TIME;//タイムリセット

        //必殺を放つときはBOSSは放つ前にｘを0、Ｙを2をの位置(笑)に、移動する
        while (true)
        {
            yield return new WaitForSeconds(JUDGMENT_TIME);//判定回数の制限
            //横方向
            _horizontalDir = _superAttackPos.position.x - transform.position.x;
            //縦方向
            _verticalDir = _superAttackPos.position.y - transform.position.y;
            //横の範囲の条件式      
            _rightRange = transform.position.x < _superAttackPos.position.x + PLAYER_POS_OFFSET;
            _leftRange = transform.position.x > _superAttackPos.position.x - PLAYER_POS_OFFSET;
            //縦の範囲の条件式
            _upperRange = transform.position.y < _superAttackPos.position.y + PLAYER_POS_OFFSET;
            _downRange = transform.position.y > _superAttackPos.position.y - PLAYER_POS_OFFSET;
            //行きたいポジションに移動する
            //近かったら
            if (_rightRange && _leftRange && _upperRange && _downRange)
            {
                Debug.Log("結果は" + _rightRange + _leftRange + _upperRange + _downRange);
                //スムーズに移動
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir) * _speed;
            }
            //遠かったら
            else
            {
                Debug.Log("結果は" + _rightRange + _leftRange + _upperRange + _downRange);
                //安定して移動
                _rb.velocity = new Vector2(_horizontalDir, _verticalDir).normalized * _speed;
            }

            //数秒経ったら
            if (_timer >= _waitTime)
            {
                Debug.Log("stop");
                _rb.velocity = Vector2.zero;//停止
                transform.position = _superAttackPos.position;//ボスの位置を修正
                break;//終わり
            }
        }

        _timer = RESET_TIME;//タイムリセット

        //必殺技発動
        while (true)
        {
            //動くマズル

            //マズルを回転する

            //ターゲット（プレイヤー）の方向を計算
            _dir = (_player.transform.position - _muzzles[0].transform.position);
            //ターゲット（プレイヤー）の方向に回転
            _muzzles[0].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);

            //弾をマズル0の向きに合わせて弾を発射
            ObjectPool.Instance.UseObject(_muzzles[0].position, _bullet).transform.rotation = _muzzles[0].rotation;

            //動かないマズル

            //弾をマズル1の向きに合わせて弾を発射（動くマズルの弾より右側）
            ObjectPool.Instance.UseObject(_muzzles[1].position, _bullet).transform.rotation = _muzzles[1].rotation;
            
            //弾をマズル2の向きに合わせて弾を発射（動くマズルより左側）
            ObjectPool.Instance.UseObject(_muzzles[2].position, _bullet).transform.rotation = _muzzles[2].rotation;

            yield return new WaitForSeconds(_attackInterval);//攻撃頻度

            //数秒経ったら
            if (_timer >= _activationTime)
            {
                break;//終了
            }
        }
        yield break;//終了
    }
}
