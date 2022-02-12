using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField, Header("何秒かけて変化させるか")] float _scoreChangeInterval = default;
    [SerializeField] Text _scoreText;
    [SerializeField] float _testScore;
    [SerializeField] int _maxScore = 999999999;
    int _score;

    void Update()
    {

    }

    /// <summary>
    /// 得点を加算し、表示を更新する
    /// </summary>
    /// <param name="score">追加する点数</param>
    public void UIScoreChange(int score)
    {
        _score = Mathf.Min(_score + score, _maxScore);

        if (_score <= _maxScore)
        {
            DOTween.To(() => _score,
                x => _score = x,
                _score,
                _scoreChangeInterval);
        }

    }
}
