using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Health
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    //Collecting Score Points
    public CollectingScore CS;

    public AudioSource audioSource;

    // Call on Start of the Game
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        audioSource = GetComponent<AudioSource>();
    }

    // keep updating in Game
    void Update()
    {
        // Death and Adding Score
        if (currentHealth <= 0)
        {
           
            ScoringSystem.theScore += 15;
            Destroy(gameObject);
            if(ScoringSystem.theScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", ScoringSystem.theScore);
                CS.highScore.text = ScoringSystem.theScore.ToString();
            }
        }

    }

    //Triggering 2 different damages and wait time for entering trigger
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Sanetize"))
        {
            yield return new WaitForSeconds(2);
            TakeDamage(50);
        }
        else
        {
            audioSource.Play();
            TakeDamage(Random.Range(5, 18));
        }
        
    }

    // Taking Damage
    public void TakeDamage(int damage)

    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

    }
}
