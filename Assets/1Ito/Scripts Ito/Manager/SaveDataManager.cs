using Overdose.Data;
using System.IO;
using UnityEngine;

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    public SaveData SaveData { get; set; }

    public int Player1StageConut => _player1StageConut;
    public int Player2StageCount => _player2StageCount;

    [SerializeField]
    private int _player1StageConut = 5;
    [SerializeField]
    private int _player2StageCount = 5;

    void Start()
    {
        Reset();
        Save(SaveData);
    }

    public void Save(SaveData saveData)
    {
        var jsonSaveDataStr = JsonUtility.ToJson(saveData);

        var writer = new StreamWriter(Application.dataPath + "/SaveData/SaveData.json", false);
        writer.Write(jsonSaveDataStr);
        writer.Flush();
        writer.Close();
    }

    public SaveData Load()
    {
        var reader = new StreamReader(Application.dataPath + "/SaveData/SaveData.json");
        var jsonSaveDataStr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<SaveData>(jsonSaveDataStr);
    }

    /// <summary>セーブデータのリセット</summary>
    public void Reset()
    {
        SaveData = new SaveData(_player1StageConut, _player2StageCount);
    }
}
