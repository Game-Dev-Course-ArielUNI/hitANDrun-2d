using UnityEngine;

public class MarcoChase : MonoBehaviour
{
    public float chaseSpeed = 3f;
    public Transform antonio;   // drag antonio here in Inspector

    private bool isChasing = false;

    public void BeginChase()
    {
        isChasing = true;
    }

    void Update()
    {
        if (!isChasing) return;
        if (GameManager.Instance.phase != GamePhase.Run) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            antonio.position,
            chaseSpeed * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Antonio"))
        {
            GameManager.Instance.OnMarcoCatch();
        }
    }
}
