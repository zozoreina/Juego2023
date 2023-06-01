using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //Para saber si ha sido recogido
    bool isCollected;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isCollected)
        {
            if(PlayerHealthController.sharedInstance.currentHealth != PlayerHealthController.sharedInstance.maxHealth)
            {
                isCollected = true;
                PlayerHealthController.sharedInstance.HealPlayer(PlayerHealthController.sharedInstance.maxHealth / 2);
                Destroy(this.gameObject);
            }
        }
    }
}
