using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Slingshot : MonoBehaviour
{
    public GameObject[] orbPrefabs;
    public Transform slingOrigin;
    public Transform launchPoint;
    public float launchForceMultiplier = 10f;
    public float maxDragDistance = 3f;

    private LineRenderer slingLine;
    private GameObject loadedOrb;
    private Vector3 dragStartPos;
    private bool isDragging;
    private OrbType selectedOrbType;
    private LevelManager levelManager;
    private AudioManager audioManager;

    void Start()
    {
        slingLine = GetComponent<LineRenderer>();
        levelManager = FindObjectOfType<LevelManager>();
        launchPoint.localPosition = Vector3.zero;
        slingLine.positionCount = 2;
        slingLine.SetPosition(0, slingOrigin.position);
        slingLine.SetPosition(1, slingOrigin.position);
        UpdateSlingLine();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (loadedOrb)
            HandleInput();

        UpdateSlingLine();
    }

    public void LoadOrb(int typeIndex)
    {
        selectedOrbType = (OrbType)typeIndex;

        if (!levelManager.HasOrb(selectedOrbType))
        {
            Debug.Log("No more orbs of this type");
            return;
        }

        if (loadedOrb != null)
            Destroy(loadedOrb);

        loadedOrb = Instantiate(orbPrefabs[typeIndex], launchPoint.position, Quaternion.identity);

        // Freeze physics before launch
        Rigidbody2D rb = loadedOrb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = GetMouseWorldPos();
            isDragging = true;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 dragPos = GetMouseWorldPos();
            Vector3 dragVector = Vector3.ClampMagnitude(dragPos - slingOrigin.position, maxDragDistance);
            launchPoint.position = slingOrigin.position + dragVector;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            LaunchOrb();
            isDragging = false;
        }
    }

    public void LaunchOrb()
    {
        if (loadedOrb == null) return;

        Rigidbody2D rb = loadedOrb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            OrbBehavior orb = loadedOrb.GetComponent<OrbBehavior>();
            if (audioManager && orb != null)
            {
                switch (orb.type)
                {
                    case OrbType.Fire:
                        audioManager.PlaySFX(AudioManager.Instance.fire);
                        break;
                    case OrbType.Earth:
                        audioManager.PlaySFX(AudioManager.Instance.earth);
                        break;
                    case OrbType.Air:
                        audioManager.PlaySFX(AudioManager.Instance.air);
                        break;
                }
            }
            // Unfreeze physics
            rb.isKinematic = false;

            // Calculate pull vector (from launch point back to sling origin)
            Vector2 pullVector = slingOrigin.position - launchPoint.position;
            float launchPower = 10f; // tweak this value to control force
            rb.AddForce(pullVector * launchPower, ForceMode2D.Impulse);
        }

        levelManager.ConsumeOrb(selectedOrbType);

        CameraFollow.Instance.Follow(loadedOrb.transform);

        // Reset launchPoint for next orb
        launchPoint.localPosition = Vector3.zero;

        loadedOrb = null;
    }


    private void UpdateSlingLine()
    {
        if (!slingLine || !slingOrigin || !launchPoint) return;
        slingLine.positionCount = 2;
        slingLine.SetPosition(0, slingOrigin.position);
        slingLine.SetPosition(1, launchPoint.position);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}