using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveBindings : MonoBehaviour
{
    public InputActionAsset _actions;

    public void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if(!string.IsNullOrEmpty(rebinds))
        {
            //_actions.LoadBindingOverridesFromJson(rebinds);
        }
    }

    public void OnDisable()
    {
        //var rebinds = _actions.SaveBindingOverridesAsJson();
        //PlayerPrefs.SetString("rebinds",rebinds);
    }
}
