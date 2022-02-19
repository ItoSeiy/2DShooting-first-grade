using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject _checkMove;
    [SerializeField] InputAction _input;

    private void Start()
    {
        MoveTutorial();
    }
    private void SetCheck()
    {
        _checkMove.SetActive(true);
    }
    private void MoveTutorial()
    {            
        _input.performed += _ => SetCheck();
    }
}
