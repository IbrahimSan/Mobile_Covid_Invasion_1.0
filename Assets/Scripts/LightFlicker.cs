using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public Light light;

    public float MinTime;
    public float MaxTime;
    public float Timer;

    public AudioSource audioSource;
    public AudioClip lightAudio;

    // Start is called before the first frame update
    void Start()
    {
        Timer = Random.Range(MinTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        Flicker();
    }

    //Flicking light with sound at every flick
    void Flicker()
    {
        if (Timer > 0)
            Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            light.enabled = !light.enabled;
            Timer = Random.Range(MinTime, MaxTime);
            audioSource.PlayOneShot(lightAudio);

        }
    }
}
