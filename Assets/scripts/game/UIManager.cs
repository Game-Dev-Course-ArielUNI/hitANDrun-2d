using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject winPanel;

    private void Awake()
    {
        Instance = this;
        winPanel.SetActive(false);
    }

    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
    }
}
