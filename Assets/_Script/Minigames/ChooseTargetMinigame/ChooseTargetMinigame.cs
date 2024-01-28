using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTargetMinigame : MonoBehaviour
{
    [SerializeField]
    private int maxRandomItems = 5;


    [Space]

    [SerializeField]
    private Transform[] targetItemsA;
    [SerializeField]
    private Transform[] targetItemsB;
    [SerializeField]
    private Transform[] targetItemsC;

    [Space]

    [SerializeField]
    private float movingObjectSpeed = 10;

    [SerializeField]
    private Transform movingObject;
    [SerializeField]
    private GameObject explosion;

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
        InputManager.OnBHit -= OnBHit;
        InputManager.OnCHit -= OnCHit;

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

            Vector3 dest;

            switch(controllerIndex)
            {
                case 0:
                    dest = targetItemsA[0].parent.position;
                    break;
                case 1:
                    dest = targetItemsB[0].parent.position;
                    break;
                default:
                    dest = targetItemsC[0].parent.position;
                    break;
            }

            GameStarted = false;

            StartCoroutine(MoveObject(dest));
        }
    }

    private IEnumerator MoveObject(Vector3 destination)
    {
        Vector3 origin = movingObject.transform.position;

        float timer = 0;
        while(timer < 1)
        {
            movingObject.position = Vector3.Lerp(origin, destination, timer);

            yield return null;

            timer += movingObjectSpeed * Time.deltaTime;
        }

        movingObject.position = destination;

        Explode();
    }

    private void Explode()
    {
        explosion.SetActive(true);
    }

    private IEnumerator CountDownExplosion(Minigame minigame)
    {
        yield return new WaitForSeconds(minigame.Duration);

        Explode();
    }

    private void StartMinigame(Minigame minigame)
    {
        this.minigame = minigame;

        InitTargets();

        GameStarted = true;

        StartCoroutine(CountDownExplosion(minigame));
    }

    private void InitTargets()
    {
        int randomItems = maxRandomItems + 1;

        int itemsCount = Random.Range(1, Mathf.Min(10, randomItems + Mathf.RoundToInt(difficulty.Value * 3)));

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
