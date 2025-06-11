using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject gamewinUI;
    private bool isGameOver = false;
    private bool isGameWin = false;
    void Start()
    {
        UpdateScore();
        gameoverUI.SetActive(false); // Hide the Game Over UI at the start
        gamewinUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0; // Stop the game
        gameoverUI.SetActive(true); // Show the Game Over UI
    }

    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gamewinUI.SetActive(true);
    }

    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("Level1");
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }
}   
