using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    public Collider2D swordCollider; // Reference to the sword's collider
    public float attackDuration = 1f; // Duration of the attack in seconds
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        swordCollider.enabled = false; // Disable the sword collider at the start
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void EndSwordAttack()
    {
        swordCollider.enabled = false; // Disable the sword collider when the attack ends
        PlayerMovement.Instance.EndAttack();
    }
    
    
    public void ActiveSwordAttack()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {

        swordCollider.enabled = true;
        yield return new WaitForSeconds(attackDuration); // Wait for the attack duration
        swordCollider.enabled = false; // Disable the sword collider after the attack

    }
}
