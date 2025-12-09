using UnityEngine;

public class PileBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            // Add some physics explosion effect
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;

            rb.AddForce(new Vector2(
                Random.Range(-2f, 2f),
                Random.Range(1f, 4f)
            ), ForceMode2D.Impulse);

            GameManager.Instance.OnPileHit();
        }
    }
}
