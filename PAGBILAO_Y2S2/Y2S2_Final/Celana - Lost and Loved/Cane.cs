using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cane : MonoBehaviour
{
    public float reachDistance = 5f;
    public float extendSpeed = 8f;
    public float retractSpeed = 4f;
    public float pullSpeed = 16f;
    public LayerMask obstacleLayers;
    public LayerMask interactableLayers;
    public LineRenderer lineRenderer;
    public Transform playerCenter;
    private bool isShooting = false;
    private GameObject objectToPull = null;
    private Player playerScript;

    void Start()
    {
        playerScript = GetComponent<Player>();
        if (playerScript == null)
        {
            Debug.LogError("PLAYER MUST HAVE THE CANE SCRIPT");
        }
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.enabled = false;
        lineRenderer.widthMultiplier = 0.35f;
        lineRenderer.numCapVertices = 4;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isShooting && playerScript != null && playerScript.hasCane && playerScript.caneActive)
        {
            StartCoroutine(ShootCane());
        }
    }

    private IEnumerator ShootCane()
    {
        isShooting = true;
        objectToPull = null;
        lineRenderer.enabled = true;

        Vector2 startPoint = playerCenter.position;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - startPoint).normalized;

        float visualLength = 0f;
        float hitDistance = reachDistance;
        Vector2 hitPoint = startPoint + direction * reachDistance;

        RaycastHit2D hitIN = Physics2D.Raycast(startPoint, direction, reachDistance, interactableLayers);
        if (hitIN.collider != null)
        {
            objectToPull = hitIN.collider.gameObject;
            hitDistance = hitIN.distance;
            hitPoint = hitIN.point;
        }
        else
        {
            RaycastHit2D hitOB = Physics2D.Raycast(startPoint, direction, reachDistance, obstacleLayers);
            if (hitOB.collider != null)
            {
                hitDistance = hitOB.distance;
                hitPoint = hitOB.point;
            }
        }

        while (visualLength < hitDistance)
        {
            float step = extendSpeed * Time.deltaTime;
            visualLength += step;
            if (visualLength > hitDistance) visualLength = hitDistance;

            Vector2 endPoint = startPoint + direction * visualLength;
            lineRenderer.SetPosition(0, playerCenter.position);
            lineRenderer.SetPosition(1, endPoint);
            yield return null;
        }

        Coroutine pullingRoutine = null;
        if (objectToPull != null)
        {
            pullingRoutine = StartCoroutine(PullWhileLineVisible(objectToPull));
        }

        Vector2 retractTip = lineRenderer.GetPosition(1);
        while (visualLength > 0f)
        {
            float step = retractSpeed * Time.deltaTime;
            visualLength -= step;
            if (visualLength < 0f) visualLength = 0f;

            Vector2 dirToPlayer = ((Vector2)playerCenter.position - retractTip).normalized;
            retractTip += dirToPlayer * step;
            lineRenderer.SetPosition(0, playerCenter.position);
            lineRenderer.SetPosition(1, retractTip);
            yield return null;
        }

        lineRenderer.enabled = false;
        if (pullingRoutine != null) StopCoroutine(pullingRoutine);
        isShooting = false;
    }

    private IEnumerator PullWhileLineVisible(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("No rigidbody on object");
            yield break;
        }

        while (lineRenderer.enabled && obj != null)
        {
            Vector2 direction = ((Vector2)playerCenter.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * pullSpeed * Time.deltaTime);
            yield return null;
        }
    }
}