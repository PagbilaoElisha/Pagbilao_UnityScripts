using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool[] levelUnlocked = new bool[3]; // Three levels

    private void Awake()
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

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelUnlocked.Length)
            levelUnlocked[levelIndex] = true;
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex + 1 <= SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene("Level" + (levelIndex + 1));
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
