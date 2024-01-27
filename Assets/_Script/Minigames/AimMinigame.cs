using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimMinigame : MonoBehaviour
{
    [SerializeField]
    private AimController controller;

    [SerializeField]
    private int difficulty = 1;

    private void OnEnable()
    {
        InputManager.OnAHit += OnAHit;
    }

    private void OnDisable()
    {
        InputManager.OnAHit -= OnAHit;
    }
    private void Start()
    {
        controller.StartAiming(difficulty);
    }

    private void OnAHit()
    {
        if (controller.CheckHit())
        {
            Debug.Log("Aim is right on hit.");
        }

        difficulty++;

        controller.StartAiming(difficulty);
    }

}
