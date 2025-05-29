using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void Start()
    {
        // Save the current level when the scene loads
        string currentLevel = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastLevel", currentLevel);
        PlayerPrefs.Save();
        Debug.Log($"Saved current level on start: {currentLevel}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("Build Index exceeded. Returning to Menu..."); // Menu's Build Index is 0
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Debug.Log("Loading next level...");
            SceneManager.LoadScene(nextIndex);
        }
    }
}
