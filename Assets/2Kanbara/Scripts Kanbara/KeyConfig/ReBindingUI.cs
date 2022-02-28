using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReBindingUI : MonoBehaviour
{
    [SerializeField] InputActionReference _inputActionReference;

    [SerializeField] bool _exclubeMouse = true;

    [SerializeField, Range(0, 10)] int _selectedBinding;
    [SerializeField] InputBinding.DisplayStringOptions _displayStringOptions;
    [SerializeField] InputBinding _inputBinding;
    int _bindingIndex;

    string _actionName;

    [SerializeField] Text _actionText;
    [SerializeField] Button _rebindButton;
    [SerializeField] Text _rebindText;
    [SerializeField] Button _resetButton;

    private void OnEnable()
    {
        _rebindButton.onClick.AddListener(() => DoRebind());
        _resetButton.onClick.AddListener(() => ResetBinding());

        if(_inputActionReference != null)
        {
            GetBindingInfo();
            UpdateUI();
        }
    }

    private void OnValidate()
    {
        if (_inputActionReference == null) return;
        GetBindingInfo();
        UpdateUI();
    }

    void GetBindingInfo()
    {
        if(_inputActionReference.action != null)
        {
            _actionName = _inputActionReference.name;
        }

        if(_inputActionReference.action.bindings.Count > _selectedBinding)
        {
            _inputBinding = _inputActionReference.action.bindings[_selectedBinding];
            _bindingIndex = _selectedBinding;
        }
    }

    void UpdateUI()
    {
        if (_actionText != null)
        {

        }
            _actionText.text = _actionName;
            if(_rebindText != null)
            {
                if(Application.isPlaying)
                {

                }
                else
                {
                _rebindText.text = _inputActionReference.action.GetBindingDisplayString(_bindingIndex);
                }
            }
    }

    void DoRebind()
    {

    }

    void ResetBinding()
    {

    }
}
