using System;
using TMPro;
using UnityEngine;

public class ResultsScreen : MonoBehaviour
{
	[SerializeField] TMP_Text m_Score;
	[SerializeField] Animator m_Animator;

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

	public void Show(Minigame minigame)
	{
		gameObject.SetActive(true);
		m_Animator.SetTrigger(minigame.Success() ? m_SuccessAnimParam : m_FailAnimParam);

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
		Destroy(transform.GetChild(0));
		gameObject.SetActive(false);
	}
}
