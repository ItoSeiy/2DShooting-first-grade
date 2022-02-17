using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField, Header("スコアを何秒かけて変化させるか")] float _scoreChangeInterval = default;
    [SerializeField] Text _scoreText;
    [SerializeField, Header("残機数を何秒かけて変化させるか")] float _residuesChangeInterval = default;
    [SerializeField] Text _residueText;
    [SerializeField, Header("ボム数を何秒かけて変化させるか")] float _bombChangeInterval = default;
    [SerializeField] Text _bombText;
    [SerializeField, Header("無敵オブジェクト所持数を何秒かけて変化させるか")] float _invisibleObjectLimitChangeInterval = default;
    [SerializeField] Text _invisibleObjectCountText;
    [SerializeField, Header("無敵オブジェクト必要数を何秒かけて変化させるか")] float _invisibleObjectCountChangeInterval = default;
    [SerializeField] Text _invisibleObjectLimitText;
    [SerializeField, Header("パワーアイテム所持数を何秒かけて変化させるか")] float _powerItemCountChamgeInterval = default;
    [SerializeField] Text _powerItemCountText;
    [SerializeField, Header("パワーアイテム必要数を何秒かけて変化させるか")] float _powerItemLimitChangeInterval = default;
    [SerializeField] Text _powerItemLimitText;
    [SerializeField] Text _levelText;

    int _score;
    int _residue;
    int _bomb;
    int _maxScore;
    int _maxResidue;
    int _maxBomb;
    int _invisibleObjectLimit;
    int _invisibleObjectCount;
    int _powerItemLimit2;
    int _powerItemLimit3;
    int _powerItemCount;
    int _level;
    const int Level3 = 3; 

    public void UISet()
    {
        _maxScore = GameManager.Instance.PlayerScoreLimit;

        _maxResidue = GameManager.Instance.PlayerResidueLimit;

        _maxBomb = GameManager.Instance.PlayerBombCount;
        
        _powerItemLimit2 = GameManager.Instance.PlayerPowerRequiredNumberLevel2;
        _powerItemLimit3 = GameManager.Instance.PlayerPowerRequiredNumberLevel3;
        _powerItemLimitText.text = _powerItemLimit2.ToString("000");

        _invisibleObjectLimit = GameManager.Instance.PlayerInvicibleObjectLimit;
        _invisibleObjectLimitText.text = _invisibleObjectLimit.ToString("000");

        _level = GameManager.Instance.PlayerLevel;
        _levelText.text = _level.ToString("1");
    }

    /// <summary>
    /// 得点の表示を更新する
    /// </summary>
    /// <param name="score">追加する点数</param>
    public void UIScoreChange(int score)
    {
        int tempScore = int.Parse(_scoreText.text.ToString());

        tempScore = Mathf.Min(tempScore + score, _maxScore);

        if (tempScore <= _maxScore)
        {
            DOTween.To(() => tempScore,
                x => tempScore = x,
                score,
                _scoreChangeInterval)
                .OnUpdate(() => _scoreText.text = tempScore.ToString("00000000"))
                .OnComplete(() => _scoreText.text = GameManager.Instance.PlayerScore.ToString("00000000"));
        }
    }
    public void UIResidueChange(int residue)
    {
        int tempResidues = int.Parse(_residueText.text.ToString());

        tempResidues = Mathf.Min(tempResidues + residue, _maxResidue);

        if (tempResidues <= _maxResidue)
        {
            DOTween.To(() => tempResidues,
                x => tempResidues = x,
                residue,
                _residuesChangeInterval)
                .OnUpdate(() => _residueText.text = tempResidues.ToString("00"))
                .OnComplete(() => _residueText.text = GameManager.Instance.PlayerResidueCount.ToString("00"));
        }
    }
    public void UIBombChange(int bomb)
    {
        int tempBomb = int.Parse(_bombText.text.ToString());

        tempBomb = Mathf.Min(tempBomb + bomb, _maxBomb);

        if (tempBomb <= _maxBomb)
        {
            DOTween.To(() => tempBomb,
                x => tempBomb = x,
                bomb,
                _bombChangeInterval)
                .OnUpdate(() => _bombText.text = tempBomb.ToString("00"))
                .OnComplete(() => _bombText.text = GameManager.Instance.PlayerBombCount.ToString("00"));
        }
    }
    public void UIInvisibleLimitChange(int invisible)
    {
        int tempInvisible = int.Parse(_invisibleObjectLimitText.text.ToString());

        tempInvisible = Mathf.Min(tempInvisible + invisible, _invisibleObjectLimit);

        if (tempInvisible <= _invisibleObjectLimit)
        {
            DOTween.To(() => tempInvisible,
                x => tempInvisible = x,
                invisible,
                _invisibleObjectLimitChangeInterval)
                .OnUpdate(() => _invisibleObjectLimitText.text = tempInvisible.ToString("000"))
                .OnComplete(() => _invisibleObjectLimitText.text = GameManager.Instance.PlayerInvicibleObjectLimit.ToString("00"));
        }
    }
    public void UIInvisibleCountChange(int invisible)
    {
        int tempInvisible = int.Parse(_invisibleObjectCountText.text.ToString());

        tempInvisible = Mathf.Min(tempInvisible + invisible, _invisibleObjectCount);

        if (tempInvisible <= _invisibleObjectCount)
        {
            DOTween.To(() => tempInvisible,
                x => tempInvisible = x,
                invisible,
                _invisibleObjectCountChangeInterval)
                .OnUpdate(() => _invisibleObjectCountText.text = tempInvisible.ToString("000"))
                .OnComplete(() => _invisibleObjectCountText.text = GameManager.Instance.PlayerInvincibleObjectCount.ToString("000"));
        }
    }
    public void UIPowerLimitChange(int power)
    {
        if (GameManager.Instance.PlayerLevel == Level3)
        {
            int tempPower = int.Parse(_powerItemLimitText.text.ToString());

            tempPower = Mathf.Min(tempPower + power, _powerItemLimit3);
            DOTween.To(() => tempPower,
                x => tempPower = x,
                power,
                _powerItemLimitChangeInterval)
                .OnUpdate(() => _powerItemCountText.text = tempPower.ToString("000"))
                .OnComplete(() => _powerItemLimitText.text = _powerItemLimit3.ToString("000"));
        }
    }
    public void UIPowerCountChange(int power)
    {
        int tempPower = int.Parse(_powerItemCountText.text.ToString());

        tempPower = Mathf.Min(tempPower + power, _powerItemCount);

        if (tempPower <= _powerItemCount)
        {
            DOTween.To(() => tempPower,
                x => tempPower = x,
                power,
                _powerItemCountChamgeInterval)
                .OnUpdate(() => _powerItemCountText.text = tempPower.ToString("000"))
                .OnComplete(() => _powerItemCountText.text = GameManager.Instance.PlayerInvincibleObjectCount.ToString("000"));
        }
    }
}
