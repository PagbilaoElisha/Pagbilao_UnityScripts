using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Vector3 hitDirection, float pushForce)
    {
        // Damage
        currentHealth -= amount;

        // Pushback
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(hitDirection * pushForce, ForceMode.Impulse);
        }

        // Death
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.EnemyDefeated();
        Destroy(gameObject);
    }
}
