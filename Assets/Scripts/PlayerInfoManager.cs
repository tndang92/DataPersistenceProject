using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoManager : MonoBehaviour
{
    public static PlayerInfoManager Instance;

    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI scoreText;

    private int m_Points;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddPoint(int point)
    {
        m_Points += point;
        scoreText.text = $"Score : {m_Points}";
    }

    public void BestScore()
    {
        bestScoreText.text = $"Best {scoreText}";
    }
}
