using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanetizeSpell : MonoBehaviour
{
    public float spellSpeed;
    public GameObject impactEffect;
    GameObject[] Clone;
    private new Rigidbody rigidbody;
    public float radius = 5f;
    public float force = 700f;

    //calling Enemy script
    public Enemy enemyEnter;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * spellSpeed;


    }
    
        // Spell impact, pushing enemy away & Destroy Object
        IEnumerator OnTriggerEnter(Collider other)
        {
            yield return new WaitForSeconds(2);

            Instantiate(impactEffect, transform.position, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders){

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)
            {

                rb.AddExplosionForce(force, transform.position, radius);
            }

                
                Destroy(gameObject);

            }
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
