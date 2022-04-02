using Overdose.Data;
using System.IO;
using UnityEngine;
using System;

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    public SaveData SaveData { get; private set; }

    public int Player1StageConut => _player1StageConut;
    public int Player2StageCount => _player2StageCount;

    [SerializeField]
    private int _player1StageConut = 5;
    [SerializeField]
    private int _player2StageCount = 5;

    [SerializeField]
    private StageData _stageData = null;

    protected override void Awake()
    {
        base.Awake();
        Load();
        GameManager.Instance.OnInGame += SetCurreentStageData;
        GameManager.Instance.OnStageClear += TryOpenStage;
    }

    void OnApplicationQuit()
    {
        Save();
    }

    private void Save()
    {
        var jsonSaveDataStr = JsonUtility.ToJson(SaveData);

        var writer = new StreamWriter(Application.dataPath + "/SaveData/SaveData.json", false);
        writer.Write(jsonSaveDataStr);
        writer.Flush();
        writer.Close();

        SaveData = JsonUtility.FromJson<SaveData>(jsonSaveDataStr);
    }

    private void Load()
    {
        var reader = new StreamReader(Application.dataPath + "/SaveData/SaveData.json");
        var jsonSaveDataStr = reader.ReadToEnd();
        reader.Close();

        SaveData = JsonUtility.FromJson<SaveData>(jsonSaveDataStr);
    }

    /// <summary>セーブデータのリセット</summary>
    public void Reset()
    {
        SaveData = new SaveData(_player1StageConut, _player2StageCount);
    }

    private void SetCurreentStageData()
    {
        _stageData = PhaseNovelManager.Instance.StageData;
    }

    private void TryOpenStage()
    {
        switch (_stageData.PlayerNum)
        {
            case 1:
                SaveData.Player1StageActives[_stageData.StageNum - 1] = true;
                break;
            case 2:
                SaveData.Player2StageActives[_stageData.StageNum - 1] = true;
                break;

            default:
                Debug.LogWarning($"{_stageData.PlayerNum}は存在しません");
                break;
        }
    }
}
