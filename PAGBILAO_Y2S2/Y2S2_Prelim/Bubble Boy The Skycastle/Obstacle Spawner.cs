using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn Attributes")]
    public GameObject rock;

    //[Header("Spawn Settings")]
    private float spawnRate = 500f;
    private float timer = 1f;
    //public float heightOffset = 5;
    // Start is called before the first frame update
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver.isGameOver) return;

        timer += 1f;
        //Debug.Log($"timer{timer}");

        if (timer >= spawnRate)
        {
            spawnPipe();
            //Debug.Log($"if timer{timer}");
            timer = 0;
        }
    } 

    void spawnPipe()
    {
        Instantiate(rock, new Vector3(Random.Range(-12, 12), transform.position.y, 1), transform.rotation);
    }
}
