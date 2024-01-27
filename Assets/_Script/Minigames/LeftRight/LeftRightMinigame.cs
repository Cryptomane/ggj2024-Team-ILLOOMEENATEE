using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightMinigame : MonoBehaviour
{
    [SerializeField]
    private float decreaseScoreTime = 0.5f;
    [SerializeField]
    private float decreaseScoreTimeMultiplier = 0.8f;

    [Space]

    [SerializeField]
    private FloatValue difficulty;

    [Header("Read-only")]

    [SerializeField]
    private float currentDecreaseScoreTime;

    private Minigame minigame;

    private bool isLastPressedLeft;

    private void OnEnable()
    {
        InputManager.OnLeftArrowHit += OnLeftArrowHit;
        InputManager.OnRightArrowHit += OnRightArrowHit;

        MinigameManager.OnGameStartRequested += StartMinigame;
    }

    private void OnDisable()
    {
        InputManager.OnLeftArrowHit -= OnLeftArrowHit;
        InputManager.OnRightArrowHit -= OnRightArrowHit;

        MinigameManager.OnGameStartRequested -= StartMinigame;

        StopAllCoroutines();
    }

    private void StartMinigame(Minigame minigame)
    {
        this.minigame = minigame;

        Initialize();

        StartCoroutine(DecreaseScore());
    }

    private void Initialize()
    {
        if (difficulty.Value <= 1)
        {
            currentDecreaseScoreTime = decreaseScoreTime;
        }
        else
        {
            currentDecreaseScoreTime = decreaseScoreTimeMultiplier / (difficulty.Value - 1) * decreaseScoreTime;
        }
    }

    private IEnumerator DecreaseScore()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(currentDecreaseScoreTime);

        while (true)
        {
            yield return waitForSeconds;

            minigame.AddToScore(-1);
        }
    }

    private void OnLeftArrowHit()
    {
        if (minigame.GetScore() == 0 || !isLastPressedLeft)
        {
            minigame.AddToScore(1);
        }

        isLastPressedLeft = true;
    }

    private void OnRightArrowHit()
    {
        if (minigame.GetScore() == 0 || isLastPressedLeft)
        {
            minigame.AddToScore(1);
        }

        isLastPressedLeft = false;
    }
}
