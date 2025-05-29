using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovements : MonoBehaviour
{
    public GameObject obstaclePairPrefab;
    public GameObject BubbleCharac;

    public float initialSpawnInterval = 2f;
    public float minX = -2.5f;
    public float maxX = 2.5f;
    public float spawnY = 110f;
    public float initialGapSize = 3f;
    public float initialObstacleSpeed = 2f;

    private float spawnInterval;
    private float obstacleSpeed;
    private float gapSize;
    private float difficultyIncreaseRate = 0.1f;

    private ScoreManager scoreManager;
    private int lastScore = 0;
    
    void Start()
    {
        scoreManager = FindAnyObjectByType<ScoreManager>();
        spawnInterval = initialSpawnInterval;
        obstacleSpeed = initialObstacleSpeed;
        gapSize = initialGapSize;

        Debug.Log("Obstacle Spawning Started");
        InvokeRepeating(nameof(SpawnObstaclePair), 1f, spawnInterval);    
    }

    void Update()
    {
        if (GameOver.isGameOver) return;

        int currentScore = scoreManager.GetScore();

        if (currentScore > lastScore)
        {
            AdjustDifficulty(currentScore);
            lastScore = currentScore;
        }
    }

    void AdjustDifficulty(int score)
    {
        spawnInterval = Mathf.Max(0.5f, initialSpawnInterval - (score * difficultyIncreaseRate));
        obstacleSpeed = Mathf.Min(5f, initialObstacleSpeed + (score * difficultyIncreaseRate));
        gapSize = Mathf.Max(1.5f, initialGapSize - (score * 0.05f));

        Debug.Log("Obstacle Spawning Cancelled");
        CancelInvoke(nameof(SpawnObstaclePair));
        Debug.Log("Obstacle Spawning Started");
        InvokeRepeating(nameof(SpawnObstaclePair), 1f, spawnInterval);
        Debug.Log("Difficulty adjusted - Interval: " + spawnInterval + "Gap Size:" + "Obstacle Speed: " + obstacleSpeed);
    }

    void SpawnObstaclePair()
    {
        if (GameOver.isGameOver) return;

        Debug.Log("Obstacle Spawned");
        float randomX = Random.Range(minX, maxX);
        float randomYOffset = Random.Range(-gapSize, gapSize);
        Vector2 spawnPosition = new Vector2(randomX, spawnY);

        GameObject newObstaclePair = Instantiate(obstaclePairPrefab, spawnPosition, Quaternion.identity);

        //float randomYOffset = Random.Range(-1.5f, 1.5f);
        newObstaclePair.transform.position = new Vector2(randomX, spawnY + randomYOffset);
    }
}
