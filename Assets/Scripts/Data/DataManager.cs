using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour {

    public static DataManager instance = null;  // Singleton
    public static Save save = new Save();

    private string savePath;
    private string dataJson;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        savePath = Path.Combine(Application.persistentDataPath, "Save.json");
    }
    public void CreateGameFiles()
    {
        if(!File.Exists(savePath))
        {
            TextAsset sav = Resources.Load("Save") as TextAsset;
            save = JsonUtility.FromJson<Save>(sav.text);
            File.WriteAllText(Application.persistentDataPath + "/Save.json", JsonUtility.ToJson(save, true));
        }
    }
    public bool CheckFiles()
    {
        if (File.Exists(savePath))
            return true;
        else
            return false;
    }
    public void LoadData()
    {
        if(File.Exists(savePath))
        {
            save = JsonUtility.FromJson<Save>(File.ReadAllText(savePath));
            WWW reader = new WWW(savePath);
            while(!reader.isDone) {}
            dataJson = reader.text;
        }
    }
    public void SaveData()
    {
        File.WriteAllText(savePath, JsonUtility.ToJson(save, true));
    }
    void OnApplicationPause(bool pause)
    {
        if(pause)
            SaveData();
    }
    public void GenerateJsonFileWithExhibits(Exhibit[] exhs)
    {
        CreateGameFiles();
        for (int i = 0; i < exhs.Length; i++)
        {
            ExhibitData ed = new ExhibitData();
            ed.id = exhs[i].id;
            ed.isExplored = exhs[i].isExplored;
            save.exhibits.Add(ed);
        }
        SaveData();
        LoadData();
    }
}
[Serializable]
public class Save
{
    public List <ExhibitData> exhibits;
}
[Serializable]
public class ExhibitData
{
    public int id;
    public bool isExplored;
}
