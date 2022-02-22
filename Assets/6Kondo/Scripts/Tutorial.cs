using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [SerializeField, Header("移動チュートリアルのチェック")] GameObject _moveCheck;
    [SerializeField, Header("移動チュートリアルの対応キー")] InputAction _inputMove;
    [SerializeField, Header("射撃チュートリアルのチェック")] GameObject _shotCheck;
    [SerializeField, Header("射撃チュートリアルの対応キー")] InputAction _inputShot;
    [SerializeField, Header("精密操作のチュートリアルのチェック")] GameObject _slowMoveCheck;
    [SerializeField, Header("精密操作のチュートリアルの対応キー")] InputAction _inputSlowMove;
    private void Start()
    {
        MoveTutorial();
        ShotTutorial();
        SlowMoveTutorial();
    }
    private void OnEnable()
    {
        _inputMove.Enable();
        _inputShot.Enable();
        _inputSlowMove.Enable();
    }
    private void OnDisable()
    {
        _inputMove.Disable();
        _inputShot.Disable();
        _inputSlowMove.Disable();
    }
    private void SetMoveCheck()
    {
        _moveCheck.SetActive(true);
    }
    private void MoveTutorial()
    {            
        _inputMove.performed += _ => SetMoveCheck();
    }
    private void SetShotCheck()
    {
        _shotCheck.SetActive(true);
    }
    private void ShotTutorial()
    {
        _inputShot.performed += _ => SetShotCheck();
    }
    private void SetSlowMoveCheck()
    {
        _slowMoveCheck.SetActive(true);
    }
    private void SlowMoveTutorial()
    {
        _inputShot.performed += _ => SetSlowMoveCheck();
    }
}
