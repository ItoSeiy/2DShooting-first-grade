using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadForString : MonoBehaviour
{
    [SerializeField] string _sceneName = null;

    public void LoadScene()
    {
        SceneLoder.Instance.LoadScene(_sceneName);
    }
}
