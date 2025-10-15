using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState state;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch(newState)
        {
            case GameState.Run:
                break;
            case GameState.inBossFight:
                break;
            case GameState.endBossFight:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);

        }
    }
}
public enum GameState
{
    Run,
    inBossFight,
    endBossFight,
    Lose
}
