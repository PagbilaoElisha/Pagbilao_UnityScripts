using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject viewerDiscretionMessage; // The viewer discretion message panel
    public GameObject menuObject;          // The game object to activate next
    public GameObject creditsObject;        // The currently active game object
    public GameObject thanksObject;

    private static bool isViewerDiscretionShown = false; // Tracks if the message has been shown in this session

    private void Start()
    {
        // Show viewer discretion message only once per application session
        if (!isViewerDiscretionShown)
        {
            viewerDiscretionMessage.SetActive(true);
            isViewerDiscretionShown = true;
        }
        else
        {
            viewerDiscretionMessage.SetActive(false);
        }
    }

    public void GameButtonStart()
    {
        SceneManager.LoadScene("StoryGameplay");
    }

    public void GameButtonRestart()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameButtonQuit()
    {
        Application.Quit();
    }

    // Closes the viewer discretion message
    public void CloseViewerDiscretion()
    {
        viewerDiscretionMessage.SetActive(false);
    }

    // Activates the next game object and deactivates the active game object
    public void GoToCredits()
    {
        if (menuObject != null)
        {
            menuObject.SetActive(false);
        }
        if (creditsObject != null)
        {
            creditsObject.SetActive(true);
        }
    }

    public void SpecialThanks()
    {
        if (creditsObject != null)
        {
            creditsObject.SetActive(false);
        }
        if (thanksObject != null)
        {
            thanksObject.SetActive(true);
        }
    }
}
