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
	[SerializeField] private FloatValue m_Difficulty;
	[SerializeField] private float m_IncreaseFactorDifficulty = 0.5f;

	[Header("Game Elements")]
	[SerializeField] private RoundIntroScreen m_RoundIntro;
	[SerializeField] private ControlsScreen m_Commands;
	[SerializeField] private ResultsScreen m_Results;

	private static event Action<Minigame> m_OnGameStartRequested;

	public static event Action<Minigame> OnGameStartRequested
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
	private int m_Loops;

	private void Start()
	{
		m_Results.OnOutOfLives += HandleOnOutOfLives;
		m_Loops = 0;
		m_Difficulty.Value = 1;
		m_CurrentState = GameState.INTRO;
		ShowIntro();
	}

	private void OnDestroy()
	{
		m_Results.OnOutOfLives -= HandleOnOutOfLives;
	}

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
				if (InputManager.GetKeyDown(InputManager.Key.ANY))
				{
					m_Minigames.InitRound();

                    ShowControlsScreen();
				}
				break;
			case GameState.CONTROLS:
				if (InputManager.GetKeyDown(InputManager.Key.ANY))
				{
					StartMinigame();
				}
				break;
			case GameState.MINIGAME:
				if (m_Timer <= 0)
				{
                    ShowResultsScreen();
				}
				m_Timer -= Time.deltaTime;
				break;
			case GameState.RESULT:
				if (InputManager.GetKeyDown(InputManager.Key.ANY))
				{
					ShowControlsScreen();
                }
				break;
			case GameState.GAME_OVER:
					SceneManager.LoadScene("GameOver");
				break;
			default:
				break;
		}
	}

	public void ShowIntroPanel()
	{
        if (m_CurrentState == GameState.RESULT)
        {
            HideResults();
        }

		m_CurrentState = GameState.INTRO;

        ShowIntro();
    }

	private void ShowIntro()
	{
		m_RoundIntro.Show(m_Loops);
	}

    private void HideIntro()
    {
        m_RoundIntro.Hide();
    }

    private void ShowCommands()
    {
        m_Commands.Show(m_CurrentMinigame);
    }

    private void HideCommands()
    {
        m_Commands.Hide();
    }

    private void ShowControlsScreen()
	{
		m_CurrentMinigame = m_Minigames.GetNextMinigame();
		if(m_CurrentMinigame == null)
		{
			m_Loops++;
			m_Difficulty.Value += m_IncreaseFactorDifficulty;
			m_NextState = GameState.INTRO;
            ShowIntroPanel();
			return;
		}

        if (m_CurrentState == GameState.INTRO)
        {
            StartCoroutine(WaitAndExecute(HideIntro, 0.7f));
        }
        else if (m_CurrentState == GameState.RESULT)
		{
            StartCoroutine(WaitAndExecute(HideResults, 0.7f));
        }

		m_NextState = GameState.CONTROLS;
		m_CurrentState = GameState.TRANSITION;
		m_Transitioning = true;
		StartCoroutine(LoadMinigameScene(m_CurrentMinigame.SceneName));
        StartCoroutine(WaitAndExecute(ShowCommands, 0.7f));
    }

    private void ShowResults()
    {
        m_Results.Show(m_CurrentMinigame);
    }

    private void HideResults()
    {
        m_Results.Hide();
    }

    private void ShowResultsScreen()
	{
		m_NextState = GameState.RESULT;
		m_CurrentState = GameState.TRANSITION;
		m_Transitioning = true;
		StartCoroutine(UnloadMinigameScene(m_CurrentMinigame.SceneName));
		StartCoroutine(WaitAndExecute(ShowResults, 0.7f));
	}

	private void HandleOnOutOfLives()
	{
		m_NextState = GameState.GAME_OVER;
	}

	private void StartMinigame()
	{
		HideCommands();

		m_Timer = m_CurrentMinigame.Duration;
		m_CurrentState = GameState.MINIGAME;
		m_CurrentMinigame.ResetScore();
        m_OnGameStartRequested?.Invoke(m_CurrentMinigame);
	}

	private IEnumerator WaitAndExecute(Action action, float waitTime)
	{
		yield return new WaitForSeconds(waitTime);

		action?.Invoke();
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

	private IEnumerator UnloadMinigameScene(string sceneName)
	{
		m_TransitionAnimator.SetTrigger(m_TransitionStartAnimationTrigger);

		yield return new WaitForSecondsRealtime(0.7f);

		AsyncOperation loading = SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.None);

		while (!loading.isDone)
		{
			yield return new WaitForEndOfFrame();
		}

		m_TransitionAnimator.SetTrigger(m_TransitionEndAnimationTrigger);
		m_Transitioning = false;
	}
}
