using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetString("LastLevel", "Level1"); // Reset last level to Level1
        PlayerPrefs.Save();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadLast()
    {
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            string lastLevel = PlayerPrefs.GetString("LastLevel");
            Debug.Log($"Loading {lastLevel}");
            SceneManager.LoadScene(lastLevel);
        }
        else
        {
            Debug.Log("NO LASTLEVEL KEY ERROR");
            SceneManager.LoadScene("Level1"); // Fallback in case no level was saved
        }
    }
}
