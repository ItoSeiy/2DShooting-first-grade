using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(GSSReader))]
public class NovelManager : MonoBehaviour
{
    public bool NovelFinish { get; private set; }
    [SerializeField] Text _mainText = null;
    [SerializeField] Text _nameText = null;
    [SerializeField, Range(0f, 0.5f)] float _textInterval = 0.1f;
    float _oldTextInterval;
    [SerializeField] Animator _characterAnimator;
    [SerializeField] Animator _bossAnimator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] GSSReader _gssReader;
    string[][] _datas = null;

    int _ggsRow = 0;
    int _currentCharNum = 0;

    bool _isDisplaying = false;
    bool _isClick = false;
    bool _isCommandFirstTime = true;

    SoundType _soundType;

    const int MainTextColumn = 1;
    const int NameTextColumn = 0;

    private void Start()
    {
        _gssReader.Reload();
        _mainText.text = "";
        _oldTextInterval = _textInterval;
    }

    private void Update()
    {
        if (_gssReader.IsLoading || NovelFinish) return;
        ControllText();
    }
    public void OnGSSLoadEnd()
    {
        _datas = _gssReader.Datas;
    }

    public void ControllText()
    {

        if (_currentCharNum < _datas[_ggsRow][MainTextColumn].Length)
        {

            if (_isClick)
            {
                _textInterval = 0;
            }

            if (_datas[_ggsRow][MainTextColumn][_currentCharNum] == '&' && _isCommandFirstTime)
            {
                Command();
                _isCommandFirstTime = false;
            }
            else
            {
                DisplayText();
            }
        }
        else
        {
            _textInterval = _oldTextInterval;
            _isDisplaying = false;
            NextRow();
        }

    }

    void NextRow()
    {
        if(_isClick)
        {
            _ggsRow++;
            _currentCharNum = 0;
            _mainText.text = "";

            if (_ggsRow >= _datas.Length)
            {
                Debug.Log("すべてのシナリオを読み込んだ");
                NovelFinish = true;
                return;
            }

            if (_datas[_ggsRow][MainTextColumn][_currentCharNum] == '&')
            {
                Command();
            }

            _isClick = false;
        }
    }

    void DisplayText()
    {
        if (_isDisplaying) return;
        StartCoroutine(MoveText());
    }

    IEnumerator MoveText()
    {
        _isDisplaying = true;

        switch (_datas[_ggsRow][NameTextColumn])
        {
            case "効果音":
                _audioSource.mute = true;
                _nameText.text = "";
                break;
            default:
                _audioSource.mute = false;
                _nameText.text = _datas[_ggsRow][NameTextColumn];
                break;
        }

        while(_isDisplaying)
        {
            _audioSource.Play();
            _mainText.text += _datas[_ggsRow][MainTextColumn][_currentCharNum];
            _currentCharNum++;
            yield return new WaitForSeconds(_textInterval);
        }
    }

    void Command()
    {
        string[] command = _datas[_ggsRow][MainTextColumn].Split(' ');
        switch (command[0])
        {
            case "&MainCharacterAnim":
                Debug.Log("メインキャラアニメーションアニメーション" + command[1]);
                _characterAnimator.Play(command[1]);
                break;
            case "&BossAnim":
                Debug.Log("ボスアニメーション" + command[1]);
                _bossAnimator.Play(command[1]);
                break;
            case "&Sound":
                System.Enum.TryParse(command[1], out _soundType);
                SoundManager.Instance.UseSound(_soundType);
                break;
            default:
                Debug.LogError(command[0] + command[1] + "というコマンドは無効です");
                break;
        }
        _ggsRow++;
        if (_datas[_ggsRow][MainTextColumn][_currentCharNum] == '&')
        {
            Command();
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.started && !_isClick)
        {
            _isClick = true;
            Debug.Log(_isClick);
        }

        if(context.canceled)
        {
            _isClick = false;
        }
    }
}
