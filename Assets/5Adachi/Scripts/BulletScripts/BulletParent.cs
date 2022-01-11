using UnityEngine;

public class BulletParent : MonoBehaviour
{
    BulletBese[] _bulletChildren = null;
    Vector2[] _initialBulletChildrenPos = default;
    private void Awake()
    {
        _bulletChildren = GetComponentsInChildren<BulletBese>();
        for(int i = 0; i < _bulletChildren.Length; i++)
        {
            _initialBulletChildrenPos[i] = _bulletChildren[i].transform.position;
        }
    }
    void OnDisable()
    {
        for(int i = 0; i < _bulletChildren.Length; i++)
        {
            _bulletChildren[i].transform.position = _initialBulletChildrenPos[i];
        }
    }
}
