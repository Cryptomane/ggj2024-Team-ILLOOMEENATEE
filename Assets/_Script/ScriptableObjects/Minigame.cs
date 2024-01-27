using UnityEngine;

[CreateAssetMenu(fileName = "Minigame_A", menuName = "ILLOOMEENATEE/Minigame", order = -10)]
public class Minigame : ScriptableObject
{
	[SerializeField] private string m_SceneName = "";
	[SerializeField] private string m_GameName = "";
	[SerializeField] private string m_InstructionsText = "";
	[SerializeField] private GameObject m_InstructionsControlsPrefab;
	[SerializeField] private int m_ScoreGoal = 100;
	[SerializeField] private float m_Duration = 6f;
	[SerializeField, Range(.1f, .9f)] private float m_MinTarget;
	[SerializeField, Range(.1f, .9f)] private float m_MaxTarget;

	private int m_RoundScore;

	public string SceneName => m_SceneName;
	public string Name => m_GameName;
	public string InstructionsText => m_InstructionsText;
	public GameObject InstructionsControlsPrefab => m_InstructionsControlsPrefab;
	public int ScoreGoal  => m_ScoreGoal;
	public float Duration  => m_Duration;
	public float MinTargetValue  => m_MinTarget;
	public float MaxTargetValue  => m_MaxTarget;

	public void Init()
	{
		m_RoundScore = 0;
	}

	public bool Success()
	{
		return m_RoundScore >= m_ScoreGoal;
	}

	public void AddToScore(int a)
	{
		m_RoundScore += a;
	}

	/// <summary>
	/// Usiamo questo per i fallimenti di diverso tipo:
	/// </summary>
	/// <returns>1 - Successo; 2 - Failed High; 0 - Failed Low</returns>
	public int GetScore()
	{
		return m_RoundScore;
	}
}