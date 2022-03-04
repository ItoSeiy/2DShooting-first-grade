using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    [SerializeField] InputAction _inputPause;
    [SerializeField] GameObject _pausePanel;
    public void Start()
    {
        PauseUI();
    }
    private void PauseUI()
    {
        if (_pausePanel == false)
        {
            _inputPause.performed += _ =>
            {
                _pausePanel.SetActive(true);
            };
            Time.timeScale = 0f;
        }
        else
        {
            _inputPause.performed += _ =>
            {
                _pausePanel.SetActive(false);
            };
            Time.timeScale = 1f;
        }
    }
}
