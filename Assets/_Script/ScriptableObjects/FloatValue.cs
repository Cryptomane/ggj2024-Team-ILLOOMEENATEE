using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatValue", menuName ="ILLOOMEENATEE/FloatValue", order = 100)]
public class FloatValue : ScriptableObject
{
	[SerializeField] private float m_Value;

	public float Value 
	{
		get 
		{ 
			return m_Value; 
		}
		set 
		{ 
			m_Value = value;  
		}
	}
}
