using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Threading.Tasks;

public class InputCommand : MonoBehaviour
{
    [SerializeField]
    InputAction _commandsAnswer1 = default;
    [SerializeField]
    InputAction _commandsAnswer2 = default;
    [SerializeField]
    InputAction _commandsAnswer3 = default;
    [SerializeField]
    InputAction _commandsAnswer4 = default;
    [SerializeField]
    InputAction _commandsAnswer5 = default;
    [SerializeField]
    InputAction _commandsAnswer6 = default;
    [SerializeField]
    InputAction _commandsAnswer7 = default;
    [SerializeField]
    InputAction _commandsAnswer8 = default;
    [SerializeField]
    InputAction _commandsAnswer9 = default;
    [SerializeField]
    InputAction _commandsAnswer10 = default;

    bool[] _isCommandSuccess = new bool[10];

    [SerializeField,Header("コマンドが成功した際の音")]
    SoundType _onCommandSuccessSound = SoundType.Sword;

    [SerializeField]
    Text _onCommandSuccessText;
    [SerializeField, Header("テキストを出す時間(ミリ秒)")]
    int _textActiveTime;

    public async void OnInputCommand(InputAction.CallbackContext context)
    {
        if (context.action == _commandsAnswer1 && _isCommandSuccess[0] && PauseManager.Instance.PauseFlg == true)
        {
            Debug.Log("コマンド完了");
            SoundManager.Instance.UseSound(_onCommandSuccessSound);
            GameManager.Instance.Player.IsGodMode = !GameManager.Instance.Player.IsGodMode;

            var isGodModeState = GameManager.Instance.Player.IsGodMode ? "有効!" : "無効!";

            _onCommandSuccessText.text = "無敵モード" + isGodModeState;

            await Task.Delay(_textActiveTime);

            _onCommandSuccessText.text = "";
            _isCommandSuccess[0] = false;
        }
        
    }
}
