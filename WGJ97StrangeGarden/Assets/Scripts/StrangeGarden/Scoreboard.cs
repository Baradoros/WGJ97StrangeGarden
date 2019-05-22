using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    private static int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddScore(int score)
    {
        playerScore += score;
    }

    public static void ResetScore()
    {
        playerScore = 0;
    }
}
