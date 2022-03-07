using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Tutorial : SingletonMonoBehaviour<Tutorial>
{
    [SerializeField, Header("移動チュートリアルのチェック")] GameObject _moveCheck;
    [SerializeField, Header("移動チュートリアルの対応キー")] InputAction _inputMove;
    [SerializeField, Header("ショットチュートリアルのチェック")] GameObject _shotCheck;
    [SerializeField, Header("ショットチュートリアルの対応キー")] InputAction _inputShot;
    [SerializeField, Header("精密操作のチュートリアルのチェック")] GameObject _slowMoveCheck;
    [SerializeField, Header("精密操作のチュートリアルの対応キー")] InputAction _inputSlowMove;
    [SerializeField, Header("チャージショットチュートリアルのチェック")] GameObject _chargeShotCheck;
    [SerializeField, Header("チャージショットチュートリアルの対応キー")] InputAction _inputChargeShot;
    [SerializeField, Header("アイテム取得のチュートリアルのチェック")] GameObject _itemCheck;
    [SerializeField, Header("ボム取得のチュートリアルのチェック")] GameObject _bombCheck;
    [SerializeField, Header("残機取得のチュートリアルのチェック")] GameObject _residueCheck;
    [SerializeField, Header("無敵アイテム取得のチュートリアルのチェック")] GameObject _invisibleCheck;
    [SerializeField, Header("パワーアイテム取得のチュートリアルのチェック")] GameObject _powerCheck;
    [SerializeField, Header("スコアアイテム取得のチュートリアルのチェック")] GameObject _scoreCheck;
    [SerializeField, Header("チュートリアルクリア時に表示")] GameObject _tutorialClearCheck;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.SaveValue();
        GameManager.Instance.InitValue();
    }

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
    private void MoveTutorial()
    {
        _inputMove.performed += _ =>
        {
            _moveCheck.SetActive(true);
            CheckClear();
        };
    }
    private void ShotTutorial()
    {
        _inputShot.started += _ =>
        {
            _shotCheck.SetActive(true);
            CheckClear();
        };
    }
    private void CharegeShotTutorial()
    {
        _inputChargeShot .performed += _ => 
        {
            _chargeShotCheck.SetActive(true);
            CheckClear();
        };
    }
    private void SlowMoveTutorial()
    {
        _inputSlowMove.performed += _ =>
        {
            _slowMoveCheck.SetActive(true);
            CheckClear();
        };
    }
    public void GetItemTutorial()
    {
        _itemCheck.SetActive(true);
        CheckClear();
    }
    public void BombTutorial()
    {
        _bombCheck.SetActive(true);
        CheckClear();
    }
    public void ResidueTutorial()
    {
        _residueCheck.SetActive(true);
        CheckClear();
    }
    public void InvisibleTutorial()
    {
        _invisibleCheck.SetActive(true);
        CheckClear();
    }
    public void PowerTutorial()
    {
        _powerCheck.SetActive(true);
        CheckClear();
    }
    public void ScoreTutorial()
    {
        _scoreCheck.SetActive(true);
        CheckClear();
    }
    private void CheckClear()
    {
        var isClear = _moveCheck.activeSelf 
                   && _shotCheck.activeSelf
                   && _chargeShotCheck.activeSelf
                   && _slowMoveCheck.activeSelf
                   && _itemCheck.activeSelf
                   && _bombCheck.activeSelf
                   && _residueCheck.activeSelf
                   && _invisibleCheck.activeSelf
                   && _powerCheck.activeSelf
                   && _scoreCheck.activeSelf;
        if (isClear)
        {
            _tutorialClearCheck.SetActive(true);
            GameManager.Instance.Player.CanMove = false;
            GameManager.Instance.ReturnValue();
        }
    }
}
