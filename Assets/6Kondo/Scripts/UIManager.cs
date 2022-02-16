using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField, Header("‰½•b‚©‚¯‚Ä•Ï‰»‚³‚¹‚é‚©")] float _scoreChangeInterval = default;
    [SerializeField] Text _scoreText;
    [SerializeField, Header("‰½•b‚©‚¯‚Ä•Ï‰»‚³‚¹‚é‚©")] float _residuesChangeInterval = default;
    [SerializeField] Text _residueText;
    [SerializeField, Header("‰½•b‚©‚¯‚Ä•Ï‰»‚³‚¹‚é‚©")] float _bombChangeInterval = default;
    [SerializeField] Text _bombText;
    [SerializeField] Text _invisibleLimitText;
    [SerializeField, Header("‰½•b‚©‚¯‚Ä•Ï‰»‚³‚¹‚é‚©")] float _invisibleCountChangeInterval = default;
    [SerializeField] Text _invisibleCountText;


    int _score;
    int _residue;
    int _bomb;
    int _maxScore;
    int _maxResidue;
    int _maxBomb;
    int _invisibleLimit;
    int _invisibleCount;

    public void Start()
    {
        _maxScore = GameManager.Instance.PlayerScoreLimit;
        _maxResidue = GameManager.Instance.PlayerResidueLimit;
        _maxBomb = GameManager.Instance.PlayerBombCount;
        _invisibleLimit = GameManager.Instance.PlayerInvicibleLimit;
    }

    /// <summary>
    /// “¾“_‚Ì•\¦‚ğXV‚·‚é
    /// </summary>
    /// <param name="score">’Ç‰Á‚·‚é“_”</param>
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
                .OnStepComplete(() => _bombText.text = GameManager.Instance.PlayerBombCount.ToString("00"));
        }
    }
}
