using UnityEngine;

public class AntonioMover : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void RunTo(float x, float duration)
    {
        StartCoroutine(RunRoutine(x, duration));
    }

    public void Jump(float force)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
    }

    private System.Collections.IEnumerator RunRoutine(float xTarget, float duration)
    {
        Vector3 start = transform.position;
        Vector3 end = new Vector3(xTarget, start.y, start.z);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }
}
