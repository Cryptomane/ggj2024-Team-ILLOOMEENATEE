using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetMinigame : MonoBehaviour
{
    [SerializeField]
    private int maxRandomItems = 5;

    [SerializeField]
    private int itemsCountMultiplier = 3;

    [Space]

    [SerializeField]
    private Transform[] targetItemsA;
    [SerializeField]
    private Transform[] targetItemsB;
    [SerializeField]
    private Transform[] targetItemsC;

    [Space]

    [SerializeField]
    private FloatValue difficulty;

    private Minigame minigame;

    private int correctControllerIndex;

    public bool GameStarted { get; private set; }

    private void OnEnable()
    {
        InputManager.OnAHit += OnAHit;
        InputManager.OnBHit += OnBHit;
        InputManager.OnCHit += OnCHit;

        MinigameManager.OnGameStartRequested += StartMinigame;
    }

    private void OnDisable()
    {
        InputManager.OnAHit -= OnAHit;

        MinigameManager.OnGameStartRequested -= StartMinigame;

        StopAllCoroutines();
    }

    private void OnAHit()
    {
        CheckHit(0);
    }

    private void OnBHit()
    {
        CheckHit(1);
    }

    private void OnCHit()
    {
        CheckHit(2);
    }

    private void CheckHit(int controllerIndex)
    {
        if (GameStarted)
        {
            if (controllerIndex == correctControllerIndex)
            {
                minigame.AddToScore(1);
            }
            else
            {
                minigame.AddToScore(0);
            }
        }
    }

    private void StartMinigame(Minigame minigame)
    {
        this.minigame = minigame;

        InitTargets();

        GameStarted = true;
    }

    private void InitTargets()
    {
        int randomItems = maxRandomItems + 1;

        int itemsCount = Random.Range(1, Mathf.Min(10, randomItems + Mathf.RoundToInt(difficulty.Value * itemsCountMultiplier)));

        int r1 = Random.Range(1, randomItems);
        int r2 = Random.Range(1, randomItems);

        int badItemsCount1 = itemsCount + r1;
        int badItemsCount2 = itemsCount + r2;

        correctControllerIndex = Random.Range(0, 3);

        switch(correctControllerIndex)
        {
            case 0:
                ActivateRandomTargetItems(targetItemsA, itemsCount);
                ActivateRandomTargetItems(targetItemsB, badItemsCount1);
                ActivateRandomTargetItems(targetItemsC, badItemsCount2);
                break;
            case 1:
                ActivateRandomTargetItems(targetItemsA, badItemsCount1);
                ActivateRandomTargetItems(targetItemsB, itemsCount);
                ActivateRandomTargetItems(targetItemsC, badItemsCount2);
                break;
            case 2:
                ActivateRandomTargetItems(targetItemsA, badItemsCount1);
                ActivateRandomTargetItems(targetItemsB, badItemsCount2);
                ActivateRandomTargetItems(targetItemsC, itemsCount);
                break;
        }
    }

    private void ActivateRandomTargetItems(Transform[] items, int count)
    {
        items.Shuffle();
        for (int i = 0; i < items.Length; i++)
        {
            items[i].gameObject.SetActive(i < count);
        }
    }

}
