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
    public Text bestScoreText;

    public string playerName;
    public string newPlayerName;
    public int newHighScore;
    public int bestScore;

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
        newPlayerName = playerInput.GetComponent<Text>().text;
    }

    public void SetBestScore()
    {
        if (newHighScore > bestScore)
        {
            bestScore = newHighScore;
            newPlayerName = playerName;
            bestScoreText.text = $"Best Score : {playerName} : {bestScore}";
        }

    }

    [System.Serializable]

    class SaveData
    {
        public Text bestScoreText;
        public string playerName;
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScoreText = bestScoreText;
        data.playerName = playerName;
        data.bestScore = bestScore;

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
            playerName = data.playerName;
            bestScore = data.bestScore;
        }
    }
}
