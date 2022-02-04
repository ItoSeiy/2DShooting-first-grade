using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[RequireComponent(typeof(GSSReader))]
public class NovelRenderer : MonoBehaviour
{
    public bool NovelFinish { get; private set; }
    [SerializeField] Text _mainText = null;
    [SerializeField] Text _nameText = null;
    [SerializeField, Range(0f, 0.5f)] float _textInterval = 0.1f;
    float _oldTextInterval;
    [SerializeField] Animator _characterAnimator;
    [SerializeField] Animator _bossAnimator;
    [SerializeField] AudioSource _textAudioSource;
    [SerializeField] GSSReader _gssReader;
    string[][] _datas = null;

    int _ggsRow = 0;
    int _currentCharNum = 0;

    bool _isDisplaying = false;
    bool _isClick = false;
    bool _isCommandFirstTime = true;

    SoundType _soundType;

    const int NAME_TEXT_COLUMN = 0;
    const int MAIN_TEXT_COLUMN = 1;
    const int ACTION_TEXT_COLUMN = 2;

    private void Start()
    {
        _gssReader.Reload();
        _mainText.text = "";
        _oldTextInterval = _textInterval;
    }

    private void Update()
    {
        if (_gssReader.IsLoading || CheckNovelFinish()) return;
        ControllText();
    }
    public void OnGSSLoadEnd()
    {
        _datas = _gssReader.Datas;
    }

    public void ControllText()
    {
        //テキストが最後まで読み込まれていなかったら
        if (_currentCharNum < _datas[_ggsRow][MAIN_TEXT_COLUMN].Length)
        {
            CheckCommand();

            if (_isClick)//クリックされたらテキストを飛ばす
            {
                _textInterval = 0;
            }

            DisplayText();
        }
        else//テキストが最後まで読み込まれたら
        {
            //テキストの速さを戻す
            _textInterval = _oldTextInterval;
            _isDisplaying = false;
            NextRow();//行の添え字をカウントアップ
        }
    }

    void NextRow()
    {
        if(_isClick)
        {
            _ggsRow++;
            _currentCharNum = 0;
            _mainText.text = "";
            _isClick = false;
            CheckCommand();
        }
    }

    void DisplayText()
    {
        //出力は一行につき一度のみ実行する
        if (_isDisplaying) return;
        StartCoroutine(MoveText());
    }

    IEnumerator MoveText()
    {
        _isDisplaying = true;

        switch (_datas[_ggsRow][NAME_TEXT_COLUMN])
        {
            case "効果音":
                _textAudioSource.mute = true;
                _nameText.text = "";
                break;
            default:
                _textAudioSource.mute = false;
                _nameText.text = _datas[_ggsRow][NAME_TEXT_COLUMN];
                break;
        }

        while(_isDisplaying)
        {
            _textAudioSource.Play();

            if (_currentCharNum == _datas[_ggsRow][MAIN_TEXT_COLUMN].Length) yield break;

            _mainText.text += _datas[_ggsRow][MAIN_TEXT_COLUMN][_currentCharNum];
            _currentCharNum++;
            yield return new WaitForSeconds(_textInterval);
        }
    }

    void Command()
    {
        //string[] command = _datas[_ggsRow][MainTextColumn].Split(' ');
        string command = _datas[_ggsRow][MAIN_TEXT_COLUMN];
        string action = _datas[_ggsRow][ACTION_TEXT_COLUMN];
        switch (command)
        {
            case "&MainCharacterAnim":
                Debug.Log("メインキャラアニメーションアニメーション" + action);
                _characterAnimator.Play(action);
                break;
            case "&BossAnim":
                Debug.Log("ボスアニメーション" + action);
                _bossAnimator.Play(action);
                break;
            case "&Sound":
                System.Enum.TryParse(action, out _soundType);
                SoundManager.Instance.UseSound(_soundType);
                break;
            default:
                Debug.LogError(command + action + "というコマンドは無効です");
                break;
        }
        _ggsRow++;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            _isClick = true;
        }

        if(context.canceled)
        {
            _isClick = false;
        }
    }

    bool CheckNovelFinish()
    {
        if (_ggsRow >= _datas.Length)
        {
            Debug.Log("すべてのシナリオを読み込んだ");
            NovelFinish = true;
            _mainText.text = "";
            _nameText.text = "";
            gameObject.SetActive(false);
            return true;
        }
        else
        {
            NovelFinish = false;
            return false;
        }
    }

    void CheckCommand()
    {
        if (_datas[_ggsRow][MAIN_TEXT_COLUMN][_currentCharNum] == '&' && _isCommandFirstTime)
        {
            //コマンド入力を検出する
            Command();
            _isCommandFirstTime = false;
        }
    }
}
