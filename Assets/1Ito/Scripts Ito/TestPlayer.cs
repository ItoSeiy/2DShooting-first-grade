using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public void Use(int i)
    {
        ObjectPool.Instance.UseObject(transform.position, (PoolObjectType)i);
    }
}
