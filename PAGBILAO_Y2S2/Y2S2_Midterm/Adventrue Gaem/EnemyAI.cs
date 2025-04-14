using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float idleRadius = 5f;
    public float detectRange = 10f;
    public float moveSpeed = 3f;
    public float killDistance = 0.2f;
    public Transform player;
    public Color idleColor = Color.yellow;
    public Color attackColor = Color.red;
    private Vector3 origin;
    private Vector3 idleTarget;
    private Renderer rend;
    private UnityEngine.AI.NavMeshAgent agent;
    private enum State { Idle, Attack, Return }
    private State currentState = State.Idle;

    void Start()
    {
        origin = transform.position;
        idleTarget = origin;
        rend = GetComponent<Renderer>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.areaMask = 1 << UnityEngine.AI.NavMesh.GetAreaFromName("EnemyArea");
        rend.material.color = idleColor;
        InvokeRepeating(nameof(SetNewIdleTarget), 2f, 5f);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectRange)
        {
            currentState = State.Attack;
            rend.material.color = attackColor;
            agent.SetDestination(player.position);
        }
        else if (Vector3.Distance(transform.position, idleTarget) < 0.5f)
        {
            currentState = State.Idle;
        }
        else
        {
            currentState = State.Return;
            rend.material.color = idleColor;
            agent.SetDestination(idleTarget);
        }
        if (distanceToPlayer < killDistance)
        {
            GameManager.Instance.EndGame(false);
        }
    }

    void SetNewIdleTarget()
    {
        if (currentState == State.Attack) return;
        Vector3 randomPoint = origin + Random.insideUnitSphere * idleRadius;
        randomPoint.y = transform.position.y;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, idleRadius, UnityEngine.AI.NavMesh.AllAreas))
        {
            idleTarget = hit.position;
        }
    }
}
