using UnityEngine;

public class ControlsScreen : MonoBehaviour
{
	public void Init(GameObject CommandsPrefab)
	{
		GameObject instance = GameObject.Instantiate(CommandsPrefab, transform, false);

		instance.transform.localPosition = Vector3.zero;
		gameObject.SetActive(true);
	}
}
