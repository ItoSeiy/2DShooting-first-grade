using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class TextValueChange : MonoBehaviour
{
    [SerializeField]
    Text _text;
    [SerializeField]
    float _changeSpeed = 2f;
    [SerializeField]
    [Header("どの値をテキストに反映させるか")]
    Value _value;
    [SerializeField]
    SoundType _onScoreCountSoundType = SoundType.ScoreCount;
    [SerializeField]
    [Header("スコアを反映し始めるまでの時間(ミリ秒)")]
    int _scoreTextChangeDelay = 2000;
    int _tempScore = 0;
    private async void Start()
    {
        switch (_value)
        {
            case Value.Score:
                await Task.Delay(_scoreTextChangeDelay);
                DOTween.To(() => _tempScore,
                    x => _tempScore = x,
                    GameManager.Instance.PlayerScore,
                    _changeSpeed)
                    .OnUpdate(() =>
                    {
                        _text.text = _tempScore.ToString("00000000");
                        SoundManager.Instance.UseSound(_onScoreCountSoundType);
                    })
                    .OnComplete(() =>
                    {
                        _text.text = GameManager.Instance.PlayerScore.ToString("00000000");
                        GameManager.Instance.InitValue();
                    });
                break;

            case Value.Level:
                _text.text = GameManager.Instance.PlayerLevel.ToString();
                break;
        }
    }

    enum Value
    {
        Score,
        Level
    }
}
