using Overdose.Data;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonController : MonoBehaviour
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
    [Header("[ステージ選択時]解放されていないステージを選択したときの音")]
    SoundType[] _onClickSoundsUnknownStage;

    [SerializeField]
    [Header("[ステージ選択時] 解放されていないステージを選択したら出すパネル")]
    GameObject _unknownStageSelectWarningPanel = null;

    [SerializeField]
    [Header("[ステージ選択時]ステージが解放されていないときに常に出すパネル")]
    GameObject _unknownStagePanel = null;

    [SerializeField]
    [Header("[ステージ選択時] ステージ番号")]
    int _stageNum = 0;

    [SerializeField]
    [Header("[ステージ選択時] プレイヤー番号")]
    int _playerNum = 0;

    SceneLoadCaller _sceneLoadCaller;

    private async void OnEnable()
    {
        _sceneLoadCaller = GetComponent<SceneLoadCaller>();
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

    public void NormalSelect()
    {
        if (_onClickSounds != null)
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
                    foreach (var sound in _onClickSounds)
                    {
                        SoundManager.Instance.UseSound(sound);
                    }
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

                    _unknownStageSelectWarningPanel.SetActive(true);
                }

                break;
            default:
                Debug.LogError($"プレイヤー{_playerNum}は存在しません");
                break;
        }
    }

    private void CheckButtonText()
    {

    }

    private void ObjectsActiveChange(GameObject[] gameObjects, bool active) => Array.ForEach(gameObjects, x => x.SetActive(active));

    private void UseSounds(SoundType[] soundTypes) => Array.ForEach(soundTypes, x => SoundManager.Instance.UseSound(x));
}