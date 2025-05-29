using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Transform player;
    private bool attacking = false;
    public LayerMask wallLayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!attacking) // Idle Phase
        {
            Vector2 dir = player.position - transform.position;
           if (Mathf.Abs(dir.y) <= 1f) // Checks if player is in the same horizontal area as itself
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, dir.magnitude, wallLayer);
                if (!hit.collider) // Checks if there is a wall between the player and itself
                {
                    attacking = true;
                }
            }
        }
        else // Attacking Phase
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // Chase player
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.playerDeath);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Instant reload level
        }
    }
}
