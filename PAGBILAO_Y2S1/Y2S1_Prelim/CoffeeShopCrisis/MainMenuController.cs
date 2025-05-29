using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject optionsPanel;  // Reference to Options GameObject
    public GameObject creditsPanel;  // Reference to Credits GameObject

    void Start()
    {
        // Options should be active and Credits inactive at the start.
        optionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    // Start Button: Loads the VisualNovel scene
    public void StartGame()
    {
        SceneManager.LoadScene("VisualNovel");
    }

    // Credits Button: Activates the Credits panel and hides Options
    public void OpenCredits()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    // Back Button in Credits: Hides the Credits panel and shows Options
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // Exit Button: Quits the application
    public void ExitGame()
    {
        Application.Quit();
    }
}
