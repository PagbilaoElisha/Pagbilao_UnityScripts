using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject[] panels;
    public Button[] levelButtons;

    void Start()
    {
        ShowPanel("MainPanel");
        UpdateLevelButtons();
        if (!AudioManager.Instance.musicSource.isPlaying)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.music);
        }
    }

    public void ShowPanel(string panelName)
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(p.name == panelName);
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + (level));
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        UpdateLevelButtons();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < levelButtons.Length; i++)
            levelButtons[i].interactable = (i + 1) <= unlockedLevel;
    }
}