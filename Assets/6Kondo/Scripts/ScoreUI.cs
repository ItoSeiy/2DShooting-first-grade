using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreUI : MonoBehaviour
{
    [SerializeField, Header("ScoreText")] Text _scoreText;
    float _score;

    void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    void Update()
    {
        _score += GameManager.Instance.PlayerScore;
        _scoreText.text = _score.ToString("0000000000");
    }
}
