using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryMe : MonoBehaviour
{
	[SerializeField] Sprite m_BaseSprite;
	[SerializeField] Sprite m_FriedSprite;

	private void OnEnable()
	{
		GetComponent<SpriteRenderer>().sprite = m_BaseSprite;
	}

	public void Ded()
	{
		GetComponent<SpriteRenderer>().sprite = m_FriedSprite;
	}
}
