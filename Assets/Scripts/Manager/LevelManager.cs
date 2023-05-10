using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //Cantidad de tiempo a esperar que el jugador respawnee
    public float waitToRespawn;



    //Singleton
    public static LevelManager sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    
    //Para respawear al jugador
    public void respawnPlayer()
    {
        //Llamamos a la corrutina
        StartCoroutine(respawnPlayerCo());
    }

    //M�todo para respawnear al compa�ero
    public void respawnCompanion(GameObject Companion)
    {
        StartCoroutine(respawnCompanionCo(Companion));
    }

    //M�todo para respawnear enemigos
    public void respawnEnemy(GameObject Enemy)
    {
        StartCoroutine(respawnEnemyCo(Enemy));
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
        //Respawneamos a los compa�eros con junto con �l
        respawnCompanion(PlayerController.sharedInstance.companion1);
        respawnCompanion(PlayerController.sharedInstance.companion2);
        //Lo movemos a la posici�n de respawn
        PlayerController.sharedInstance.transform.position = CheckpoitnController.sharedInstance.spawnPoint;
        //Movemos la c�mara a la posici�n del jugador
        CameraController.sharedInstance.ChangeCurrentPos(1);
    }

    IEnumerator respawnCompanionCo(GameObject Companion)
    {
        //Companion.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        //Companion.gameObject.SetActive(true);
        Companion.transform.position = Companion.GetComponent<CompanionController>().objetivePos[0].transform.position;
    }

    IEnumerator respawnEnemyCo(GameObject Enemy)
    {
        //Enemy.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        //Enemy.gameObject.SetActive(true);
        Enemy.transform.position = Enemy.GetComponent<EnemyController>().spawnPoint;
    }
}
