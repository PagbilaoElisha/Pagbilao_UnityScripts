using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject titleGroup;
    public GameObject instructionsGroup;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.button);
        SceneManager.LoadScene("Game");
    }

    public void ShowInstructions()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.button);
        titleGroup.SetActive(false);
        instructionsGroup.SetActive(true);
    }

    public void HideInstructions()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.button);
        instructionsGroup.SetActive(false);
        titleGroup.SetActive(true);
    }

    public void QuitGame()
    {
        SFXManager.Instance.PlaySFX(SFXManager.Instance.button);
        Application.Quit();
    }
}