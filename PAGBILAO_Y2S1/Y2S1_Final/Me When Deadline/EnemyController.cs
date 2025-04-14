using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Set the destination to the player's position
        navMeshAgent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("LOSS");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}
