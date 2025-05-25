using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MyGameManager
{
    private FigureSpawner _spawner;
    private ActionBar _actionBar;

    [Inject]
    public MyGameManager( FigureSpawner spawner, ActionBar actionBar)
    {
        _actionBar = actionBar;
        ActionBar.OnSlotsEndedEvent += GameOver; // todo EventBus замутить
        ActionBar.OnFigurinesEndedEvent += Win;
        _spawner = spawner;
    }

    public void GameOver()
    {
        Debug.Log($"Game Over {this}");
    }
    
    public void Win()
    {
        Debug.Log($"You Win {this}");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    internal void StartGame()
    {
        _actionBar.Activate();
        _spawner.StartGame();
    }
}
