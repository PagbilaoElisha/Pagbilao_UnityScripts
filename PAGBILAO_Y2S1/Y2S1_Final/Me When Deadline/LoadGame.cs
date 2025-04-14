using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LoadGame : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        float x = Convert.ToSingle(videoPlayer.length);
        Invoke("EndIntro", x);
    }

    void EndIntro()
    {
        SceneManager.LoadScene("Game");
    }
}
