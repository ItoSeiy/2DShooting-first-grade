using UnityEngine;

public class BulletParent : MonoBehaviour
{
    GameObject[] _bulletsChildren = new GameObject[99];
    Vector2[] _initialBulletChildrenPos = new Vector2[99];
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _bulletsChildren[i] = transform.GetChild(i).gameObject;
            _initialBulletChildrenPos[i] = _bulletsChildren[i].transform.position;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _bulletsChildren[i].transform.position = _initialBulletChildrenPos[i];
        }
    }
}
