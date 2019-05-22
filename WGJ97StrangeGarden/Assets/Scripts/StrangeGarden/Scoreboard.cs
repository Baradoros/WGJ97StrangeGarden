using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    private static int playerScore;
    [SerializeField] private static TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        Debug.Log(scoreText);
        scoreText.text = "Score: " + playerScore.ToString();
        playerScore = 0;
    }

    public static void AddScore(int score)
    {
        playerScore += score;
        scoreText.text = "Score: " + playerScore.ToString();

    }

    public static void ResetScore()
    {
        playerScore = 0;
    }
}
