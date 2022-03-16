using Overdose.Data;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    Button _button;
    Animator _animator = null;

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

    [SerializeField]
    SoundType[] _onClickSounds;

    private async void OnEnable()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        if(_animStateName != null && _animator != null)
        {
            _animator.Play(_animStateName);
        }
        if (_isDelayMode)
        {
            await Task.Delay(_delay);
        }
        if(_isFirstSelectButton)
        {
            _button.Select();
        }
    }

    public void Click()
    {
        if (_onClickSounds.Any())
        {
            foreach(var sound in _onClickSounds)
            {
                SoundManager.Instance.UseSound(sound);
            }
        }
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
