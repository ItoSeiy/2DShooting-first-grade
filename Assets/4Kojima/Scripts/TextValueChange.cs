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
    [SerializeField, Header("どの値をテキストに反映させるか")]
    Value _value;
    private async void Start()
    {
        await Task.Delay(2000);

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
