using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject rulePanel;
    public GameObject creditsPanel;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
    public void ToInstructions()
    {
        mainPanel.SetActive(false);
        rulePanel.SetActive(true);
    }
    public void FromInstructions()
    {
        rulePanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void ToCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    public void FromCredits()
    {
        creditsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
