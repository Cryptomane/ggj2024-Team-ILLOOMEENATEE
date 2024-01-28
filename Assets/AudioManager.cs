using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource m_commandSound;
    [SerializeField] private AudioSource m_minigameSound;
    [SerializeField] private AudioSource m_resultsSound;

    public void playMinigame() 
    {
        m_minigameSound.Play();
    }
    
    public void stopMinigame() 
    {
        m_minigameSound.Stop();
    }

    public void playResults() 
    {
        m_resultsSound.Play();
    }

    public void stopResults()
    {
        m_resultsSound.Stop();
    }

    public void playCommand()
    {
        m_commandSound.Play();
    }

    public void stopCommand()
    {
        m_commandSound.Stop();
    }

}
