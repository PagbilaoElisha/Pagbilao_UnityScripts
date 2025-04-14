using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the AudioManager across scene reloads
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate AudioManagers
        }
    }

    private void Update()
    {
        // Destroy AudioManager if the active scene is not the Game scene
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Game")
        {
            Destroy(gameObject);
        }
    }
}
