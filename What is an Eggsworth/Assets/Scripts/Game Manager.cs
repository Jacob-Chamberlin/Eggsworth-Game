using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public Camera mainCam;
    public Camera bossCam;

    [SerializeField] private BlueBoss bb;

    private void Awake()
    {
        instance = this;
    }
    private void Start ()
    {
        mainCam.gameObject.SetActive(true);
        bossCam.gameObject.SetActive(false);
    }

    public void inBossRoom()
    {
        mainCam.gameObject.SetActive(false);
        bossCam.gameObject.SetActive(true);
        state = GameState.inBossFight;
        bb.playerIsHere();
    }

    public void bossDefeated()
    {
        Debug.Log("ahhhh");
        state = GameState.endBossFight;
        mainCam.gameObject.SetActive(true);
        bossCam.gameObject.SetActive(false);
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
            case GameState.Win:
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
    Lose,
    Win,
}
