using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
      
            playerHealthController player = other.GetComponent<playerHealthController>();
            if (player != null)
            {
                player.DealDamage();
            }
        
    }
}
