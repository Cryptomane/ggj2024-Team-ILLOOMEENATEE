using TMPro;
using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
	[SerializeField] TMP_Text m_Instructions;
	public void Show(Minigame minigame)
	{
		GameObject instance = GameObject.Instantiate(minigame.InstructionsControlsPrefab, transform, false);

		instance.transform.localPosition = Vector3.zero;
		m_Instructions.text = minigame.InstructionsText;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		Destroy(transform.GetChild(1).gameObject);
		gameObject.SetActive(false);
	}
}
