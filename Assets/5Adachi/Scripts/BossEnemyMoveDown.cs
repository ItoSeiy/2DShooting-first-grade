using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyMoveDown : MonoBehaviour
{
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Down());
    }
    void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -8f, 8f), (Mathf.Clamp(transform.position.y, -4f, 4f)));  
    }
    IEnumerator Down()
    {
        //int count = 0;
        //count++;
        if (transform.position.x < 0)
        {
            _rb.velocity = new Vector2(-3, 0);
            Debug.Log("a");
        }
        else
        {
            _rb.velocity = new Vector2(3, 0);
            Debug.Log("b");
        }

        yield return new WaitForSeconds(4);

        if (transform.position.x <= -7.5f)
        {
            Debug.Log("c");
            _rb.velocity = new Vector2(0, -3);
            
        }
        else if (transform.position.x >= 7.5f)
        {
            Debug.Log("d");
            _rb.velocity = new Vector2(0, -3);
        }

        yield return new WaitForSeconds(3);

        if (transform.position.y <= -4 && transform.position.x <= -7.5f)
        {
            Debug.Log("e");
            _rb.velocity = new Vector2(7, 0);
        }
        else if(transform.position.y <= -4 && transform.position.x >= -7.5f)
        {
            Debug.Log("f");
            _rb.velocity = new Vector2(-7, 0);
        }

        yield return new WaitForSeconds(4);

        _rb.velocity = new Vector2(0, 5);

        yield return new WaitForSeconds(3);

        if (transform.position.x < 0)
        {
            _rb.velocity = new Vector2(3, 0);
            Debug.Log("a");
        }
        else
        {
            _rb.velocity = new Vector2(-3, 0);
            Debug.Log("b");
        }

        yield return new WaitForSeconds(2.5f);

        _rb.velocity = new Vector2(0, 0);
        yield break;
    }
}
