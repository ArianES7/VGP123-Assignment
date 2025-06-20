using UnityEngine;

public class RespawnManager : Singelton<RespawnManager>
{
    private Vector3 spawnPosition;

    protected override void Awake()
    {
        base.Awake();
        // default spawn is Mario’s start position
        if (PlayerMovement.Instance != null)
            spawnPosition = PlayerMovement.Instance.transform.position;
    }

    /// <summary>Called by a checkpoint or at start to set where Mario respawns.</summary>
    public void SetSpawnPoint(Vector3 pos)
    {
        spawnPosition = pos;
    }

    /// <summary>Moves Mario back to the last spawn point and resets his state.</summary>
    public void RespawnPlayer()
    {
        var player = PlayerMovement.Instance;
        var health = playerHealthController.Instance;
        

        // 1) Reposition
        player.transform.position = spawnPosition;

        // 2) Reset velocity
        player.rb.linearVelocity = Vector2.zero;

        // 3) Reset health
        health.currentHealth = health.maxHealth;
        
        health.sr.color = new Color(health.sr.color.r, health.sr.color.g, health.sr.color.b, 1f);


        // 5) (Optional) Recenter camera: Cinemachine will follow the player automatically
    }
}

