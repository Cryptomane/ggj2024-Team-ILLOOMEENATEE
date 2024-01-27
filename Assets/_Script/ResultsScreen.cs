using System;
using TMPro;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{
	[SerializeField] TMP_Text m_Score;
	[SerializeField] Animator m_Animator;
	[SerializeField] GameObject[] m_Lives;

	private static int m_CurrentLives = 3;

	private event Action m_OnOutOfLives;

	public event Action OnOutOfLives
	{
		add
		{
			m_OnOutOfLives -= value;
			m_OnOutOfLives += value;
		}
		remove
		{
			m_OnOutOfLives -= value;
		}
	}

	private int m_SuccessAnimParam = Animator.StringToHash("success");
	private int m_FailAnimParam = Animator.StringToHash("fail");
	private int m_LivesAnimParam = Animator.StringToHash("fail");

	private void Awake()
	{
		m_CurrentLives = 3;
	}

	public void Show(Minigame minigame)
	{
		gameObject.SetActive(true);
		
		for (int i=0; i < m_Lives.Length; i++)
		{
			m_Lives[i].SetActive(i < m_CurrentLives);
		}

		bool success = minigame.Success();

		if(!success)
		{
			m_CurrentLives--;
			
			if(m_CurrentLives == 0)
			{
				m_OnOutOfLives?.Invoke();
			}
		}

		m_Animator.SetTrigger(success ? m_SuccessAnimParam : m_FailAnimParam);

		if (minigame.ScoreGoal != 1)
		{
			m_Score.text = minigame.GetScore().ToString();
		}
		else
		{
			m_Score.text = "";
		}
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
