using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeftRightMinigame : MonoBehaviour
{
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
    private float decreaseScoreTime = 0.5f;

    [Space]

    [SerializeField]
    private FloatValue difficulty;

    [SerializeField]
    private UnityEvent onLeft;
    [SerializeField]
    private UnityEvent onRight;
    [SerializeField]
    private UnityEvent onGameEnded;


    [Header("Read-only")]

    [SerializeField]
    private float currentDecreaseScoreTime;

    private Minigame minigame;

    private bool isLastPressedLeft = true;

	private int m_SuccessTrigger = Animator.StringToHash("Success");

    public bool GameStarted { get; private set; }


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

        GameStarted = true;

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
            currentDecreaseScoreTime = decreaseScoreTime / difficulty.Value;
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
        if (GameStarted)
        {
            if (!isLastPressedLeft)
            {
                minigame.AddToScore(1);
                onLeft?.Invoke();

                CheckEnd();
            }

            isLastPressedLeft = true;
        }
    }

    private void OnRightArrowHit()
    {
        if (GameStarted)
        {
            if (isLastPressedLeft)
            {
                minigame.AddToScore(1);
                onRight?.Invoke();

                CheckEnd();
            }

            isLastPressedLeft = false;
        }
    }

    private void CheckEnd()
    {
        if(minigame.GetScore() >= minigame.ScoreGoal)
        {
            onGameEnded?.Invoke();

            enabled = false;
			m_Animator.SetTrigger(m_SuccessTrigger);
        }
    }
}
