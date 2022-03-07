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
    int _tempScore = 0;
    private void Start()
    {
        switch (_value)
        {
            case Value.Score:

                _text.text = GameManager.Instance.PlayerScore.ToString("00000000");

                //DOTween.To(() => _tempScore,
                //    x => _tempScore = x,
                //    GameManager.Instance.PlayerScore,
                //    _changeSpeed)
                //    .OnUpdate(() => _text.text = _tempScore.ToString("00000000"))
                //    .OnComplete(() => _text.text = GameManager.Instance.PlayerScore.ToString("00000000"));
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
