using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TextValueChange : MonoBehaviour
{
    [SerializeField]
    Text _text;
    [SerializeField]
    float _changeSpeed = 2f;
    [SerializeField, Header("どの値をテキストに反映させるか")]
    Value _value;
    private void Start()
    {
        switch (_value)
        {
            case Value.Score:
                int tempScore = 0;

                DOTween.To(() => tempScore,
                    x => tempScore = x,
                    GameManager.Instance.PlayerScore,
                    _changeSpeed)
                    .OnUpdate(() => _text.text = tempScore.ToString("00000000"))
                    .OnComplete(() => _text.text = GameManager.Instance.PlayerScore.ToString("00000000"));
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
