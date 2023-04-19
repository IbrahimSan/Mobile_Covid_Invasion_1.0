using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    //Interacting with player when in range
    public float lookRadius;

    Transform target;
    NavMeshAgent agent;
    Animator Eanim;

    // Call on Start of Game
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        Eanim = GetComponent<Animator>();
    }

    // Updating while in Game
    void Update()
    {
        //Calling the Distance
        float distance = Vector3.Distance(target.position, transform.position);

        //Counting distance and player range
        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            // if enemy stopped near player start attacking
            if (distance <= agent.stoppingDistance)
            {
                Eanim.SetBool("Eidle", false);
                Eanim.SetBool("Eattack", true);

                FaceTarget();

            }
            else
            {
                Eanim.SetBool("Eidle", true);
                Eanim.SetBool("Eattack", false);

            }
           
        }
        
    }

    //Looking at player when in range
    void FaceTarget()
    {
        
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        
    }

    //Creating wire sphere to show range of interaction
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

}
