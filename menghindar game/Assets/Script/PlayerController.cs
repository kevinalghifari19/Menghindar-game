using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float movementSpeed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // Untuk flip sprite

    // Event untuk memberitahu GameController saat player mati
    public event Action playerDied;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Ambil SpriteRenderer
    }

    void Update()
    {
        // Input gerakan
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;
        else
            horizontalInput = 0f;

        // Flip sprite sesuai arah
        FlipSprite();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);
    }

    // Fungsi untuk membalik sprite
    void FlipSprite()
    {
        // Jika bergerak ke kiri dan sprite belum menghadap kiri
        if (horizontalInput < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        // Jika bergerak ke kanan dan sprite belum menghadap kanan
        else if (horizontalInput > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Destroy(gameObject);
            playerDied?.Invoke();
        }
    }
}