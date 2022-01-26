using UnityEngine;

public class EnemyBullt : BulletBese
{
    [SerializeField] BulltMode _bulletMode;
    GameObject _player;
    float _timer;
    Vector2 _oldDir = Vector2.up;
    Transform _muzzle;
    [SerializeField, Header("í«è]Ç∑ÇÈéûä‘")] float _followTime = 2f;

    void Start()
    {
        _muzzle = GetComponentInChildren<Transform>();
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
