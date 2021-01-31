using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] float _delayEndGame = 2f;
    [SerializeField] float _delayTimeChange = 2f;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI timeChange;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] UnityEvent OnGameOver;
    [SerializeField] GameObject _victoryPanel;
    [SerializeField] UnityEvent OnVictory;

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
        StartCoroutine(DoAfterTime(_delayTimeChange, () =>
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
        StartCoroutine(DoAfterTime(_delayTimeChange, () =>
        {
            timeChange.gameObject.SetActive(false);
        }));
    }

    public void ShowGameOver()
    {
        StartCoroutine(DoAfterTime(_delayEndGame, () => 
        {
            _gameOverPanel.SetActive(true);
            _victoryPanel.SetActive(false);
            OnGameOver?.Invoke();
        }));
    }

    public void ShowVictory()
    {
        StartCoroutine(DoAfterTime(_delayEndGame, () =>
        {
            _gameOverPanel.SetActive(false);
            _victoryPanel.SetActive(true);
            OnVictory?.Invoke();
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