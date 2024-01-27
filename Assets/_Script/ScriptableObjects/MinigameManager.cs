using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


public class MinigameManager : MonoBehaviour
{
    public enum GameState
    {
        INTRO,          //Introduce game
        COMMANDS,       //Introduce commands
        MINIGAME,       //minigame section start
        RESULT          //Result logic that select next minigame, round or game over
    }
    //public GameState state;
    [SerializeField] private GameState m_currentState;

    //Duration for each State
    [SerializeField] private float m_stateDuration;
    public MinigameSelection m_selectedGame;
    [SerializeField] private Minigame m_currentMinigame;

    [SerializeField] private float m_difficulty = 1.0f;
    [SerializeField] private float m_increaseFactorDifficulty = 0.5f;

    public float Difficulty { get => m_difficulty; set => m_difficulty = value; }
    public float IncreaseFactorDifficulty { get => m_increaseFactorDifficulty; set => m_increaseFactorDifficulty = value; }

    private void Start()
    {
        
    }

    private void Update()
    {
        switch (m_currentState)
        {
            case GameState.INTRO:
                if (Input.anyKey)
                {
                    IntroRound();
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
        if (m_currentState == GameState.COMMANDS)
        {
            
        }
        if (m_currentState == GameState.MINIGAME)
        {
            startMinigame(m_currentMinigame.Duration);
        }
        if (m_currentState == GameState.RESULT)
        {

        }
    }

    public void IntroRound()
    {
        m_selectedGame.InitRound();
        SceneManager.LoadScene(m_currentMinigame.SceneName, LoadSceneMode.Single);
        showCommands(GameState.COMMANDS);
        //if (m_selectedGame.getMinigame() != null)
        //{
        //Show Commands Camera
        //    currentState = GameState.MINIGAME;
        //}
    }

    private void showCommands(GameState cOMMANDS)
    {
        //ShowCommand Camera
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
        m_currentState = nextState;
    }
}
