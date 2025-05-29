using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPersistence : MonoBehaviour
{
    public static BGMPersistence instance; // Singleton instance
    private AudioSource audioSource;

    private void Awake()
    {
        // Ensure only one instance of the music manager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Set up the AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true; // Enable looping
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if (audioSource.clip != musicClip)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public bool IsMusicPlaying()
    {
        return audioSource.isPlaying;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource; // Returns the private audioSource reference
    }
}
