using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePassingDetector : MonoBehaviour
{
    public ScoreManager scoreManager;
    private bool hasPassed = false;

    private void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPassed)
        {
            scoreManager.IncreasedScore();
            hasPassed = true;
        }
    }
    void OnBecamInvisible()
    {
        hasPassed = false;
    }
}

