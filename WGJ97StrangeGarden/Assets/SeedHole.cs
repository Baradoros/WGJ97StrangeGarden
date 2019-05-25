using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedHole : MonoBehaviour
{
    [SerializeField] int pointGain = 100;
    [SerializeField] ScoreDisplay scoreDisplay;


    void OnTriggerEnter2D(Collider2D other)
    {
        // When a seed enter this, destroy it, and give the player some points
        SeedController seed = other.GetComponent<SeedController>();

        if (seed != null)
        {
            Destroy(seed.gameObject);
            scoreDisplay.score += pointGain;
        }
    }
}
