using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
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
            m_OnRightArrowHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < 0)
        {
            m_OnLeftArrowHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            m_OnAHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            m_OnBHit?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            m_OnCHit?.Invoke();
        }
    }
}
