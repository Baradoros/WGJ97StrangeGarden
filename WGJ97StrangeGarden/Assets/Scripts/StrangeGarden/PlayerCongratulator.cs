using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCongratulator : MonoBehaviour
{
    [SerializeField] int pointThreshold =               200;
    [SerializeField] ScoreDisplay scoreDisplay;
    [SerializeField] GameObject congratsScreen;

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay.ScoreChange.AddListener(OnScoreChange);
    }

    void OnScoreChange()
    {
        if (scoreDisplay.score >= pointThreshold)
        {
            Time.timeScale =                            0;
            congratsScreen.SetActive(true);
        }
    }
}
