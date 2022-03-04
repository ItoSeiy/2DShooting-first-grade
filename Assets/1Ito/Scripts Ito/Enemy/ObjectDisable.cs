using UnityEngine;
using System.Threading.Tasks;

public class ObjectDisable : MonoBehaviour
{
    [SerializeField, Header("オブジェクトを非アクティブ化する時間(ミリ秒)")]
    int _disableTime = 1500;
    async private void OnEnable()
    {
        await Task.Delay(_disableTime);
        gameObject.SetActive(false);
    }
}
