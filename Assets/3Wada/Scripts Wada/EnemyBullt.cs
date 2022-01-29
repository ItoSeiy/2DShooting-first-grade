using UnityEngine;
using DG.Tweening;

public class EnemyBullt : BulletBese
{
    [SerializeField] BulltMode _bulletMode;
    GameObject _player;
    float _timer;
    Vector2 _oldDir = Vector2.up;
     
    [SerializeField, Header("’Ç]‚·‚éŽžŠÔ")] float _followTime = 2f;
      public EnemyController _enemy;

    void Start()
    {
       
        
    }
    
void Update()
    {
        void Fllow()
        {
            _timer = 0;
            _player = GameObject.FindWithTag(OpponenTag);
            base.OnEnable();
            _timer += Time.deltaTime;
            if (_timer >= _followTime) return;

        }
        switch (_bulletMode)
        {
            case BulltMode.FollowBullet:
                

                if (_player)
                {
                    Fllow();
                    Vector2 dir = _player.transform.position - this.transform.position;
                    dir = dir.normalized * Speed;
                    Rb.velocity = dir;
                    _oldDir = dir;
                }
                else if (!_player)
                {
                    Rb.velocity = _oldDir.normalized * Speed;
                }
                break;
            case BulltMode.straightBullet:
                 break;
            case BulltMode.rotateBullet:
                transform.DOLocalRotate(new Vector3(0, 0, 360f), 6f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
                break;



        }
            
    }

    enum BulltMode
    {

        FollowBullet,
        straightBullet,
        rotateBullet,


    }
}
