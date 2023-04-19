using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{

    public float spellSpeed;
    public GameObject impactEffect;
    GameObject[] Clone;
    private new Rigidbody rigidbody;
    

    // Start is called before the first frame update
    void Start()
    { 
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * spellSpeed;
        

    }

    // Spell impact & Destroy Object
    private void OnTriggerEnter(Collider collision)
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }

   
    // Clone Removing
    private void Update()
    {

        Clone = GameObject.FindGameObjectsWithTag("Explosions");
        for (int i = 0; i < Clone.Length; i++)
        {
            Destroy(Clone[i].gameObject);
        }
    }

}
