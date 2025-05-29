using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndManager : MonoBehaviour
{
    public TextMeshProUGUI resultText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        resultText.text = PlayerPrefs.GetString("EndResult");
        string endSFX = PlayerPrefs.GetString("EndSound");
        if (endSFX == "y")
            SFXManager.Instance.PlaySFX(SFXManager.Instance.winner);
        else if (endSFX == "n")
            SFXManager.Instance.PlaySFX(SFXManager.Instance.loser);
    }

    public void Replay()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.button);
        SceneManager.LoadScene("Game");
    }
    public void Return()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.button);
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.button);
        Application.Quit();
    }
}
