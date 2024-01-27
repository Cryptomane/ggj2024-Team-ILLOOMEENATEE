using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameSelection", menuName = "ILLOOMEENATEE/MinigameSelection", order = 0)]
public class MinigameSelection : ScriptableObject
{
	[SerializeField] private List<Minigame> m_Minigames;

	private List<Minigame> m_AvailableMinigames;

	public void InitRound()
	{
		m_AvailableMinigames = new List<Minigame>(m_Minigames);
	}

	public Minigame GetNextMinigame()
	{
		if(m_AvailableMinigames.Count == 0)
		{
			return null;
		}

		int index = Random.Range(0, m_AvailableMinigames.Count);

		Minigame minigame = m_AvailableMinigames[index];
		m_AvailableMinigames.RemoveAt(index);
		return minigame;
	}
}