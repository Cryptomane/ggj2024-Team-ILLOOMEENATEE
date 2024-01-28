using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_introSound;
    [SerializeField] private AudioSource m_commandSound;
    [SerializeField] private AudioSource m_minigameSound;
    [SerializeField] private AudioSource m_resultsSound;

    public void playMinigame() 
    {
		if (m_minigameSound == null)
			return;
        m_minigameSound.Play();
    }
    
    public void stopMinigame() 
    {
		if (m_minigameSound == null)
			return;
		m_minigameSound.Stop();
    }

    public void playResults() 
    {
		if (m_resultsSound == null)
			return;
		m_resultsSound.Play();
    }

    public void stopResults()
    {
		if (m_resultsSound == null)
			return;
		m_resultsSound.Stop();
    }

    public void playCommand()
    {
		if (m_commandSound == null)
			return;
		m_commandSound.Play();
    }

    public void stopCommand()
    {
		if (m_commandSound == null)
			return;
		m_commandSound.Stop();
    }

    public void playIntro()
    {
        if (m_introSound == null)
            return;
        m_introSound.Play();
    }

    public void stopIntro()
    {
        if (m_introSound == null)
            return;
        m_introSound.Stop();
    }

}
