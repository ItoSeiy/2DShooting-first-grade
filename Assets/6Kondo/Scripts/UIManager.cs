using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField, Header("何秒かけて変化させるか")] float _scoreChangeInterval = default;
    [SerializeField] Text _scoreText;
    [SerializeField, Header("何秒かけて変化させるか")] float _residuesChangeInterval = default;
    [SerializeField] Text _residueText;

    int _score;
    int _residue;
    int _maxScore;
    int _maxResidue;

    public void Start()
    {
        _maxScore = GameManager.Instance.PlayerScoreLimit;
        _maxResidue = GameManager.Instance.PlayerResidueLimit;
    }

    public void TestScore()
    {
        UIScoreChange(_score);
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
                .OnComplete(() => _residueText.text = GameManager.Instance.PlayerResidue.ToString("00"));
        }
    }
}
