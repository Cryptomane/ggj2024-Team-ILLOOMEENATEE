using UnityEngine;
using UnityEngine.SceneManagement;

public class PressPlay : MonoBehaviour
{
	void Update()
	{
		if(Input.anyKeyDown)
		{
			SceneManager.LoadScene("MinigameContainer");
		}
	}
}
