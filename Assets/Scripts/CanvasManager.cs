﻿using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI timeChange;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _victoryPanel;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void RestartGame()
    {

        _gameManager.RestartGame();
    }

    public void IncreaseTimer(float amount)
    {
        int roundAmount = Mathf.RoundToInt(amount);

        timeChange.gameObject.SetActive(true);
        timeChange.color = Color.green;
        timeChange.text = $"+{roundAmount}";
        StartCoroutine(DoAfterTime(2f, () =>
        {
            timeChange.gameObject.SetActive(false);
        }));
    }
    public void DecreaseTimer(float amount)
    {
        int roundAmount = Mathf.RoundToInt(amount);

        timeChange.gameObject.SetActive(true);
        timeChange.color = Color.red;
        timeChange.text = $"-{roundAmount}";
        StartCoroutine(DoAfterTime(2f, () =>
        {
            timeChange.gameObject.SetActive(false);
        }));
    }

    public void ShowGameOver()
    {
        StartCoroutine(DoAfterTime(1f, () => 
        {
            _gameOverPanel.SetActive(true);
            _victoryPanel.SetActive(false);
        }));
    }

    public void ShowVictory()
    {
        StartCoroutine(DoAfterTime(1f, () =>
        {
            _gameOverPanel.SetActive(false);
            _victoryPanel.SetActive(true);
        }));
    }

    public void UpdateTimer(float currentTime)
    {
        timeText.text = "Time: " + Mathf.RoundToInt(currentTime).ToString();
    }

    private IEnumerator DoAfterTime(float delay, Action action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action?.Invoke();
    }
}