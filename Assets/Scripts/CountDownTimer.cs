using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownTimer : MonoBehaviour
{
    public Text timer;
    public float time;

    // Called on start of Game
    private void Start()
    {
        StartCountDownTimer();
    }

    // Count Down Counter
    void StartCountDownTimer()
    {
        timer.text = "TimeLeft: ";
        InvokeRepeating("UpdateTimer", 0.0f, 0.01667f);
    }

    //Timer counter
    private void FixedUpdate()
    {
        if (timer != null)
        {
            time -= Time.deltaTime;
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = (time % 60).ToString("00");
            string fraction = ((time * 100) % 100).ToString("000");
            timer.text = "Time Left: " + minutes + ":" + seconds + ":" + fraction;
        }
    }
}
