using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectingScore : MonoBehaviour
{

    public AudioSource collectSound;

    public Text highScore;

    // Start is called before the first frame update
    void Start()
    {
        //Making your score go to zero 
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // To Reset HighScore
    public void ResetHS()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "0";
    }
}
