using UnityEngine;



public class AntonioController : MonoBehaviour
{
    public float runSpeed = 4f;
    private bool canRun = false;

    public void BeginRun()
    {
        canRun = true;
    }

    void Update()
    {
        if (!canRun) return;
        if (GameManager.Instance.phase != GamePhase.Run) return;

        float input = Input.GetAxis("Horizontal");   // player uses arrows / A,D
        transform.position += new Vector3(input * runSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.OnAntonioReachFinish();
        }
        if (other.CompareTag("Marco"))
        {
            GameManager.Instance.OnMarcoCatch();
        }
    }
}

//public class AntonioController : MonoBehaviour
//{
//    public float runSpeed = 4f;
//    private bool canRun = false;

//    public void BeginRun()
//    {
//        canRun = true;
//    }

//    void Update()
//    {
//        if (!canRun) return;
//        if (GameManager.Instance.phase != GamePhase.Run) return;

//        // Player controls Antonio with keyboard arrows
//        float input = Input.GetAxis("Horizontal");
//        transform.position += new Vector3(input * runSpeed * Time.deltaTime, 0, 0);
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Finish"))
//        {
//            GameManager.Instance.OnAntonioReachFinish();
//        }
//        if (other.CompareTag("Marco"))
//        {
//            GameManager.Instance.OnMarcoCatch();
//        }
//    }
//}
