using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Pickup : MonoBehaviour
{

    private int points = 1;
    private int score = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            GameManager.Instance.addGem(points);
            GameManager.Instance.AddScore(score);
            AudioManager.Instance.PlaySound("Score");

            Destroy(gameObject); 
        }
    }
}