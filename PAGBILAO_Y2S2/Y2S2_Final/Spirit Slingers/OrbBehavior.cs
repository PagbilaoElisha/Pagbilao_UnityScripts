using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbType { Fire, Earth, Air }

public class OrbBehavior : MonoBehaviour
{
    public OrbType type;
    public GameObject explosionEffectPrefab;
    public LayerMask destructibleLayer;

    private bool exploded = false;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (type == OrbType.Earth)
        {
            string tag = collision.collider.tag;

            if (tag == "Ground")
            {
                if (audioManager) audioManager.PlaySFX(AudioManager.Instance.quake);
                ExplodeAndDestroy(); // Only stop when hitting the ground
            }
            else
            {
                if (audioManager)
                {
                    PlayDestructionSound(tag);
                    audioManager.PlaySFX(AudioManager.Instance.quake);
                }
                Destroy(collision.gameObject); // Destroy anything else
            }

            return; // Prevent other logic from running
        }

        // Original Fire and Air behavior
        if (exploded) return;
        exploded = true;

        string colTag = collision.collider.tag;

        switch (type)
        {
            case OrbType.Fire:
                if (audioManager) audioManager.PlaySFX(AudioManager.Instance.explode);
                CreateExplosion(ExplosionType.Fire);
                break;
            case OrbType.Air:
                if (colTag == "Glass" || colTag == "Totem")
                {
                    Destroy(collision.gameObject);
                    if (audioManager) PlayDestructionSound(colTag);
                }
                if (audioManager) audioManager.PlaySFX(AudioManager.Instance.gust);
                CreateExplosion(ExplosionType.Air);
                break;
        }

        Destroy(gameObject);
    }

    void ExplodeAndDestroy()
    {
        CreateExplosion(ExplosionType.Earth);
        Destroy(gameObject);
    }


    void CreateExplosion(ExplosionType explosionType)
    {
        if (explosionEffectPrefab)
        {
            GameObject fx = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            ExplosionEffect effect = fx.GetComponent<ExplosionEffect>();
            if (effect != null)
            {
                effect.explosionType = explosionType;
            }
        }
    }

    void PlayDestructionSound(string tag)
    {
        switch (tag)
        {
            case "Glass": audioManager.PlaySFX(AudioManager.Instance.glass); break;
            case "Wood": audioManager.PlaySFX(AudioManager.Instance.wood); break;
            case "Rock": audioManager.PlaySFX(AudioManager.Instance.rock); break;
            case "Totem": audioManager.PlaySFX(AudioManager.Instance.glass); break;
        }
    }
}