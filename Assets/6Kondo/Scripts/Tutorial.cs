using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour
{
    [SerializeField, Header("移動チュートリアルのチェック")] GameObject _moveCheck;
    [SerializeField, Header("移動チュートリアルの対応キー")] InputAction _inputMove;
    [SerializeField, Header("ショットチュートリアルのチェック")] GameObject _shotCheck;
    [SerializeField, Header("ショットチュートリアルの対応キー")] InputAction _inputShot;
    [SerializeField, Header("チャージショットチュートリアルのチェック")] GameObject _chargeShotCheck;
    [SerializeField, Header("チャージショットチュートリアルの対応キー")] InputAction _inputChargeShot;
    [SerializeField, Header("精密操作のチュートリアルのチェック")] GameObject _slowMoveCheck;
    [SerializeField, Header("精密操作のチュートリアルの対応キー")] InputAction _inputSlowMove;
    private void Start()
    {
        MoveTutorial();
        ShotTutorial();
        SlowMoveTutorial();
        CharegeShotTutorial();
    }
    private void OnEnable()
    {
        _inputMove.Enable();
        _inputShot.Enable();
        _inputSlowMove.Enable();
        _inputChargeShot.Enable();
    }
    private void OnDisable()
    {
        _inputMove.Disable();
        _inputShot.Disable();
        _inputSlowMove.Disable();
        _inputChargeShot.Disable();
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
    private void SetChargeCheck()
    {
        _chargeShotCheck.SetActive(true);
    }
    private void CharegeShotTutorial()
    {
        _inputChargeShot.performed += _ => SetChargeCheck();
    }
    private void SetSlowMoveCheck()
    {
        _slowMoveCheck.SetActive(true);
    }
    private void SlowMoveTutorial()
    {
        _inputSlowMove.performed += _ => SetSlowMoveCheck();
    }
}
