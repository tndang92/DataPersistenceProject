using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager PlayerInfoInstance;

    public Text playerNameText;
    public Text bestScoreText;
    public GameObject playerInput;

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
        playerNameText.text = playerInput.GetComponent<Text>().text;
        newPlayerName = playerNameText.text;
    }

    public void SetBestScore()
    {
            bestScore = newHighScore;
            newPlayerName = playerNameText.text;
            bestScoreText.text = $"Best Score : {newPlayerName} : {bestScore}";
    }

    [System.Serializable]

    class SaveData
    {
        public Text bestScoreText;
        public Text playerNameText;
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScoreText = bestScoreText;
        data.playerNameText = playerNameText;
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
            playerNameText = data.playerNameText;
            bestScore = data.bestScore;
        }
    }
}
