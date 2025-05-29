using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelStartManager : MonoBehaviour
{
    public GameObject cutscenePanel;
    public VideoPlayer cutsceneVideo;

    void Start()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;

        if (levelIndex == 1 && PlayerPrefs.GetInt("PlayedLevel1", 0) == 0)
        {
            StartCoroutine(PlayCutscene());
        }
        else
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.intro);
            cutscenePanel.SetActive(false);
        }
    }

    IEnumerator PlayCutscene()
    {
        cutscenePanel.SetActive(true);
        cutsceneVideo.Play();
        yield return new WaitForSeconds((float)cutsceneVideo.length);
        cutscenePanel.SetActive(false);
        PlayerPrefs.SetInt("PlayedLevel1", 1);
    }
}