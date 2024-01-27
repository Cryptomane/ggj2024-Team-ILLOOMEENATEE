using System;
using TMPro;
using UnityEngine;

public class RoundIntroScreen : MonoBehaviour
{
	[SerializeField] TMP_Text m_Title;

	const string FirstLoop = "READY?";
	const string NewLoop = "FASTER!";

	public void Show(int loopValue)
	{
		m_Title.text = loopValue == 0 ? FirstLoop : NewLoop;
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
