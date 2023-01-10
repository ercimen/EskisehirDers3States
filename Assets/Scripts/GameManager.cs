using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState;

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
        }
    }

}
