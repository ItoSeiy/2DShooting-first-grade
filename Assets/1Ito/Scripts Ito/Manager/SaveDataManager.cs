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
    private StageData[] _stageData = null;

    protected override void Awake()
    {
        base.Awake();
        Load();

        GameManager.Instance.OnStageClear += Check;
    }

    void OnApplicationQuit()
    {
        Save(SaveData);
    }

    public void Save(SaveData saveData)
    {
        var jsonSaveDataStr = JsonUtility.ToJson(saveData);

        var writer = new StreamWriter(Application.dataPath + "/SaveData/SaveData.json", false);
        writer.Write(jsonSaveDataStr);
        writer.Flush();
        writer.Close();

        SaveData = JsonUtility.FromJson<SaveData>(jsonSaveDataStr);
    }

    public void Load()
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

    private void Check()
    {
        Array.ForEach(_stageData, x =>
        {

        });
    }
}
