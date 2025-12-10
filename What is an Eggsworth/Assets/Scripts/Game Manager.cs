using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

    public CinemachineVirtualCamera[] cameras;
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera bossCam;
    private CinemachineVirtualCamera currentCam;

    [SerializeField] private BlueBoss bb;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        currentCam = playerCam;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
        }
    }
    public void SwitchCam(CinemachineVirtualCamera Newcam)
    {
        currentCam = Newcam;
        currentCam.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }

    public void inBossRoom()
    {
        state = GameState.inBossFight;
        bb.playerIsHere();
        SwitchCam(bossCam);
    }

    public void bossDefeated()
    {
        Debug.Log("ahhhh");
        state = GameState.endBossFight;
        SwitchCam(playerCam);
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
