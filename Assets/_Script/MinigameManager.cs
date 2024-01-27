using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MinigameManager : MonoBehaviour
{
	public enum GameState
	{
		INTRO,          // Introduce round
		TRANSITION,     // Transition between states
		CONTROLS,       // Introduce commands
		MINIGAME,       // Minigame section start
		RESULT,          // Result logic that select next minigame, round or game over
		GAME_OVER          // Result logic that select next minigame, round or game over
	}

	[SerializeField] private Animator m_TransitionAnimator;

	[Header("Data")]
	[SerializeField] private MinigameSelection m_Minigames;

	[Header("Difficulty Settings")]
	[SerializeField] private float m_Difficulty = 1.0f;
	[SerializeField] private float m_IncreaseFactorDifficulty = 0.5f;

	[Header("Game Elements")]
	[SerializeField] private GameObject m_RoundIntro;
	[SerializeField] private ControlsScreen m_Commands;
	[SerializeField] private GameObject m_Result;

	private static event Action m_OnGameStartRequested;

	public static event Action OnGameStartRequested
	{
		add
		{
			m_OnGameStartRequested -= value;
			m_OnGameStartRequested += value;
		}
		remove
		{
			m_OnGameStartRequested -= value;
		}
	}

	private Minigame m_CurrentMinigame;
	private GameState m_CurrentState;
	private GameState m_NextState;
	private bool m_Transitioning;
	private float m_Timer;

	private int m_TransitionStartAnimationTrigger = Animator.StringToHash("start");
	private int m_TransitionEndAnimationTrigger = Animator.StringToHash("end");

	private void Update()
	{
		switch (m_CurrentState)
		{
			case GameState.TRANSITION:
				if (!m_Transitioning)
				{
					m_CurrentState = m_NextState;
				}
				break;
			case GameState.INTRO:
				if (Input.anyKey)
				{
					m_Minigames.InitRound();
					ShowControlsScreen();
				}
				break;
			case GameState.CONTROLS:
				if (Input.anyKey)
				{
					StartMinigame();
				}
				break;
			case GameState.MINIGAME:
				if (m_Timer <= 0)
				{
				}
				m_Timer -= Time.deltaTime;

				break;
			case GameState.RESULT:
				break;
			case GameState.GAME_OVER:
				break;
			default:
				break;
		}
	}

	public void ShowControlsScreen()
	{
		m_NextState = GameState.CONTROLS;
		m_CurrentState = GameState.TRANSITION;
		m_Transitioning = true;
		m_CurrentMinigame = m_Minigames.GetNextMinigame();
		StartCoroutine(LoadMinigameScene(m_CurrentMinigame.SceneName));
		m_Commands.Show(m_CurrentMinigame.InstructionsControlsPrefab);
	}

	private void StartMinigame()
	{
		m_Timer = m_CurrentMinigame.Duration;
		m_CurrentState = GameState.MINIGAME;
		m_Commands.Hide();
		m_OnGameStartRequested?.Invoke();
	}

	private IEnumerator LoadMinigameScene(string sceneName)
	{
		m_TransitionAnimator.SetTrigger(m_TransitionStartAnimationTrigger);

		yield return new WaitForSecondsRealtime(0.7f);
		AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

		while (!loading.isDone)
		{
			yield return new WaitForEndOfFrame();
		}

		m_TransitionAnimator.SetTrigger(m_TransitionEndAnimationTrigger);
		m_Transitioning = false;
	}
}
