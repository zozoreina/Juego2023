using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageToPlayer;

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
        if(collision.CompareTag("Player"))
        {
            if (gameObject.tag == ("Trap"))
            {
                collision.GetComponent<PlayerHealthController>().DealWithTrapDamage();
            }
            else
            {
                collision.GetComponent<PlayerHealthController>().DealWithDamage(damageToPlayer);
            }
        }
    }
}
