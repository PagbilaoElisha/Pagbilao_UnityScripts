using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip music;
    public AudioClip intro;

    [Header("SFX - Events")]
    public AudioClip win;
    public AudioClip loss;

    [Header("SFX - Orbs")]
    public AudioClip fire;
    public AudioClip earth;
    public AudioClip air;

    public AudioClip explode;
    public AudioClip quake;
    public AudioClip gust;

    [Header("SFX - Materials")]
    public AudioClip glass;
    public AudioClip wood;
    public AudioClip rock;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic() => musicSource.Stop();

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }
}
