using UnityEngine;

public class BallThrower : MonoBehaviour
{
    [SerializeField] private float maxDragDistance = 2.5f;
    [SerializeField] private float throwPower = 6f;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private Camera cam;
    private bool isDragging = false;

    public System.Action OnBallReleased;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        startPos = transform.position;

        rb.isKinematic = true; // so player can drag without physics breaking
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;

        Vector2 direction = (startPos - transform.position);
        rb.AddForce(direction * throwPower, ForceMode2D.Impulse);

        OnBallReleased?.Invoke();
    }

    void Update()
    {
        if (!isDragging) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Limit drag distance
        if (Vector2.Distance(mousePos, startPos) > maxDragDistance)
        {
            Vector2 dir = (mousePos - startPos).normalized;
            mousePos = startPos + (Vector3)dir * maxDragDistance;
        }

        transform.position = mousePos;
    }
}
