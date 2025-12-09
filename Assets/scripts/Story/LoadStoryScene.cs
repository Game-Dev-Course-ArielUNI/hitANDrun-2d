using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStoryScene : MonoBehaviour
{
    // This method will be called by the UI Button
    public void LoadStory()
    {
        SceneManager.LoadScene("story");   // exactly the name of your story scene
    }
}
