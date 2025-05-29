using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExplosionType { Fire, Earth, Air }

public class ExplosionEffect : MonoBehaviour
{
    public ExplosionType explosionType;
    public float duration = 0.5f;
    public float radius = 2f;
    public LayerMask destructibleLayer;

    void Start()
    {
        switch (explosionType)
        {
            case ExplosionType.Fire:
                DamageNearby();
                break;
            case ExplosionType.Air:
                PushNearby();
                break;
            case ExplosionType.Earth:
                // Earth doesn't do a radius effect.
                break;
        }

        Destroy(gameObject, duration);
    }

    void DamageNearby()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, destructibleLayer);
        foreach (var col in hits)
        {
            Destroy(col.gameObject);
        }
    }

    void PushNearby()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, destructibleLayer);
        foreach (var col in hits)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = (col.transform.position - transform.position).normalized;
                rb.AddForce(dir * 250f); // Adjust force as needed
            }
        }
    }
}