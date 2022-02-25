using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerTutorialTriger : MonoBehaviour
{
    [SerializeField, Header("アイテムのレイヤー名")] string _itemLayerName = default;
    [SerializeField, Header("ボムのタグ名")] string _bombTag = default;
    [SerializeField, Header("残機のタグ名")] string _residueTag = default;
    [SerializeField, Header("無敵アイテムのタグ名")] string _invincibleTag = default;
    [SerializeField, Header("パワーアイテムのタグ名")] string _powerTag = default;
    [SerializeField, Header("スコアアイテムのタグ名")] string _scoreTag = default;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string _layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (_layerName == _itemLayerName)
        {
            Tutorial.Instance.GetItemTutorial();
        }
        if (collision.tag == _bombTag)
        {
            Tutorial.Instance.BombTutorial();
        }
        if (collision.tag == _residueTag)
        {
            Tutorial.Instance.ResidueTutorial();
        }
        if (collision.tag == _invincibleTag)
        {
            Tutorial.Instance.InvisibleTutorial();
        }
        if (collision.tag == _powerTag)
        {
            Tutorial.Instance.PowerTutorial();
        }
        if (collision.tag == _scoreTag)
        {
            Tutorial.Instance.ScoreTutorial();
        }
    }
}
