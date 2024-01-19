using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class MainManager : MonoBehaviour
{
    /// <summary>
    /// This code enables you to access the MainManager object from any other script
    public static MainManager Instance;
    public Color TeamColor; // new variable declared
    private void Awake()
    {
       //singleton pattern - makes sure there is only one instance
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        //end pattern
        LoadColor();
    }/// </summary>

    ///FOR SAVING BETWEEN SESSIONS
    [System.Serializable]//required for JSON to save file
    class SaveData
    {
        public Color TeamColor;
    }
    ///SAVE COLOR
    ///
    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.TeamColor = TeamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    //LOAD COLOR
    public void LoadColor()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TeamColor = data.TeamColor;
        }
    }
}
