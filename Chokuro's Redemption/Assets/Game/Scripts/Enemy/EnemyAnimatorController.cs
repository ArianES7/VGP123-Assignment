using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    public Animator animator;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void UpdateAnimation(Vector2 velocity)
    {
        animator.SetFloat("Speed", Mathf.Abs(velocity.x));

        if (velocity.x > 0.01f)
            sr.flipX = true; // started facing left
        else if (velocity.x < -0.01f)
            sr.flipX = false;
    }

    public void TriggerDeath()
    {
        animator.SetTrigger("Died");
    }
}
