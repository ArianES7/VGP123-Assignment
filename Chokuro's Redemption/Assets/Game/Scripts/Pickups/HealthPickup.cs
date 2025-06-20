using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HealthPickup : MonoBehaviour
{

    private int Health = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if(GameManager.Instance.Lives >= 3)
            {
                return;
            }

            GameManager.Instance.addLives(Health);
        
            Destroy(gameObject); 
        }
    }
}
