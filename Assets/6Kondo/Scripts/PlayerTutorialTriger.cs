using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerTutorialTriger : MonoBehaviour
{
    [SerializeField] string _itemLayerName = default;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string _layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (_layerName == _itemLayerName)
        {
            Tutorial.Instance.GetItemTutorial();
        }
    }
}
