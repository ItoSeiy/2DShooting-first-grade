using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var gameManager = target as GameManager;
        EditorGUI.BeginDisabledGroup(gameManager.IsGameStart);
        if(GUILayout.Button("GameStart"))
        {
            gameManager.TrySettingScene();
        }
        EditorGUI.EndDisabledGroup();
    }
}
