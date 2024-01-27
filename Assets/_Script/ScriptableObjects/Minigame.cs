using UnityEngine;

[CreateAssetMenu(fileName = "Minigame_A", menuName = "ILLOOMEENATEE/Minigame", order = -10)]
public class Minigame : ScriptableObject
{
	[SerializeField] private string m_SceneName = "";
	[SerializeField] private string m_Name = "";
	[SerializeField] private string m_InstructionsText = "";
	[SerializeField] private GameObject m_InstructionsControlsPrefab;
	[SerializeField] private int m_ScoreGoal = 100;
	[SerializeField] private float m_Duration = 6f;
	[SerializeField, Range(.1f, .9f)] private float m_MinTarget;
	[SerializeField, Range(.1f, .9f)] private float m_MaxTarget;


	public string SceneName => m_SceneName;
	public string Name => m_Name;
	public string InstructionsText => m_InstructionsText;
	public GameObject InstructionsControlsPrefab => m_InstructionsControlsPrefab;
	public int ScoreGoal  => m_ScoreGoal;
	public float Duration  => m_Duration;
	public float MinTargetValue  => m_MinTarget;
	public float MaxTargetValue  => m_MaxTarget;
}