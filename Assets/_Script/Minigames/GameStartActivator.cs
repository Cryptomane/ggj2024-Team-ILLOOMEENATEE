using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;

    private void OnEnable()
    {
        MinigameManager.OnGameStartRequested += OnGameStarted;

    }

    private void OnDisable()
    {
        MinigameManager.OnGameStartRequested += OnGameStarted;

    }

    private void OnGameStarted(Minigame minigame)
    {
        for(int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }
}
