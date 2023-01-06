using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public static UserInfo Instance;

    public string userName;

    public string BestName = "NoBody";

    public int BestScore = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHistory();
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void UpdateHistory(int score)
    {
        if (score > BestScore)
        {
            BestScore = score;
            BestName = name;
        }
        SaveHistory();
    }

    public void SaveHistory()
    {
        SaveData data = new SaveData();
        data.name = BestName;
        data.score = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHistory()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestName = data.name;
            BestScore = data.score;
        }
    }

}
