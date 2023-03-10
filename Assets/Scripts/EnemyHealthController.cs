using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public int maxHealth;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyTakingDamage(int damage)
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {

        }
    }

}
