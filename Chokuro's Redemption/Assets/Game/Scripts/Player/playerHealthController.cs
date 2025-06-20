using System;
using UnityEngine;

public class playerHealthController : Singelton<playerHealthController>
{

    

    public float currentHealth, maxHealth;
    public SpriteRenderer sr;
    public Animator anim;

    private bool canTakeDamage = true;
    
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Starting health: " + currentHealth); // Add this
    }

    public void DealDamage()
    {
      

        currentHealth--;

        Debug.Log("Took Damage! HP: " + currentHealth);

        if (currentHealth >= 0)
        {
            

            GameManager.Instance.PlayerDamaged(); // this decreases lives

            Debug.Log("We Here!");

            if (GameManager.Instance.Lives >= 0)
            {
                Invoke(nameof(Respawn), 0.1f); // Respawn if lives remain
            }
            else
            {
                GameManager.Instance.TriggerGameOver();
            }
        }
        else
        {
            // Optional: show hit animation, screen flash, etc.
        }
    }

    void Respawn()
    {
        RespawnManager.Instance.RespawnPlayer();
        canTakeDamage = true;
    }

    public void ResetDamageFlag()
    {
        canTakeDamage = true;
    }
   
}
