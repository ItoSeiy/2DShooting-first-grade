using UnityEngine;

public class BulletParent : MonoBehaviour
{
    public int BulletChilderenCount { get => _bulletsChildren.Length; }
    BulletBese[] _bulletsChildren = new BulletBese[99];
    Vector2[] _initialBulletChildrenPos = new Vector2[99];
    private void Awake()
    {
        _bulletsChildren = GetComponentsInChildren<BulletBese>();
        for (int i = 0; i < _bulletsChildren.Length; i++)
        {
            _initialBulletChildrenPos[i] = _bulletsChildren[i].transform.position;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < _bulletsChildren.Length; i++)
        {
            _bulletsChildren[i].transform.position = _initialBulletChildrenPos[i];
        }
    }
}
