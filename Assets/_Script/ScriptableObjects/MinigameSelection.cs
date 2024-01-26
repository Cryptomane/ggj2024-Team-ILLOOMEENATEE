using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Minigame_A", menuName = "ILLOOMEENATEE/MinigameSelection", order = -10)]
public class Minigame : ScriptableObject
{
	[SerializeField] private List<Minigame> m_Minigames;

	private List<Minigame> m_AvailableMinigames;

	public void InitRound()
	{
		m_AvailableMinigames = new List<Minigame>(m_Minigames);
	}

	public bool GetNextMinigame(out Minigame minigame)
	{
		if(m_AvailableMinigames.Count == 0)
		{
			minigame = null;
			return false;
		}

		minigame = m_AvailableMinigames[Random.Range(0, m_AvailableMinigames.Count)];
		return true;
	}
}