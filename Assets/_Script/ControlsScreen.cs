using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
	public void Show(GameObject CommandsPrefab)
	{
		GameObject instance = GameObject.Instantiate(CommandsPrefab, transform, false);

		instance.transform.localPosition = Vector3.zero;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		Destroy(transform.GetChild(0));
		gameObject.SetActive(false);
	}
}
