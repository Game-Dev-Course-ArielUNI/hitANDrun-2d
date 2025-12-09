using UnityEngine;



public enum GamePhase { Throw, Run, GameOver, Win }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GamePhase phase = GamePhase.Throw;

    private int blocksHit = 0;

    public AntonioController antonio;   // drag in Inspector
    public MarcoChase marco;            // drag in Inspector

    void Awake()
    {
        Instance = this;
    }

    public void OnPileHit()
    {
        blocksHit++;

        if (blocksHit >= 3)   // or however many blocks your pile has
        {
            //  Start both run & chase immediately
            phase = GamePhase.Run;
            antonio.BeginRun();
            marco.BeginChase();
        }
    }

    public void OnMarcoCatch()
    {
        phase = GamePhase.GameOver;
        Debug.Log("GAME OVER!");
    }

    public void OnAntonioReachFinish()
    {
        phase = GamePhase.Win;
        Debug.Log("YOU WIN!");
    }
}






//public enum GamePhase { Throw, Run, Chase, GameOver, Win }

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public GamePhase phase = GamePhase.Throw;

//    private int blocksHit = 0;

//    void Awake()
//    {
//        Instance = this;
//    }

//    public void OnPileHit()
//    {
//        blocksHit++;

//        if (blocksHit >= 3) // all blocks hit
//        {
//            phase = GamePhase.Run;
//            FindObjectOfType<AntonioController>().BeginRun();
//        }
//    }

//    public void OnMarcoCatch()
//    {
//        phase = GamePhase.GameOver;
//        Debug.Log("GAME OVER!");
//    }

//    public void OnAntonioReachFinish()
//    {
//        phase = GamePhase.Win;
//        Debug.Log("YOU WIN!");
//    }
//}
