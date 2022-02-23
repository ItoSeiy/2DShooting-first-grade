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
    [SerializeField, Header("無敵オブジェクト所持数を何秒かけて変化させるか")] float _invicibleObjectLimitChangeInterval = default;
    [SerializeField] Text _invicibleObjectCountText;
    [SerializeField, Header("無敵オブジェクト必要数を何秒かけて変化させるか")] float _invicibleObjectCountChangeInterval = default;
    [SerializeField] Text _invicibleObjectLimitText;
    [SerializeField, Header("パワーアイテム所持数を何秒かけて変化させるか")] float _powerItemCountChamgeInterval = default;
    [SerializeField] Text _powerItemCountText;
    [SerializeField, Header("パワーアイテム必要数を何秒かけて変化させるか")] float _powerItemLimitChangeInterval = default;
    [SerializeField] Text _powerItemLimitText;
    [SerializeField] Text _levelText;

    int _firstScore;
    int _firstResidue;
    int _firstBomb;
    int _firstInvicibleObject;
    int _firstPowerItem;
    int _maxScore;
    int _maxResidue;
    int _maxBomb;
    int _maxInvicibleObject;
    int _maxPowerItem;
    int _invicibleObjectLimit;
    int _invicibleObjectCount;
    int _powerItemLimit2;
    int _powerItemLimit3;
    int _powerItemCount;
    int _level;
    const int _default = 0;
    const int Level1 = 1;
    const int Level2 = 2;
    const int Level3 = 3;

    public void UISet()
    {
        _firstScore = GameManager.Instance.PlayerScore;
        _scoreText.text = _firstScore.ToString("00000000");

        _firstResidue = GameManager.Instance.PlayerResidueCount;
        _residueText.text = _firstResidue.ToString("00");

        _firstBomb = GameManager.Instance.PlayerBombCount;
        _bombText.text = _firstBomb.ToString("00");

        _firstInvicibleObject = GameManager.Instance.PlayerInvincibleObjectCount;
        _invicibleObjectCountText.text = _firstInvicibleObject.ToString("000");
       
        _maxInvicibleObject = GameManager.Instance.PlayerInvicibleObjectLimit;

        _firstPowerItem = GameManager.Instance.PlayerPowerItemCount;
        _powerItemCountText.text = _firstPowerItem.ToString("000");

        _maxScore = GameManager.Instance.PlayerScoreLimit;

        _maxResidue = GameManager.Instance.PlayerResidueLimit;

        _maxBomb = GameManager.Instance.PlayerBombLimit;

        _maxPowerItem = GameManager.Instance.PlayerPowerLimit;
        
        _powerItemLimit2 = GameManager.Instance.PlayerPowerRequiredNumberLevel2;
        _powerItemLimit3 = GameManager.Instance.PlayerPowerRequiredNumberLevel3;
        _powerItemLimitText.text = _powerItemLimit2.ToString("000");

        _invicibleObjectLimit = GameManager.Instance.PlayerInvicibleObjectLimit;
        _invicibleObjectLimitText.text = _invicibleObjectLimit.ToString("000");

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
                GameManager.Instance.PlayerScore,
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
                GameManager.Instance.PlayerResidueCount,
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
                GameManager.Instance.PlayerBombCount,
                _bombChangeInterval)
                .OnUpdate(() => _bombText.text = tempBomb.ToString("00"))
                .OnComplete(() => _bombText.text = GameManager.Instance.PlayerBombCount.ToString("00"));
        }
    }
    public void UIInvisibleCountChange(int invisible)
    {
        int tempInvisible = int.Parse(_invicibleObjectCountText.text.ToString());

        tempInvisible = Mathf.Min(tempInvisible + invisible, _maxInvicibleObject);

        if (tempInvisible <= _maxInvicibleObject)
        {
            DOTween.To(() => tempInvisible,
                x => tempInvisible = x,
                GameManager.Instance.PlayerInvincibleObjectCount,
                _invicibleObjectCountChangeInterval)
                .OnUpdate(() => _invicibleObjectCountText.text = tempInvisible.ToString("000"))
                .OnComplete(() => _invicibleObjectCountText.text = GameManager.Instance.PlayerInvincibleObjectCount.ToString("000"));
        }
    }
    public void UIPowerLimitChange(int power)
    {
        switch(GameManager.Instance.PlayerLevel)
        {
            case Level1:
                int tempPower = int.Parse(_powerItemLimitText.text.ToString());

                tempPower = Mathf.Min(tempPower + power, _powerItemLimit2);
                DOTween.To(() => tempPower,
                    x => tempPower = x,
                    _powerItemLimit2,
                    _powerItemLimitChangeInterval)
                    .OnUpdate(() => _powerItemLimitText.text = tempPower.ToString("000"))
                    .OnComplete(() => _powerItemLimitText.text = _powerItemLimit2.ToString("000"));
                UILevelChange();
                break;
            case Level2:
                int tempPower2 = int.Parse(_powerItemLimitText.text.ToString());

                tempPower2 = Mathf.Min(tempPower2 + power, _powerItemLimit3);
                DOTween.To(() => tempPower2,
                    x => tempPower2 = x,
                    _powerItemLimit3,
                    _powerItemLimitChangeInterval)
                    .OnUpdate(() => _powerItemLimitText.text = tempPower2.ToString("000"))
                    .OnComplete(() => _powerItemLimitText.text = _powerItemLimit3.ToString("000"));
                UILevelChange();
                break;
            case Level3:
                int tempPower3 = int.Parse(_powerItemLimitText.text.ToString());

                tempPower3 = Mathf.Min(tempPower3 + power, _maxPowerItem);
                DOTween.To(() => tempPower3,
                    x => tempPower3 = x,
                    _maxPowerItem,
                    _powerItemLimitChangeInterval)
                    .OnUpdate(() => _powerItemLimitText.text = tempPower3.ToString("000"))
                    .OnComplete(() => _powerItemLimitText.text = _maxPowerItem.ToString("000"));
                UILevelChange();
                break;
        }
    }
    public void UIPowerCountChange(int power)
    {
        int tempPower = int.Parse(_powerItemCountText.text.ToString());

        tempPower = Mathf.Min(tempPower + power, _maxPowerItem);
        if(tempPower <= _default)
        {
            tempPower = _default;
        }
        if (tempPower <= _maxPowerItem)
        {
            DOTween.To(() => tempPower,
                x => tempPower = x,
                GameManager.Instance.PlayerPowerItemCount,
                _powerItemCountChamgeInterval)
                .OnUpdate(() => _powerItemCountText.text = tempPower.ToString("000"))
                .OnComplete(() => _powerItemCountText.text = GameManager.Instance.PlayerPowerItemCount.ToString("000"));
        }
    }
    void UILevelChange()
    {
        _levelText.text = GameManager.Instance.PlayerLevel.ToString("0");
    }
}
