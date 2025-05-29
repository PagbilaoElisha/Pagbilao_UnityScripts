using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Only needed if Quit is implemented via SceneManager.
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menu; // Reference to the menu object.
    public GameObject[] levels; // Array of all level objects.
    public Button[] levelButtons; // Buttons corresponding to levels.
    private int unlockedLevels = 1; // Tracks the number of unlocked levels.

    void Start()
    {
        UpdateMenu();
    }

    public void SelectLevel(int levelIndex)
    {
        menu.SetActive(false);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i == levelIndex); // Activate the selected level only.
        }
    }

    public void UnlockNextLevel()
    {
        if (unlockedLevels < levelButtons.Length)
        {
            levelButtons[unlockedLevels].interactable = true;
            unlockedLevels++;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateMenu()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = i < unlockedLevels;
        }
    }
}
