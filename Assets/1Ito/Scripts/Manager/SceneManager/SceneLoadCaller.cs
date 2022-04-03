using UnityEngine;

public class SceneLoadCaller : MonoBehaviour
{
    [SerializeField] string _sceneName = null;

    public void LoadSceneString()
    {
        SceneLoder.Instance.LoadScene(_sceneName);
    }

    /// <summary>
    /// 現在開いているシーンをロードする
    /// </summary>
    public void LoadSameScene()
    {
        SceneLoder.Instance.LoadScene(SceneLoder.Instance.ActiveSceneName);
    }
}
