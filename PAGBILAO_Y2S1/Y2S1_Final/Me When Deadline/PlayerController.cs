using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() // Use FixedUpdate for Rigidbody physics
    {
        float moveDirection = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow
        float rotation = Input.GetAxis("Horizontal");   // A/D or Left/Right Arrow

        // Calculate movement
        Vector3 forwardMovement = transform.forward * moveDirection * moveSpeed * Time.fixedDeltaTime;

        // Move the Rigidbody (updates physics correctly)
        rb.MovePosition(rb.position + forwardMovement);

        // Rotate the player
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation * rotationSpeed * Time.fixedDeltaTime, 0));
    }
}
