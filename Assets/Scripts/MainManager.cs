using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Rigidbody Ball;

    public Brick brickPrefab;
    public GameObject gameOverText;
    public GameObject newHighScoreText;
    public Text scoreText;

    private PlayerInfoManager playerInfoManager;


    public int m_Points;
    public int lineCount = 6;

    private bool m_Started = false;
   

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        playerInfoManager = GameObject.Find("PlayerInfoManager").GetComponent<PlayerInfoManager>();

        scoreText.text = $"Score : {playerInfoManager.newPlayerName} : 0";

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    public void AddPoint(int point)
    {
        m_Points += point;

        if (m_Points > playerInfoManager.bestScore)
        {
            playerInfoManager.playerNameText.text = playerInfoManager.newPlayerName;
            playerInfoManager.newHighScore = m_Points;
        }

        scoreText.text = $"Score : {playerInfoManager.newPlayerName} : {m_Points}";
    }

    public void GameOver()
    {
        gameOverText.SetActive(true);

        if (playerInfoManager.newHighScore > playerInfoManager.bestScore)
        {
            playerInfoManager.SetBestScore();
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
