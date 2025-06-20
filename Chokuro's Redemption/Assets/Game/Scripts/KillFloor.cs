using System;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      
      GameManager.Instance.PlayerDamaged();
      RespawnManager.Instance.RespawnPlayer();
      
   }
}
