using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float movementSpeed = 10f;
    private Rigidbody2D rb;

    // Event untuk memberitahu GameController saat player mati
    public event Action playerDied;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Menggunakan Input.GetKey agar pasti jalan di Unity 6
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;
        else
            horizontalInput = 0f;
    }

    void FixedUpdate()
    {
        // Unity 6 menggunakan linearVelocity
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            Destroy(gameObject);
            playerDied?.Invoke(); // Kirim sinyal player mati
        }
    }
}