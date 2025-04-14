using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource sfxSource;
    public AudioClip buttonClick, weaponPickup, weaponFire, hitWall, hitEnemy, playerDeath;

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

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
