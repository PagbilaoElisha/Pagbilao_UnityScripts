using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) // Enable IsTrigger of item object
    {
        if (other.CompareTag("Player")) // Give Player tag to player object to work
        {
            Destroy(gameObject);
            Debug.Log($"Got item {gameObject}");
        }
    }
}