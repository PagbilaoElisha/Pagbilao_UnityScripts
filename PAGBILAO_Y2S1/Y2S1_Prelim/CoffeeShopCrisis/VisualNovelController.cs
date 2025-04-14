using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualNovelController : MonoBehaviour
{
    public GameObject activeScene;
    public GameObject nextScene;
    private static int points;           // Points tracker
    public int addPoints;
    public GameObject ending1;
    public GameObject ending2;
    public GameObject ending3;
    public AudioClip additionalSFX;

    void Start()
    {
        // Ensure only the first scenario is active, rest are inactive
        activeScene.SetActive(true);
    }

    // Progress Button: Moves to the next assigned scenario
    public void NextScenario()
    {
        activeScene.SetActive(false);
        nextScene.SetActive(true);
        points += addPoints;
        Debug.Log("Current points: " + points);
    }

    // Check Ending: Activates an ending based on points
    public void CheckEnding()
    {
        activeScene.SetActive(false);
        if (points == 0)
        {
            ending1.SetActive(true);
        }
        else if (points >= 1 && points <= 3)
        {
            ending2.SetActive(true);
        }
        else
        {
            ending3.SetActive(true);
        }
    }

    // Return to MainMenu
    public void ReturnToMainMenu()
    {
        points = 0;
        activeScene.SetActive(false);
        nextScene.SetActive(true);
        activeScene = nextScene;
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectSFX()
    {
        AudioManager.instance.PlaySelectSFX();
    }

    public void OtherSFX()
    {
        // Call the method to play the additional SFX
        AudioManager.instance.PlayAdditionalSFX(additionalSFX);
    }

    public void MusicStop()
    {
        AudioManager.instance.StopMusic();
    }

    public void MusicResume()
    {
        AudioManager.instance.ResumeMusic();
    }

    public void LoopStart()
    {
        AudioManager.instance.PlayLoopingSFX();
    }
    public void LoopStop()
    {
        AudioManager.instance.StopLoopingSFX();
    }
}
