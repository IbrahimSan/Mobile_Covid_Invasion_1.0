using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;

    // To Update the Score
    void Update()
    {
        scoreText.GetComponent<Text>().text = "Points: " + theScore;
    }
}
