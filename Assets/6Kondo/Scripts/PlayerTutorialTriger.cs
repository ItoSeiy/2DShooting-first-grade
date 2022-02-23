using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerTutorialTriger : MonoBehaviour
{
    [SerializeField] LayerMask _itemLayer;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aaa");
        if (collision.gameObject.layer == _itemLayer)
        {
            Tutorial.Instance.GetItemTutorial();
        }
    }
}
