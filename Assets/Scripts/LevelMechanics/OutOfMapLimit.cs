using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapLimit : MonoBehaviour
{
    //Método para respawnear cosas que se caigan fuera del mapa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.sharedInstance.respawnPlayer();
        }
        else if (collision.CompareTag("Companion"))
            LevelManager.sharedInstance.respawnCompanion();
        else if (collision.CompareTag("Enemy"))
            LevelManager.sharedInstance.respawnEnemy();
    }
}
