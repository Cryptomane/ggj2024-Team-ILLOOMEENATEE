using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomShake : MonoBehaviour
{
	[SerializeField] ShakeBehavior m_ShakeShakeShake;
	[SerializeField] float m_Duration;
	[SerializeField] float m_Magnitude;

	private void OnEnable()
	{
		m_ShakeShakeShake.TriggerShake(m_Duration, m_Magnitude);
	}
}
