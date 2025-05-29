using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public Transform rayOrigin;
    public float rayDistance = 1.5f;
    public LayerMask interactableLayers;
    public LineRenderer line;
    public bool hasWeapon = false;
    public GameObject guideText;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateLine();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if ((move < 0 && facingRight) || (move > 0 && !facingRight))
        {
            facingRight = !facingRight;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Vector2 rayDirection = facingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.position, rayDirection, rayDistance, interactableLayers);
        UpdateLine();

        if (hit.collider && hit.collider.CompareTag("Guide"))
        {
            guideText.SetActive(true);
        }
        else
        {
            guideText.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (hasWeapon)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.weaponFire);
            }

            if (hit.collider)
            {
                string tag = hit.collider.tag;
                switch (tag)
                {
                    case "Weapon":
                        hasWeapon = true;
                        Destroy(hit.collider.gameObject);
                        UpdateLine(); // Making sure line turns red
                        SoundManager.Instance.PlaySound(SoundManager.Instance.weaponPickup);
                        break;
                    case "Enemy":
                        if (hasWeapon)
                        {
                            Destroy(hit.collider.gameObject);
                            SoundManager.Instance.PlaySound(SoundManager.Instance.hitEnemy);
                        }
                        break;
                    case "WallB":
                        if (hasWeapon)
                        {
                            Destroy(hit.collider.gameObject);
                            SoundManager.Instance.PlaySound(SoundManager.Instance.hitWall);
                        }
                        break;
                }
            }
        }
    }

    void UpdateLine()
    {
        Vector2 start = rayOrigin.position;
        Vector2 direction = facingRight ? Vector2.right : Vector2.left;
        Vector2 end = start + direction * rayDistance;

        line.SetPosition(0, start);
        line.SetPosition(1, end);
        line.startColor = line.endColor = hasWeapon ? Color.red : Color.white;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
