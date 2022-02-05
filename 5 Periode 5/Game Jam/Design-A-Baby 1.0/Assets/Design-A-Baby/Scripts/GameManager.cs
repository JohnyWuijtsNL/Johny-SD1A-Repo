using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void StartGame(float time, bool startAsWin, string gameplayText)
    {
        Debug.Log("Time given: " + time + "   Start as win: " + startAsWin + "   Text given: " + gameplayText);
    }


    public int GetHealth() { return 3; }
    public float GetProgressValue() { return 0.5f; }
    public float GetTime() { return 0.75f; }
    public int GetScore() { return 1; }
    public void SetWon(bool won) { Debug.Log("Won value gas been set to: " + won); }
}
