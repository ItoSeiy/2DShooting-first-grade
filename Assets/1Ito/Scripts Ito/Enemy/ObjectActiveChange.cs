using UnityEngine;
using System.Threading.Tasks;

public class ObjectActiveChange : MonoBehaviour
{
    [SerializeField]
    [Header("オブジェクトのアクティブを変更する時間(ミリ秒)")]
    int _activeChangeTime = 1500;

    [SerializeField]
    [Header("変更後のオブジェクトのセットアクティブ")]
    bool _active = false;

    async private void OnEnable()
    {
        await Task.Delay(_activeChangeTime);
        gameObject.SetActive(_active);
    }
}
