using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button startButton, loadButton, quitButton;
    public GameObject loadPanel;
    public Button[] levelButtons;

    void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
            GameManager.Instance.UnlockLevel(0);
            GameManager.Instance.LoadLevel(0);
        });

        loadButton.interactable = GameManager.Instance.levelUnlocked[0];
        loadButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
            loadPanel.SetActive(true);
        });

        for (int i = 0; i<levelButtons.Length; i++)
        {
            int index = i;
            levelButtons[i].interactable = GameManager.Instance.levelUnlocked[i];
            levelButtons[i].onClick.AddListener(() => 
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
                GameManager.Instance.LoadLevel(index);
            });
        }

        quitButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
            GameManager.Instance.QuitGame();
        });
    }
}
