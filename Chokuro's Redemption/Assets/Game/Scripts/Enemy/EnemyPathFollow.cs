using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFollow : MonoBehaviour
{ [Header("Path Points")]
    public List<Transform> pathPoints = new List<Transform>();
    public float moveSpeed = 2f;
    public bool loop = true;
    public float pointReachedThreshold = 0.05f; // ← tighter threshold

    private int currentIndex = 0;
    private Rigidbody2D rb;
    private EnemyAnimatorController animatorController;
    private bool isDead = false;

    void Start()
    {
        if (pathPoints.Count < 2)
        {
            Debug.LogWarning("Assign at least 2 path points to EnemyPathFollow.");
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<EnemyAnimatorController>();
        transform.position = pathPoints[0].position;
    }

    void FixedUpdate()
    {
        if (isDead || pathPoints.Count == 0) return;

        Transform targetPoint = pathPoints[currentIndex];
        Vector2 direction = (targetPoint.position - transform.position);
        float distance = direction.magnitude;

        // Switch to next point if close enough
        if (distance < pointReachedThreshold)
        {
            currentIndex++;
            if (currentIndex >= pathPoints.Count)
            {
                currentIndex = loop ? 0 : pathPoints.Count - 1;
            }
            return; // Skip movement this frame
        }

        // Move and animate
        Vector2 move = direction.normalized * moveSpeed;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
        animatorController?.UpdateAnimation(move);
    }

    public void DisableMovement()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Count < 2) return;

        Gizmos.color = Color.red;
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            if (pathPoints[i] && pathPoints[i + 1])
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
        }

        if (loop && pathPoints.Count > 2)
            Gizmos.DrawLine(pathPoints[^1].position, pathPoints[0].position);

        Gizmos.color = Color.yellow;
        foreach (var point in pathPoints)
        {
            if (point)
                Gizmos.DrawSphere(point.position, 0.1f);
        }
    }
#endif
}

