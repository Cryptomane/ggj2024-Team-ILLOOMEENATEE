using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMinigame : MonoBehaviour
{
    [SerializeField]
    private AimController controller;

    [SerializeField]
    private FloatValue difficulty;

    private Minigame minigame;

    private void OnEnable()
    {
        InputManager.OnAHit += OnAHit;

        MinigameManager.OnGameStartRequested += StartMinigame;
    }

    private void OnDisable()
    {
        InputManager.OnAHit -= OnAHit;

        MinigameManager.OnGameStartRequested -= StartMinigame;
    }
    private void StartMinigame(Minigame minigame)
    {
        this.minigame = minigame;

        InitControllers();
    }
    private void InitControllers()
    {
        controller.Initialize(difficulty.Value);
    }

    private void OnAHit()
    {
        if (controller.CheckHit())
        {
            minigame.AddToScore(1);
        }
        else if(controller.IsHigher())
        {
            minigame.AddToScore(2);
        }
    }

}
