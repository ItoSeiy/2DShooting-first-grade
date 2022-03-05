using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour, IPause
{
    [SerializeField] InputAction _pauseButton;
    [SerializeField] Canvas _canvas;

    bool _paused = false;

    private void OnEnable()
    {
        _pauseButton.Enable();
    }

    private void OnDisable()
    {
        _pauseButton.Disable();
    }

    public void OnClick()
    {

    }

    void IPause.Pause(bool pause)
    {
        _paused = !_paused;
        if(_paused)
        {
            _canvas.enabled = true;
        }
        else
        {
            _canvas.enabled = false;
        }
        Debug.Log(_paused);
    }

}
