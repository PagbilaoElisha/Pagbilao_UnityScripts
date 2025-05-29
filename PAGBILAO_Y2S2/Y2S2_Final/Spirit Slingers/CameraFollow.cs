using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    [Header("Tracking Settings")]
    public float followSpeed = 0.1f;
    public float returnDelay = 2f;
    public Transform defaultPosition;

    [Header("Bounds")]
    public Vector2 minBounds;
    public Vector2 maxBounds;
    public float camHalfWidth = 8f;
    public float camHalfHeight = 5f;

    private Transform target;

    void Awake()
    {
        Instance = this;
        target = defaultPosition;
    }

    void Update()
    {
        if (target == null) return;

        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, -10f);
        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(desiredPos.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth),
            Mathf.Clamp(desiredPos.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight),
            desiredPos.z
        );

        transform.position = Vector3.Lerp(transform.position, clampedPos, followSpeed);
    }

    public void Follow(Transform newTarget)
    {
        target = newTarget != null ? newTarget : defaultPosition;
        CancelInvoke(nameof(ReturnToStart));
        if (newTarget != null)
            Invoke(nameof(ReturnToStart), returnDelay);
    }

    void ReturnToStart()
    {
        target = defaultPosition;
    }
}