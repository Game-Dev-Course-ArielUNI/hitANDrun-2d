using UnityEngine;

public class AntonioJump : MonoBehaviour
{
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.phase != GamePhase.Run) return;

        // Spacebar makes Antonio jump
        if (Input.GetKeyDown(KeyCode.Space))
            {
            Jump();
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
    }

    // Detect if Antonio lands back on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
