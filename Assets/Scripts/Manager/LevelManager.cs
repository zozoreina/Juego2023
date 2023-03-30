using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Singleton
    public static LevelManager sharedInstance;

    //Cantidad de tiempo a esperar que el jugador respawnee
    public float waitToRespawn;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Para respawear al jugador
    public void respawnPlayer()
    {
        //Llamamos a la corrutina
        StartCoroutine(respawnPlayerCo());
    }

    //Método para respawnear al compañero
    public void respawnCompanion()
    {
        StartCoroutine(respawnCompanionCo());
    }

    IEnumerator respawnPlayerCo()
    {
        //Desactivamos al jugador
        PlayerController.sharedInstance.gameObject.SetActive(false);
        //Esperamos un timepo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //volvemos a activar al jugador 
        PlayerController.sharedInstance.gameObject.SetActive(true);
        //Le devolvemos algo de vida
        PlayerHealthController.sharedInstance.currentHealth = PlayerHealthController.sharedInstance.maxHealth;
        //Lo movemos a la posición de respawn
        PlayerController.sharedInstance.transform.position = CheckpoitnController.sharedInstance.spawnPoint;
    }

    IEnumerator respawnCompanionCo()
    {
        CompanionController.sharedInstance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        CompanionController.sharedInstance.gameObject.SetActive(true);
        CompanionController.sharedInstance.transform.position = CompanionController.sharedInstance.objetivePos[0].position;
    }

}
