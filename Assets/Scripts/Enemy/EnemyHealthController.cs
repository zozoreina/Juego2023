using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    //Vida
    public int maxHealth;
    public int currentHealth;

    //Referencia al objeto que suelta el enemigo al morir
    public GameObject dropeable;

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
            Instantiate(dropeable, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
        else
        {
            GetComponent<EnemyKnockBackController>().EnemyKnockback();
        }
    }

}
