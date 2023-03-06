using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    //Varibales para el contador de tiempo de invencibilidad
    public float invinvibleLength;
    private float invinvibleCounter;
    //Variables para la vida del jugador
    public float maxHealth;
    public float currentHealth;

    //Referencia al SpriteRenderer
    public SpriteRenderer theSR;

    //Singleton
    public static PlayerHealthController sharedInstance;

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
        //Obtenemos el sprite renderer
        theSR = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Comprobamos si el contador de invencibilidad está vacio
        if(invinvibleCounter > 0)
        {
            //Le restamos 1 cada segundo
            invinvibleCounter -= Time.deltaTime;

            //Cuando el contador sea 0 
            if(invinvibleCounter <= 0)
            {
                //Cambiamos la transparencia del jugador
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    //Método para dañar al jugador
    public void DealWithDamage(int damage)
    {
        if(invinvibleCounter > 0)
        {

            currentHealth -= damage;

            //Si la vida es menor o igual que 0
            if(currentHealth <= 0)
            {
                currentHealth = 0;

                LevelManager.sharedInstance.respawnPlayer();
            }
            else
            {
                invinvibleCounter = invinvibleLength;

                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);

                PlayerController.sharedInstance.KnockBack();
            }
        }
    }

    //Método para dañar por trampa el jugador
    public void DealWithTrapDamage()
    {
        if(invinvibleCounter > 0)
        {

            currentHealth -= Mathf.Max(10, Mathf.RoundToInt(currentHealth/5) + 1);

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                LevelManager.sharedInstance.respawnPlayer();
            }
            else
            {
                invinvibleCounter = invinvibleLength;

                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);

                PlayerController.sharedInstance.KnockBack();
            }
        }
    }

    //Método para curar al jugador
    public void HealPlayer(int heal)
    {
        currentHealth += heal;

        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

    }

    public void ChangeInvinvibleCounter(float changeTo)
    {
        invinvibleCounter = changeTo;
    }
}
