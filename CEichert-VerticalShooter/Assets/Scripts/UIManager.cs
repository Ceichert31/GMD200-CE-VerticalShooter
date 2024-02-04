using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int score = 0;

    [SerializeField] private TextMeshProUGUI 
        scoreText,
        endScoreText,
        endTimeText;

    [SerializeField] private GameObject deathScreen;

    private float timer;

    public delegate void EnableDeathScreen();
    public static EnableDeathScreen enable;

    public delegate void AddPoints(int points);
    public static AddPoints addPoints;

    private void Update()
    {
        timer += Time.deltaTime;
    }
    void DeathUI()
    {
        deathScreen.SetActive(true);
        endScoreText.text = score.ToString();
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);
        endTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void AddPointsToScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
    private void OnEnable()
    {
        enable += DeathUI;
        addPoints += AddPointsToScore;
    }
    private void OnDisable()
    {
        enable -= DeathUI;
        addPoints -= AddPointsToScore;
    }
}
