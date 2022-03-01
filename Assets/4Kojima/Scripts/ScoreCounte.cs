using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounte : MonoBehaviour
{
    [SerializeField]
    Text _scoreText;

    private void Start()
    {
        _scoreText.text = GameManager.Instance.PlayerScore.ToString("0000000");
    }
}
