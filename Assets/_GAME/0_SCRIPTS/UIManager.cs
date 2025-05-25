using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gamoverPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] TMP_Text textCount;
    [SerializeField] Slider slider;

    private MyGameManager _gameManager;
    private FigureSpawner _spawner;
    private GameSettings _settings;

    [Inject]
    public void Construct(MyGameManager gameManager, FigureSpawner spawner, GameSettings settings)
    {
        _gameManager = gameManager;
        _spawner = spawner;
        _settings = settings;
    }

    private void OnEnable()
    {
        ActionBar.OnSlotsEndedEvent += OnGameOver;
        ActionBar.OnFigurinesEndedEvent += OnWin;
        slider.value = 5;
    }

    private void OnDisable()
    {
        ActionBar.OnSlotsEndedEvent -= OnGameOver;
        ActionBar.OnFigurinesEndedEvent -= OnWin;
    }
    private void OnGameOver()
    {
        gamePanel.SetActive(false);
        gamoverPanel.SetActive(true);
    }

    private void OnWin()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void OnRestartButtonClicked()
    {
        _gameManager.RestartGame();
    }

    public void OnMixButtonClicked()
    {
        _spawner.RespawnFigurines();
    }

    public void OnStartButtonClicked()
    {
        _settings.uniqueFirurinesCount = (int) slider.value;
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        _gameManager.StartGame();
    }

    public void OnSliderChanged()
    {
        textCount.text = slider.value.ToString();
    }
}
