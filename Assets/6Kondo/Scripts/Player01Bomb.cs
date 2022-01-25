using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class Player01Bomb : BulletBese
{
    [SerializeField] Explosion _explosionPrefab　= null;
    [SerializeField] float _childBulletDelay = 1f;
    [SerializeField] Transform[] _muzzle = null;
    SpriteRenderer _sprite = null;
    //[SerializeField] Light2D _grobalLight = null;
    //[SerializeField] float _goalColor = default;
    //[SerializeField] float _changeValueInterval = default;
    //[SerializeField] float _lightChangeDelay = default;

    //Color _test = default;
    //Color _test2 = default;
    protected override void Awake()
    {
        base.Awake();
        _sprite = GetComponent<SpriteRenderer>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.BulletAttack(collision);
        StartCoroutine(UseBombChildBullet(collision));
    }

    IEnumerator UseBombChildBullet(Collider2D collision)
    {
        if (collision.tag == OpponenTag)
        {
            var explosionPrefab = Instantiate(_explosionPrefab);
            explosionPrefab.transform.position = collision.transform.position;

            //var sequence = DOTween.Sequence(DOTween.To(() => _grobalLight.color.g,
            //    (x) =>
            //    {
            //        Color c = _grobalLight.color;
            //        _test = _grobalLight.color;
            //        c.g = x;
            //        _grobalLight.color = c;
            //    },
            //    _goalColor,
            //    _changeValueInterval));
            //sequence.Append(DOTween.To(() => _grobalLight.color.b,
            //    (x) =>
            //    {
            //        Color c = _grobalLight.color;
            //        _test2 = _grobalLight.color;
            //        c.b = x;
            //        _grobalLight.color = c;
            //    },
            //    _goalColor,
            //    _changeValueInterval));
            //RetrunColor();

            _sprite.enabled = false;
            Rb.velocity = Vector2.zero;
            for (int i = 0; i < _muzzle.Length; i++)
            {
                var bombChild = PlayerBulletPool.Instance.UseBullet(_muzzle[i].position, PoolObjectType.Player01BombChild);
                bombChild.transform.rotation = _muzzle[i].rotation;
                yield return new WaitForSeconds(_childBulletDelay);
            }

            _sprite.enabled = true;
            gameObject.SetActive(false);
        }
        else if(collision.tag == GameZoneTag)
        {
            gameObject.SetActive(false);
        }
    }

    //public async void RetrunColor()
    //{
    //    await Task.Delay(3);
    //    var sequence = DOTween.Sequence(DOTween.To(() => _grobalLight.color.g,
    //            (x) =>
    //            {
    //                Color c = _grobalLight.color;
    //                c.g = x;
    //                _grobalLight.color = c;
    //            },
    //            _test.g,
    //            _changeValueInterval));
    //    sequence.Append(DOTween.To(() => this._grobalLight.color.b,
    //        (x) =>
    //        {
    //            Color c = _grobalLight.color;
    //            c.b = x;
    //            _grobalLight.color = c;
    //        },
    //        _test2.b,
    //        _changeValueInterval));

    //}
}
