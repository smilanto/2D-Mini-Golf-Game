StateManager.cs:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum GameState { MainMenu, Playing, Paused, GameOver }
    public GameState currentState;
                private void Start()
    {
        SetGameState(GameState.MainMenu);
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                Debug.Log("Main Menu");
                break;
            case GameState.Playing:
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                Debug.Log("Game Over");
                break;
        }
    }
                public void PauseGame()
    {
        SetGameState(GameState.Paused);
    }
                  public void ResumeGame()
    {
        SetGameState(GameState.Playing);
    }
                  public void EndGame()
    {
        SetGameState(GameState.GameOver);
    }

    public void RestartGame()
    {
        SetGameState(GameState.MainMenu);
    }
}
