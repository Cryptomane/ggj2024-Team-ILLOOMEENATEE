using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MinigameManager : MonoBehaviour
{
	public enum GameState
	{
		INTRO,          // Introduce round
		TRANSITION,     // Transition between states
		COMMANDS,       // Introduce commands
		MINIGAME,       // Minigame section start
		RESULT          // Result logic that select next minigame, round or game over
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

    private Minigame m_CurrentMinigame;
	private GameState m_CurrentState;
	private GameState m_NextState;
	private bool m_Transitioning;

	private int m_TransitionStartAnimationTrigger = Animator.StringToHash("start");
	private int m_TransitionEndAnimationTrigger = Animator.StringToHash("end");
    
 private void Update()
    {
        switch (m_CurrentState)
        {
			case GameState.TRANSITION:
				if(!m_Transitioning)
				{
					m_CurrentState = m_NextState;
				}
				break;
            case GameState.INTRO:
                if (Input.anyKey)
                {
					m_NextState = GameState.COMMANDS;
					m_CurrentState = GameState.TRANSITION;
					m_Transitioning = true;
					m_Minigames.InitRound();
                    ShowCommandsScreen();
                }
                break;
        case GameState.COMMANDS:
                if (Input.anyKey)
                {

                }
                break;
        case GameState.MINIGAME: 
                break;
        case GameState.RESULT: 
                break;
        default: 
                break;
        }
        
        startMinigame(m_CurrentMinigame.Duration);
    }

    public void ShowCommandsScreen()
    {
		m_CurrentMinigame = m_Minigames.GetNextMinigame();
		StartCoroutine(LoadMinigameScene(m_CurrentMinigame.SceneName));
		m_Commands.Init(m_CurrentMinigame.InstructionsControlsPrefab);
    }

	private IEnumerator LoadMinigameScene(string sceneName)
	{
		m_TransitionAnimator.SetTrigger(m_TransitionStartAnimationTrigger);

		yield return new WaitForSecondsRealtime(0.7f);
		AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

		while(!loading.isDone)
		{
			yield return new WaitForEndOfFrame();
		}

		m_TransitionAnimator.SetTrigger(m_TransitionEndAnimationTrigger);
		m_Transitioning = false;
	}

    IEnumerator startMinigame(float seconds)
    {
        float time = seconds;
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        changeStateHandler(GameState.RESULT);
    }

    private void changeStateHandler(GameState nextState) 
    {
        m_CurrentState = nextState;
    }
}
