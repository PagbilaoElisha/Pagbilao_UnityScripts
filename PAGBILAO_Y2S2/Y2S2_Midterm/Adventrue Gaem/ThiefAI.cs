using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefAI : MonoBehaviour
{
    public float runRadius = 10f;
    public float repositionInterval = 3f;
    public Transform player;
    private UnityEngine.AI.NavMeshAgent agent;
    private float nextMoveTime;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.areaMask = 1 << UnityEngine.AI.NavMesh.GetAreaFromName("ThiefArea");
        nextMoveTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= nextMoveTime)
        {
            AvoidPlayer();
            nextMoveTime = Time.time + repositionInterval;
        }
    }

    void AvoidPlayer()
    {
        Vector3 directionAway = (transform.position - player.position).normalized;
        Vector3 target = transform.position + directionAway * runRadius;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(target, out hit, runRadius, UnityEngine.AI.NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
