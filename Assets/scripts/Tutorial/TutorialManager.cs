using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;


public class TutorialManager : MonoBehaviour
{
    [Header("UI")]
    public RectTransform tutorialBox;
    public TMP_Text tutorialText;
    public RectTransform arrow;
 

    [Header("World Targets")]
    public Transform antonio;
    public Transform pile;
    public Transform finishLine;
    public Transform cone;
    public Transform marco;
    public Transform ball;        // the ball in Antonio's hand

    [Header("Movement settings")]
    public float ballThrowTime = 0.6f;
    public float antonioRunTime = 2f;
    public float marcoRunTime = 2.2f;

    private int step = 0;
    private bool isAnimating = false;

    private Transform currentArrowTarget;

    private Vector3 ballStartPos;
    private Vector3 antonioStartPos;
    private Vector3 marcoStartPos;

    private void Start()
    {
        // Cache start positions
        if (ball != null) ballStartPos = ball.position;
        if (antonio != null) antonioStartPos = antonio.position;
        if (marco != null) marcoStartPos = marco.position;

        // Make sure UI is visible
        if (tutorialBox != null) tutorialBox.gameObject.SetActive(true);
        if (arrow != null) arrow.gameObject.SetActive(true);

        step = 0;
        ShowCurrentStep();
    }

    private void Update()
    {
        if (isAnimating) return;

        // New Input System mouse click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            step++;
            ShowCurrentStep();
        }

        // Keep arrow sitting on the current target
        if (currentArrowTarget != null && arrow != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(currentArrowTarget.position);
            arrow.position = screenPos;
        }
    }

    private void ShowCurrentStep()
    {
        switch (step)
        {
            case 0:
                // Show Antonio + ball
                tutorialText.text = "This is Antonio. He is holding the ball.";
                currentArrowTarget = antonio;
                break;

            case 1:
                // Show the pile
                tutorialText.text = "This is the pile you are trying to hit.";
                currentArrowTarget = pile;
                break;

            case 2:
                // Explain throw + auto throw animation
                tutorialText.text = "Antonio throws the ball at the pile.";
                currentArrowTarget = pile;
                StartCoroutine(AutoThrowBall());
                break;

            case 3:
                // Explain running to finish line
                tutorialText.text = "After hitting the pile, Antonio must run to the finish line.";
                currentArrowTarget = finishLine;
                StartCoroutine(AutoRunAntonio());
                break;

            case 4:
                // Explain jumping over cones
                tutorialText.text = "Antonio must jump over the cones on his way.";
                currentArrowTarget = cone;
                //Invoke(nameof(AntonioJump), 1f);
                break;

            case 5:
                // Explain Marco chases
                tutorialText.text = "Marco grabs the ball and chases Antonio!,he must hit him before he reachs the finish line";
                currentArrowTarget = marco;
                StartCoroutine(AutoRunMarco());
                break;

            default:
                tutorialText.text = "Now it's your turn to play!";
                currentArrowTarget = null;
                if (arrow != null) arrow.gameObject.SetActive(false);
                break;

            case 6:   // or whatever your final step number is
                SceneLoader loader = FindObjectOfType<SceneLoader>();
                loader.LoadGameScene();
                break;
        }
    }

    private System.Collections.IEnumerator AutoThrowBall()
    {
        if (ball == null || pile == null) yield break;

        isAnimating = true;

        Vector3 start = ball.position;
        Vector3 end = pile.position + new Vector3(0f, 0.8f, 0f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / ballThrowTime;
            ball.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        // Keep ball at pile
        ball.position = end;

        isAnimating = false;

        // Automatically move to next step
        step++;
        ShowCurrentStep();
    }

    private void AntonioJump()
    {
        var mover = antonio.GetComponent<AntonioMover>();
        mover.Jump(6f);   // jump upwards
    }


    private System.Collections.IEnumerator AutoRunAntonio()
    {
        if (antonio == null || finishLine == null) yield break;

        isAnimating = true;

        Vector3 start = antonio.position;
        Vector3 end = new Vector3(finishLine.position.x - 0.5f, start.y, start.z);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / antonioRunTime;
            antonio.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        antonio.position = end;
        isAnimating = false;

        // Do NOT auto-advance here – player will click to go to next step.
    }

    private System.Collections.IEnumerator AutoRunMarco()
    {
        if (marco == null || antonio == null) yield break;

        isAnimating = true;

        Vector3 start = marco.position;
        Vector3 end = new Vector3(antonio.position.x - 1f, start.y, start.z);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / marcoRunTime;
            marco.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        marco.position = end;
        isAnimating = false;
    }
}
