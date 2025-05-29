using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 2f;

    private void Update()
    {
        if (GameOver.isGameOver) return;

        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < -12f)
        {
            Destroy(gameObject);
        }
    }
}
