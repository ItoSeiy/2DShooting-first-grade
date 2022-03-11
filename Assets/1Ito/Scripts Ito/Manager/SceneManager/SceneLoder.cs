using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// シングルトンパターンのシーンを遷移させるスクリプト
/// </summary>
public class SceneLoder : SingletonMonoBehaviour<SceneLoder>
{
    [SerializeField]
    Image _fadeImage = null;
    [SerializeField]
    float _fadeTime = 1;
    float _timer = 0;
    bool _isLoad = false;

    /// <summary>ロードが終わった際に呼び出されるデリゲート</summary>
    public event Action OnLoadEnd;
    /// <summary>
    /// シーンをロードする関数
    /// </summary>
    /// <param name="sceneName">遷移したいシーンの名前</param>
    public void LoadScene (string sceneName)
    {
        if (_isLoad) return;
        StartCoroutine(Fade(sceneName));
    }

    IEnumerator Fade(string sceneName)
    {
        _isLoad = true;

        Color c;
        while (_fadeImage.color.a < 1)
        {
            c = _fadeImage.color;
            _timer += Time.deltaTime;
            c.a = _timer / _fadeTime;
            _fadeImage.color = c;
            yield return new WaitForEndOfFrame();
        }

        yield return SceneManager.LoadSceneAsync(sceneName);

        OnLoadEnd?.Invoke();

        while (_fadeImage.color.a >  0)
        {
            c = _fadeImage.color;
            _timer -= Time.deltaTime;
            c.a = _timer / _fadeTime;
            _fadeImage.color = c;
            yield return new WaitForEndOfFrame();
        }
        _isLoad = false; 
    }
}
