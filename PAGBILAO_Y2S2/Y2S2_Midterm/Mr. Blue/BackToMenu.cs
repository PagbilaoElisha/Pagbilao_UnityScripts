using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script is for the Back to Menu button found in a level, as using GameManager will require Inspector initialization
public class BackToMenu : MonoBehaviour
{
    public void GoToMenu()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClick);
        GameManager.Instance.BackToMenu();
    }
}
