using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int startingTime = 20;
    [SerializeField] Text timeText;
    [SerializeField] CanvasManager _canvas;

    private float currentTime;
    private bool isGameOver;

    void Start()
    {
        currentTime = startingTime;

        if (_canvas == null)
            _canvas = FindObjectOfType<CanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
    }

    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void UpdateTime()
    {
        if (isGameOver)
            return;

        currentTime -= 1 * Time.deltaTime;
        _canvas.UpdateTimer(currentTime);

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        if(!isGameOver)
        {
            isGameOver = true;
            _canvas.ShowGameOver();
        }
    }

    public void DecreaseTimer(float amount)
    {
        currentTime -= amount;
        _canvas.DecreaseTimer(amount);
    }

    public void FoundChest()
    {
        isGameOver = true;
        _canvas.ShowVictory();
    }
}