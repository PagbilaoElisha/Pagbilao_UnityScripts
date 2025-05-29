using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioClip newMusicClip; // Assign via the Inspector if needed

    public void StopMusic()
    {
        if (BGMPersistence.instance != null)
        {
            BGMPersistence.instance.StopMusic();
        }
    }

    public void ChangeMusic()
    {
        if (newMusicClip == null)
        {
            Debug.LogWarning("No music clip assigned to 'newMusicClip'.");
            return;
        }

        if (BGMPersistence.instance != null)
        {
            AudioSource audioSource = BGMPersistence.instance.GetAudioSource();

            if (audioSource.clip != newMusicClip)
            {
                audioSource.clip = newMusicClip;
            }

            audioSource.Play(); // Ensure the music starts playing
        }
        else
        {
            Debug.LogError("BGMPersistence instance is not set.");
        }
    }
}
