using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using TMPro;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class KeyConfig : MonoBehaviour
{
    [SerializeField] string _pad;
    [SerializeField] InputActionReference _actionReference;

    [SerializeField] GameObject _overlay;
    [SerializeField] TextMeshProUGUI _label;

    InputAction _inputAction;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Config()
    {
        void Done(RebindingOperation op)
        {
            _overlay.SetActive(false);
            op.Dispose();
        }

        _overlay.SetActive(true);

        _inputAction.PerformInteractiveRebinding()
            .WithTargetBinding(_inputAction.GetBindingIndex(_pad))
            //.WithControlsExcluding()
            .OnCancel(op =>
            {
                Done(op);
            })
            .OnComplete(op =>
            {
                UpdateDisplay();
                Done(op);
            })
            .Start();
    }

    void UpdateDisplay()
    {
        var bindingIndex = _inputAction.GetBindingIndex(_pad);
        var keycode = _inputAction.bindings[bindingIndex].ToDisplayString();
        _label.text = keycode;
    }
}
