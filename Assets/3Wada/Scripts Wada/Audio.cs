using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] float _destroyTime = 2f;
    float _time;
    
    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if(_time >= _destroyTime)
        {
            Destroy(gameObject);
        }

    }
    
}
