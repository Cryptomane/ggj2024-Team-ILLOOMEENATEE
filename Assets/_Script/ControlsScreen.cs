using TMPro;
using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
	[SerializeField] TMP_Text m_Instructions;

	private GameObject instance;
	public void Show(Minigame minigame)
	{
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = GameObject.Instantiate(minigame.InstructionsControlsPrefab, transform, false);

		instance.transform.localPosition = Vector3.zero;
		m_Instructions.text = minigame.InstructionsText;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		Destroy(instance.gameObject);
		gameObject.SetActive(false);
	}
}
