using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCard : MonoBehaviour
{

    public GameObject card;

    // Start is called before the first frame update
    void Start()
    {
        // Calling teh Card and it's Activations
        card = GameObject.FindGameObjectWithTag("AccessCard");
        card.SetActive(false);
        card.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Showing Card when reaching specific score
        if (ScoringSystem.theScore >= 90)
        {
                card.SetActive(true);
        }
        else
        {
            card.SetActive(false);
        }
    }
}
