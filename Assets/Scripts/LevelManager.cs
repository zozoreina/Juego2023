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

    }

    IEnumerator respawnPlayerCo()
    {
        //Desactivamos al jugador
        PlayerController.sharedInstance.gameObject.SetActive(false);
        //Esperamos un timepo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //volvemos a activar al jugador 
        PlayerController.sharedInstance.gameObject.SetActive(true);
        //Lo movemos a la posición de respawn

    }

}
