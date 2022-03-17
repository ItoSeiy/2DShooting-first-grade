using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Overdose.Data;
using System.IO;

public class SaveDataManager : SingletonMonoBehaviour<SaveDataManager>
{
    [SerializeField]
    private string _jsonPath;

    private SaveData _saveData;
    void Start()
    {
        var saveDataStr = Resources.Load<TextAsset>(_jsonPath).ToString();
        _saveData = JsonUtility.FromJson<SaveData>(saveDataStr);
    }

    void Save(SaveData saveData)
    {
        var saveDataStr = JsonUtility.ToJson(_saveData);
        
    }
}
