using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public float attackRange = 4f;
    public float damage = 1f;
    public float pushback = 5f;
    public TextMeshProUGUI attackPrompt;
    private Camera cam;

    void Start()
    {
        GameManager.Instance.enemyCount = 10;
        cam = Camera.main;
        if (attackPrompt != null )
            attackPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!GameManager.Instance.hasWeapon) return; // Do nothing if player has no weapon
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                if (attackPrompt != null)
                {
                    attackPrompt.text = "Attack";
                    attackPrompt.gameObject.SetActive(true);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    float distance = Vector3.Distance(transform.position, hit.collider.transform.position);
                    if (distance <= attackRange)
                    {
                        EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                        if (enemy != null)
                        {
                            Vector3 hitDirection = (hit.collider.transform.position - transform.position).normalized;
                            SFXManager.Instance.PlaySFX(SFXManager.Instance.weapon);
                            enemy.TakeDamage(damage, hitDirection, pushback);
                        }
                    }
                }
                return; // Avoid hiding prompt if enemy is still in range
            }
        }
        if (attackPrompt != null)
            attackPrompt.gameObject.SetActive(false);
    }
}
