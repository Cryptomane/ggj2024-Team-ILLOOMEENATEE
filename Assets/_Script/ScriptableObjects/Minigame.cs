using UnityEngine;

[CreateAssetMenu(fileName = "Minigame_A", menuName = "ILLOOMEENATEE/Minigame", order = -10)]
public class Minigame : ScriptableObject
{
	[SerializeField] private string m_SceneName = "";
	[SerializeField] private string m_Name = "";
	[SerializeField] private string m_InstructionsText = "";
	[SerializeField] private GameObject m_InstructionsControlsPrefab;
	[SerializeField] private int m_ScoreGoal = 100;

	public string SceneName => m_SceneName;
	public string Name => m_Name;
	public string InstructionsText => m_InstructionsText;
	public GameObject InstructionsControlsPrefab => m_InstructionsControlsPrefab;
	public int ScoreGoal  => m_ScoreGoal;
}