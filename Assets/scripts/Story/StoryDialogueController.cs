using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StoryDialogueController : MonoBehaviour
{
    [TextArea(2, 4)]
    public string[] lines;

    // WORLD SPACE TextMeshPro (not TextMeshProUGUI)
    public TextMeshPro dialogueText;

    public Transform antonioAnchor;
    public Transform marcoAnchor;
    public Transform circleTransform;   // your speech bubble / circle sprite

    // small offset above the anchor
    public Vector3 offset = new Vector3(0f, 1.5f, 0f);

    public string nextSceneName = "";

    private int index = 0;

    void Start()
    {
        index = 0;
        ShowCurrentLine();
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            index++;

            if (index >= lines.Length)
            {
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                return;
            }

            ShowCurrentLine();
        }
    }

    void ShowCurrentLine()
    {
        dialogueText.text = lines[index];

        // even index = Antonio, odd = Marco
        Transform target = (index % 2 == 0) ? antonioAnchor : marcoAnchor;

        Vector3 targetPos = target.position + offset;

        circleTransform.position = targetPos;
        dialogueText.transform.position = targetPos;
    }
}



























//using UnityEngine;
//using TMPro;  // remove this line if you use normal UI Text
//using UnityEngine.SceneManagement;

//public class StoryDialogueController : MonoBehaviour
//{
//    [TextArea(2, 4)]
//    public string[] lines;          // all dialog sentences in order

//    public TextMeshProUGUI dialogueText;   // the text above the circle
//    public Transform antonioAnchor;        // point above Antonio
//    public Transform marcoAnchor;          // point above Marco
//    public Transform circleTransform;      // the circle image/sprite

//    public string nextSceneName = "";      // optional: scene after story

//    private int index = 0;

//    void Start()
//    {
//        index = 0;
//        ShowCurrentLine();
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))   // any mouse click
//        {
//            index++;

//            // finished all lines
//            if (index >= lines.Length)
//            {
//                if (!string.IsNullOrEmpty(nextSceneName))
//                {
//                    SceneManager.LoadScene(nextSceneName);
//                }
//                return;
//            }

//            ShowCurrentLine();
//        }
//    }

//    void ShowCurrentLine()
//    {
//        dialogueText.text = lines[index];

//        bool antonioSpeaking = (index % 2 == 0);

//        if (antonioSpeaking)
//        {
//            circleTransform.position = antonioAnchor.position;
//            dialogueText.transform.position = antonioAnchor.position;   // NEW
//        }
//        else
//        {
//            circleTransform.position = marcoAnchor.position;
//            dialogueText.transform.position = marcoAnchor.position;     // NEW
//        }
//    }

//}
