using System;
using TMPro;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{
	[SerializeField] GameObject m_Score;
	[SerializeField] TMP_Text m_PointsCurrent;
	[SerializeField] TMP_Text m_Goal;
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

		if (minigame.ScoreGoal != 1)
		{
			m_Score.SetActive(true);
			m_PointsCurrent.text = minigame.GetScore().ToString();
			m_Goal.text = minigame.ScoreGoal.ToString();
		}
		else
		{
			m_Score.SetActive(false);
		}
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
