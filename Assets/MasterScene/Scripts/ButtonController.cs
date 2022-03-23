using Overdose.Data;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
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
    bool _isFirstSelectMode = false;

    [SerializeField]
    [Header("呼び出すオブジェクト")]
    GameObject[] _callNextButtons = null;

    [SerializeField]
    [Header("選択時に消えるオブジェクト")]
    GameObject[] _deleteButtons = null;

    [SerializeField]
    [Header("再生するアニメーション")]
    string _animStateName = null;

    [SerializeField]
    [Header("通常のボタン音")]
    SoundType[] _onClickSounds;

    [SerializeField]
    [Header("ステージ選択用に使用するボタンかどうか")]
    bool _isStageSelectMode = false;

    [SerializeField]
    [Header("[ステージ選択時]解放されていないステージを選択したときの音")]
    SoundType[] _onClickSoundsUnknownStage;

    [SerializeField]
    [Header("[ステージ選択時] 解放されていないステージを選択したら出すパネル")]
    GameObject _unknownStageSelectWarningPanel = null;

    [SerializeField]
    [Header("[ステージ選択時]ステージ解放後のボタンの色")]
    Color _buttonTargetColor = Color.white;

    [SerializeField]
    [Header("[ステージ選択時]ステージ解放後のテキストの色")]
    Color _textTargetColor; 

    [SerializeField]
    [Header("[ステージ選択時]ステージが解放された時のボタンの変色の時間")]
    float _fadeTime = 1f;

    [SerializeField]
    [Header("[ステージ選択時] ステージ番号")]
    int _stageNum = 0;

    [SerializeField]
    [Header("[ステージ選択時] プレイヤー番号")]
    int _playerNum = 0;

    Button _button;
    Animator _animator = null;
    Text _buttonText;

    SceneLoadCaller _sceneLoadCaller;

    private async void OnEnable()
    {
        _sceneLoadCaller = GetComponent<SceneLoadCaller>();
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        _buttonText = GetComponentInChildren<Text>();

        if(_animStateName != null && _animator != null)
        {
            _animator.Play(_animStateName);
        }
        if (_isDelayMode)
        {
            await Task.Delay(_delay);
        }
        if(_isFirstSelectMode)
        {
            _button.Select();
        }
        if(_isStageSelectMode)
        {
            CheckButtonState();
        }
    }

    public void NormalSelect()
    {
        if (_onClickSounds != null)
        {
            UseSounds(_onClickSounds);
        }
        if(_isDelete)
        {
            this.gameObject.SetActive(false);
        }
        if(_deleteButtons　!= null)
        {
            ObjectsActiveChange(_deleteButtons, false);
        }
        if (_callNextButtons != null)
        {
            ObjectsActiveChange(_callNextButtons, true);
        }

        _sceneLoadCaller.LoadSceneString();
    }

    public void StageSelect()
    {
        switch (_playerNum)
        {
            case 1:

                if (SaveDataManager.Instance.SaveData.Player1StageActives[_stageNum - 1] == true)
                {
                    NormalSelect();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player1StageConut)
                {
                    Debug.LogError($"ステージ{_stageNum}は存在しません");
                }
                else
                {
                    UseSounds(_onClickSoundsUnknownStage);
                    _unknownStageSelectWarningPanel.SetActive(true);
                }

                break;

            case 2:

                if (SaveDataManager.Instance.SaveData.Player2StageActives[_stageNum - 1] == true)
                {
                    NormalSelect();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player2StageCount)
                {
                    Debug.LogError($"ステージ{_stageNum}は存在しません");
                }
                else
                {
                    UseSounds(_onClickSoundsUnknownStage);
                    _unknownStageSelectWarningPanel.SetActive(true);
                }

                break;
            default:
                Debug.LogError($"プレイヤー{_playerNum}は存在しません");
                break;
        }
    }

    private void CheckButtonState()
    {
        switch(_playerNum)
        {
            case 1:

                if (SaveDataManager.Instance.SaveData.Player1StageActives[_stageNum - 1] == true)
                {
                    DoButtonColor();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player1StageConut)
                {
                    Debug.LogError($"ステージ{_stageNum}は存在しません");
                }

                break;

            case 2:

                if (SaveDataManager.Instance.SaveData.Player2StageActives[_stageNum - 1] == true)
                {
                    DoButtonColor();
                }
                else if (_stageNum <= 0 || _stageNum > SaveDataManager.Instance.Player2StageCount)
                {
                    Debug.LogError($"ステージ{_stageNum}は存在しません");
                }

                break;
            default:
                Debug.LogError($"プレイヤー{_playerNum}は存在しません");
                break;
        }
    }

    private void DoButtonColor()
    {
        var color = _button.colors.normalColor;

        DOTween.To(() => color,
            c => color = c,
            _buttonTargetColor,
            _fadeTime)
            .OnUpdate(() => _button.colors = new ColorBlock { normalColor = color })
            .OnComplete(() => _button.colors = new ColorBlock { normalColor = _buttonTargetColor });
    }

    private void DoTextColor()
    {
        DOTween.To(() => _buttonText.color,
            c => _buttonText.color = c,
            _textTargetColor,
            _fadeTime)
            .OnComplete(() => _buttonText.color = _textTargetColor);
    }

    private void ObjectsActiveChange(GameObject[] gameObjects, bool active) => Array.ForEach(gameObjects, x => x.SetActive(active));

    private void UseSounds(SoundType[] soundTypes) => Array.ForEach(soundTypes, x => SoundManager.Instance.UseSound(x));
}