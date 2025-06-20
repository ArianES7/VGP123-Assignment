using UnityEngine;

public class SwordHitbox : MonoBehaviour
{ 
    public int damageAmount = 1; // How much damage the sword does

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }
        
    }
}
