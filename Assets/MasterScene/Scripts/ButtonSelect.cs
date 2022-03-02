using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ButtonSelect : MonoBehaviour
{
    Button _button;

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
    [Header("呼び出すべきボタンがあるかどうか")]
    bool _isMustCallNextButton = false;

    [SerializeField]
    [Header("ボタンの呼び出しを遅らせる秒数（ミリ秒）")]
    int _delayCallButton = 500;

    [SerializeField]
    [Header("呼び出すボタン")]
    GameObject[] _callNextButtons;

    [SerializeField]
    [Header("一緒に消すボタンがあるかどうか")]
    bool _isMustDeleteButton = false;

    [SerializeField]
    [Header("一緒に消えるButton")]
    GameObject[] _deleteButtons;

    private async void OnEnable()
    {
        _button = GetComponent<Button>();
        if (_isDelayMode)
        {
            await Task.Delay(_delay);
        }
        if(_isFirstSelectButton)
        {
            _button.Select();
        }
    }

    public async void Click()
    {
        if(_isDelete)
        {
            this.gameObject.SetActive(false);
        }
        if(_isMustDeleteButton)
        {
            foreach(var button in _deleteButtons)
            {
                button.SetActive(false);
            }
        }
        if (_isMustCallNextButton)
        {
            foreach(var button in _callNextButtons)
            {
                await Task.Delay(_delayCallButton);
                button.SetActive(true);
            }
        }
    }
}
