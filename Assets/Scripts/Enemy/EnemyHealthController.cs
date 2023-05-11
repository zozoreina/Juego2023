using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    //Vida
    public int maxHealth;
    int currentHealth;


    //Singleton
    public static EnemyHealthController sharedInstance;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    public void EnemyTakingDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            GetComponent<EnemyKnockBackController>().EnemyKnockback();
        }
    }

}
