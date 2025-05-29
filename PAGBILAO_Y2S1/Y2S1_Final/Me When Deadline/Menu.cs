using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menuObject; // Assign the main menu panel
    public GameObject controlsObject; // Assign the How to Play panel
    public GameObject confirmObject; // Assign the saved data confirmation panel

    public void ButtonControls()
    {
        menuObject.SetActive(false); // Deactivate main menu
        controlsObject.SetActive(true); // Activate How to Play
    }

    public void ButtonSelection()
    {
        controlsObject.SetActive(false); // Deactivate How to Play
        menuObject.SetActive(true); // Activate main menu
    }

    public void ButtonStart()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            menuObject.SetActive(false);
            confirmObject.SetActive(true);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Intro");
        }
    }

    public void ButtonLoad()
    {
        if (PlayerPrefs.HasKey("PlayerX")) // Check if there is saved data
        {
            SaveSystem.loadSavedPositions = true;
            SceneManager.LoadScene("Game"); // Load the Game scene
        }
        else
        {
            Debug.Log("No saved position data found.");
        }
    }

    public void ButtonYes()
    {
        PlayerPrefs.DeleteAll(); // Delete saved position data
        Debug.Log("Saved position data deleted.");
        SceneManager.LoadScene("Intro"); // Load the Intro scene
    }

    public void ButtonNo()
    {
        confirmObject.SetActive(false); // Deactivate the confirmation panel
        menuObject.SetActive(true); // Activate the main menu panel
    }

    public void ButtonMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }
}

