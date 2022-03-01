using UnityEngine;


public class EnemyBullet : BulletBese
{
    [SerializeField] 
    BulltMode _bulletMode;

    [SerializeField, Header("í«è]Ç∑ÇÈéûä‘")] 
    float _followTime = 2f;

    GameObject _player;
    Vector2 _oldDir = Vector2.up;  
    float _timer = default;

    protected override void OnEnable()
    {
        _timer = 0;
        _player = GameObject.FindWithTag(OpponenTag);
        base.OnEnable();
    }
    protected override void BulletMove()
    {
        switch (_bulletMode)
        {

            case BulltMode.FollowBullet:
                _timer += Time.deltaTime;
                if (_player && _timer < _followTime)
                {
                    Vector2 dir = _player.transform.position - this.transform.position;
                    dir = dir.normalized * Speed;
                    Rb.velocity = dir;
                    _oldDir = dir;
                }
                else  
                {
                    Rb.velocity = _oldDir.normalized * Speed;
                }
                break;
            case BulltMode.AutoBullet:
                AutoStraight();
                break;
            case BulltMode.Straight:                
                base.BulletMove();   
                break;
        }
    }
   
    void AutoStraight()
    {
        Vector3 velocity = gameObject.transform.rotation * new Vector3(0, Speed, 0);
        Rb.velocity = velocity;
    }
 
    enum BulltMode
    {
        FollowBullet,
        AutoBullet,
        Straight,
    }
}
