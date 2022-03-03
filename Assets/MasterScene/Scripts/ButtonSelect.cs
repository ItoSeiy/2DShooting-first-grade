using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ButtonSelect : MonoBehaviour
{
    Button _button;
    Animator _animator;

    [SerializeField]
    [Header("クリックしたときに自分を消すかどうか")]
    bool _isDelete = true;

    [SerializeField]
    [Header("セレクトを遅らせるモード")]
    bool _isDelayMode = false;

    [SerializeField]
    [Header("遅らせる秒数（ミリ秒）")]
    int _delay = 1500;

    [SerializeField]
    [Header("最初にセレクトされるかどうか")]
    bool _isFirstSelectButton = false;

    [SerializeField]
    [Header("呼び出すボタン")]
    GameObject[] _callNextButtons = null;

    [SerializeField]
    [Header("一緒に消えるButton")]
    GameObject[] _deleteButtons = null;

    [SerializeField]
    [Header("呼び出すパネル")]
    GameObject[] _callPanel = null;

    [SerializeField]
    [Header("一緒に消すパネル")]
    GameObject[] _deletePanel = null;

    [SerializeField]
    [Header("再生するアニメーション")]
    string _animStateName = null;

    private async void OnEnable()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        if (_isDelayMode)
        {
            await Task.Delay(_delay);
        }
        if(_isFirstSelectButton)
        {
            _button.Select();
        }
        if(_animStateName != null && _animator != null)
        {
            _animator.Play(_animStateName);
        }
    }

    public void Click()
    {
        if(_isDelete)
        {
            this.gameObject.SetActive(false);
        }
        if(_deleteButtons　!= null)
        {
            ActiveChange(_deleteButtons, false);
        }
        if (_callNextButtons != null)
        {
            ActiveChange(_callNextButtons, true);
        }
        if(_callPanel != null)
        {
            ActiveChange(_callPanel, true);
        }
        if(_deletePanel != null)
        {
            ActiveChange(_deletePanel, false);
        }
    }
    void ActiveChange(GameObject[] gameObjects, bool set)
    {
        foreach(var go in gameObjects)
        {
            go.SetActive(set);
        }
    }
}
