using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
  public int currentHealth;

    private EnemyPathFollow pathFollow;
    private EnemyAnimatorController animatorController;

    void Start()
    {
        currentHealth = maxHealth;
        pathFollow = GetComponent<EnemyPathFollow>();
        animatorController = GetComponent<EnemyAnimatorController>();
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            animatorController?.TriggerDeath();
            pathFollow?.DisableMovement();
            // Optionally destroy or disable enemy:
             Destroy(gameObject, 1f);
        }
    }
}
