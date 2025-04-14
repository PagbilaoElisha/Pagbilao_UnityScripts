using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Coin : MonoBehaviour
{
    public static int score = 0; // Coins collected by player
    public static int total = 0; // Coins in level; assigned dynamically in OnSceneLoaded
    public static TextMeshProUGUI scoreText; // Score display
    private bool collected = false; // Safeguard in case coin animation/s is/are added (I can't, it's a single image lol)

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Run OnSceneLoaded and dynamically set score and total
    }

    private void Start()
    {
        if (scoreText == null) // Assigning score text dynamically
        {
            GameObject textObject = GameObject.FindWithTag("ScoreText"); // Score text is tagged in inspector
            if (textObject != null)
                scoreText = textObject.GetComponent<TextMeshProUGUI>();
        }
        UpdateScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collected && collision.collider.CompareTag("Player"))
        {
            collected = true;
            score++;
            UpdateScore();
            gameObject.SetActive(false);
        }
    }

    private void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = $"{score}/{total} coins collected";
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        score = 0; // No coins collected at start of level
        total = GameObject.FindObjectsOfType<Coin>().Length; // Finds all objects with Coin.cs as component (coins in level)
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Prevent incorrect data (i.e. total increasing to more than what the level actually contains, score carrying over from previous plays)
    }
}
