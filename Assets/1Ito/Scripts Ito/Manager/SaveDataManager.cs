using Overdose.Data;
using System.IO;
using UnityEngine;

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
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
}
