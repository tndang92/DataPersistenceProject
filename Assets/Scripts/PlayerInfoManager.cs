using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager PlayerInfoInstance;

    public GameObject playerInput;
    public GameObject bestScoreText;

    public int b_Score;
    public string playerName;

    private void Awake()
    {

        if (PlayerInfoInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        PlayerInfoInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName()
    {
        playerName = playerInput.GetComponent<Text>().text;
    }

    [System.Serializable]

    class SaveData
    {
        public GameObject bestScoreText;
        public string playerName;
        public int b_Score;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScoreText = bestScoreText;
        data.b_Score = b_Score;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScoreText = data.bestScoreText;
            b_Score = data.b_Score;
        }
    }
}
