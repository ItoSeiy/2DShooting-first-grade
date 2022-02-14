using UnityEngine;
using DG.Tweening;

public class EnemyBullt : BulletBese
{
    [SerializeField] BulltMode _bulletMode;
    GameObject _player;
    float _timer;
    Vector2 _oldDir = Vector2.up;  
    [SerializeField, Header("’Ç]‚·‚éŽžŠÔ")] float _followTime = 2f;
    EnemyController _enemyController;

   
    void Start()
    {
        
       
    }
    
void Update()
    {
        switch (_bulletMode)
        {
            case BulltMode.FollowBullet:
               Fllow();

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
                rotate();
                break;






        }
            
    }
        void Fllow()
        {
            _timer = 0;
            _player = GameObject.FindWithTag(OpponenTag);
            base.OnEnable();
            _timer += Time.deltaTime;
            if (_timer >= _followTime) return;

        }
   public void rotate()
    {
        Transform[] _muzul;
        _muzul = _enemyController._muzzle;
        for(int i = 0; i < _muzul.Length; i++)
        {
            _muzul[i].DOLocalRotate(new Vector3(0, 0, 360f), 6f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
        }
    }

    enum BulltMode
    {

        FollowBullet,
        straightBullet,
        rotateBullet,


    }
}
