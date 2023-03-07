using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    //Para saber si ha sido recogido
    bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
