using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;     // Main background music AudioSource
    public AudioSource sfxSource;       // For button select sound effects
    public AudioClip selectSFX;         // Default button select SFX
    public AudioSource loopingSource;   // Source for looping SFX
    public AudioClip loopingClip;
    public AudioSource additionalSFXSource; // Dedicated source for additional SFX

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures music persists between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Plays default button select SFX
    public void PlaySelectSFX()
    {
        sfxSource.PlayOneShot(selectSFX);
    }

    // Plays additional SFX from a separate AudioSource
    public void PlayAdditionalSFX(AudioClip additionalSFX)
    {
        additionalSFXSource.PlayOneShot(additionalSFX);
    }

    // Stops music in specific scenarios
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    // Resumes the paused music
    public void ResumeMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    // For playing a looping SFX in certain scenarios
    public void PlayLoopingSFX()
    {
        loopingSource.clip = loopingClip;
        loopingSource.loop = true;
        loopingSource.Play();
    }

    // Stops any looping SFX when leaving a scenario
    public void StopLoopingSFX()
    {
        loopingSource.loop = false;
        loopingSource.Stop();
    }
}
