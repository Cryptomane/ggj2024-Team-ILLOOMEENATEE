using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public enum Key
	{
		KEY_A,
		KEY_B,
		KEY_C,
		LEFT,
		RIGHT,
		ANY
	}

	private static bool m_APressed;
	private static bool m_BPressed;
	private static bool m_CPressed;
	private static bool m_LeftPressed;
	private static bool m_RightPressed;

    private static event Action m_OnRightArrowHit;

    public static event Action OnRightArrowHit
    {
        add 
        { 
            m_OnRightArrowHit -= value;
            m_OnRightArrowHit += value;

        }
        remove 
        { 
            m_OnRightArrowHit -= value;
        }
    }

    private static event Action m_OnLeftArrowHit;

    public static event Action OnLeftArrowHit
    {
        add
        {
            m_OnLeftArrowHit -= value;
            m_OnLeftArrowHit += value;

        }
        remove
        {
            m_OnLeftArrowHit -= value;
        }
    }

    private static event Action m_OnAHit;

    public static event Action OnAHit
    {
        add
        {
            m_OnAHit -= value;
            m_OnAHit += value;

        }
        remove
        {
            m_OnAHit -= value;
        }
    }

    private static event Action m_OnBHit;

    public event Action OnBHit
    {
        add
        {
            m_OnBHit -= value;
            m_OnBHit += value;

        }
        remove
        {
            m_OnBHit -= value;
        }
    }

    private static event Action m_OnCHit;

    public static event Action OnCHit
    {
        add
        {
            m_OnCHit -= value;
            m_OnCHit += value;

        }
        remove
        {
            m_OnCHit -= value;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0)
        {
			m_RightPressed = true;
            m_OnRightArrowHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0)
        {
			m_LeftPressed = true;
            m_OnLeftArrowHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
			m_APressed = true;
            m_OnAHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
			m_BPressed = true;
            m_OnBHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
			m_CPressed = true;
            m_OnCHit?.Invoke();
        }
    }

	public static bool GetKeyDown(Key key)
	{
		switch (key)
		{
			case Key.KEY_A:
				return m_APressed;
			case Key.KEY_B:
				return m_BPressed;
			case Key.KEY_C:
				return m_CPressed;
			case Key.LEFT:
				return m_LeftPressed;
			case Key.RIGHT:
				return m_RightPressed;
			default:
				break;
		}
		return m_APressed || m_BPressed || m_CPressed;
	}

	private void LateUpdate()
	{
		m_APressed = false;
		m_BPressed = false;
		m_CPressed = false;
		m_LeftPressed = false;
		m_RightPressed = false;
	}
}
