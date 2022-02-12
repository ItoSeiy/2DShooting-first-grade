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
    [SerializeField] int _testScore;

    void Awake()
    {
    }

    void Update()
    {

    }

    /// <summary>
    /// 得点を加算し、表示を更新する
    /// </summary>
    /// <param name="score">追加する点数</param>
    public void UIScoreChange(int score, int maxScore)
    {
        int tempScore = int.Parse(_scoreText.ToString());

        score = Mathf.Min(tempScore + score, maxScore);

        if (score <= maxScore)
        {
            DOTween.To(() => score,
                x => score = x,
                score,
                _scoreChangeInterval)
                .OnComplete(() => _scoreText.text = _testScore.ToString());
        }

    }
}
