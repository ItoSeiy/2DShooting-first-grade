using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// Enemyの派生クラス
/// </summary>
public class EnemyController : EnemyBase 
{
    /// <summary>このオブジェクトが破棄されたときに次のフェイズに移るか</summary>
    [SerializeField, Header("このオブジェクトが破棄されたときに次のフェイズに移るか")]
    bool _isPhaseTriger = false;
    /// <summary>次のフェイズに移らず同じプレハブを生成するかどうか</summary>
    [SerializeField, Header("次のフェイズに移らず同じプレハブを生成するかどうかisPhaseTrigerがオンになっているときにのみに発動")]
    bool _isPhaseLoop = false;

    [SerializeField, Header("球の出る位置")]
    Transform[] _muzzle = null;

    [SerializeField, Header("ノーマルマズル時回転するかどうか")] 
    bool _normalMuzzleRotate = false;

    [SerializeField,Header("Muzzleを回しながら球が出る位置")]
    Transform[] _rotateMuzzles = null;

    [SerializeField,Header("Muzzleの切り替え")] 
    MuzzleTransform _muzzleTransform;

    [SerializeField,Header("弾幕")]
    GameObject _bullet;

    [SerializeField, Header("攻撃頻度をランダムにするか")]
    bool _attackIntervelChange = false;

    [SerializeField, Header("攻撃頻度がランダム時のmax")] 
    float _randomIntervalMax = 1f;

    [SerializeField, Header("攻撃頻度がランダム時のmin")] 
    float _randomIntervalMin = 0.1f;

    [SerializeField,Header("モブ敵の出現位置")]
    GeneratePos _generatePos;

    [SerializeField, Header("移動方向が変わるXまたはYの座標")] 
    float _limitPos = 0;

    [SerializeField,Header("Muzzleが一周するまでの秒数")] 
    float _rotateSecond = 2f;

    [SerializeField, Header("Muzzleが1秒間で回転する角度")] 
    float _rotsteMuzzleLimit = 360f;

    [SerializeField, Header("出た時の移動方向")] 
    Vector2 _beforeDir;

    [SerializeField, Header("移動変わった後の移動方向")] 
    Vector2 _afterDir;

    [SerializeField, Header("何秒とどまるか")] 
    float _stopcount = 0.0f;

    [SerializeField, Header("モブの攻撃するときを変える")] AttackMode _attackMode;
    bool _isBttomposition = false;
    bool _isMove = false;
    SpriteRenderer _spriteRenderer = default;

    [SerializeField, Header("ダメージを受けた際の音")]
    SoundType _getDamageSFX = SoundType.None;

    [SerializeField, Header("死亡時の音")]
    SoundType _onDestroySFX = SoundType.None;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Rb.velocity = _beforeDir * Speed;
        _isMove = true; 
    }

    protected override void Update()
     {
        if(Rb.velocity.x < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
        switch(_attackMode)
        {
            case AttackMode.MoveAttack:
                base.Update();
                break;
            case AttackMode.StopAttack:
            if (!_isMove)
            {
                base.Update();
            }
                break;
        }
      
        switch (_generatePos)
        {
            case GeneratePos.Right:
                if (this.transform.position.x <= _limitPos)
                {
                    EnemyMove();
                }
                break;
            case GeneratePos.Left:
                if (this.transform.position.x >= _limitPos)
                {
                    EnemyMove();
                }
                break;
            case GeneratePos.Up:
                if (this.transform.position.y <= _limitPos)
                {
                    EnemyMove();
                }
                break;
            case GeneratePos.Down:
                if (this.transform.position.y >= _limitPos)
                {
                    EnemyMove();
                }
                break;
        }
     }

    void EnemyMove()
    {   
        //途中で止まる時の処理
        Rb.velocity = Vector2.zero;
        _stopcount -= Time.deltaTime;
        _isMove = false;
        // また動き出す時の処理
        if (_stopcount <= 0)
        {
            Rb.velocity = _afterDir * Speed;
            _isBttomposition = true;
            _isMove = true;
        }
    }

    protected override void Attack()
    {
        
        switch (_muzzleTransform)
        {
            case MuzzleTransform.Normal:
                for (int i = 0; i < _muzzle.Length; i++)
                {
                    var bullet = Instantiate(_bullet);
                    bullet.transform.position = _muzzle[i].position;
                    bullet.transform.rotation = _muzzle[i].rotation;  
                }
                switch(_attackIntervelChange)
                {
                    case false:
                        break;
                    case true:
                        ChangeAttackIntervalRandom(_randomIntervalMin,_randomIntervalMax );
                        break;
                }
                switch(_normalMuzzleRotate)
                {
                    case false:
                        break;
                    case true:
                        Rotate();
                        break;
                }
                break;
            case MuzzleTransform.Rotate:
                Rotate();
                for (int i = 0; i < _rotateMuzzles.Length; i++)
                {
                    var bullet = Instantiate(_bullet);
                    bullet.transform.position = _rotateMuzzles[i].position;
                    bullet.transform.rotation = _rotateMuzzles[i].rotation;
                }
                switch (_attackIntervelChange)
                {
                    case false:
                        break;
                    case true:
                        ChangeAttackIntervalRandom(_randomIntervalMin, _randomIntervalMax);
                        break;
                }

                break;
        }
    }

    void Rotate()
    {   
        foreach(var rotateMuzzle in _rotateMuzzles)
        {                      
            rotateMuzzle.DORotate(new Vector3(0, 0, _rotsteMuzzleLimit), _rotateSecond, RotateMode.WorldAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
        }
    }

    protected override void OnGetDamage()
    {
        //SoundManager.Instance.UseSound(_getDamageSFX);
        //if(EnemyHp <= 0)
        //{
        //    SoundManager.Instance.UseSound(_onDestroySFX);
        //}
    }

    //void OnDestroy()
    //{
    //    if (!_isPhaseTriger) return;
    //    //このオブジェクトが破棄されたときに次のフェイズに移る場合だったら
    //    PhaseNovelManager.Instance.EnemyGenerate(_isPhaseLoop);
    //}

    enum GeneratePos
    {
        /// <summary>右から生成する</summary>
        Right,
        /// <summary>左から生成する</summary>
        Left,
        /// <summary>上から生成する</summary>
        Up,
         /// <summary>下から生成する</summary>
        Down
    }

    enum MuzzleTransform
    {
        Normal,
        Rotate
    }
    enum AttackMode
    {
        StopAttack,
        MoveAttack
    }
}
