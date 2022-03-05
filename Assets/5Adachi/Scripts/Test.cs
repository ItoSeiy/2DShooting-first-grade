using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]　GameObject _particleObject = null;
    public float _timer = 0; 
    void Start()
    {
        StartCoroutine(A()); 
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    IEnumerator A()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f/ 60f);
            if(_timer >= 0f)
            {
                Instantiate(_particleObject, transform.position, Quaternion.identity); //パーティクル用ゲームオブジェクト生成
                Destroy(gameObject);
                break;
            }
            
        }      
    }   
}
