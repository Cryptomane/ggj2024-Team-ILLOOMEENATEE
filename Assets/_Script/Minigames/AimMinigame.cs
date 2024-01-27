using UnityEngine;

public class AimMinigame : MonoBehaviour
{
	[SerializeField]
	private Animator m_Animator;

	[SerializeField]
	private AimController controller;

	[SerializeField]
	private FloatValue difficulty;

	private Minigame minigame;


	private int m_SuccessTrigger = Animator.StringToHash("Success");
	private int m_FailHighTrigger = Animator.StringToHash("FailHigh");
	private int m_FailLowTrigger = Animator.StringToHash("FailLow");

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
			m_Animator.SetTrigger(m_SuccessTrigger);
			return;
		}

		if (controller.IsHigher())
		{
			minigame.AddToScore(2);
			m_Animator.SetTrigger(m_FailHighTrigger);
			return;
		}

		m_Animator.SetTrigger(m_FailLowTrigger);
	}
}
