using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<GameState> OnGameStateChanged;

    public static GameManager Instance;

    public GameState CurrentState;

    public Transform PlayerTransform;

    private void Awake()
    {
        Instance = this;
        CurrentState = GameState.Waiting;
    }


    private void Update()
    {
        StartGame();
    }
    private void StartGame()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CurrentState = GameState.Play;
            OnGameStateChanged?.Invoke(GameState.Play);
        }
    }

}
