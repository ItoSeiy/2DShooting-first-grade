using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
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

    void Start()
    {
        _pauseButton.performed += _ => Pause();
    }

    public void Pause()
    {
        _paused = !_paused;
        if(_paused)
        {
            Time.timeScale = 0;
            _canvas.enabled = true;
        }
        else
        {
            Time.timeScale = 1;
            _canvas.enabled = false;
        }
        Debug.Log(_paused);
    }

}
