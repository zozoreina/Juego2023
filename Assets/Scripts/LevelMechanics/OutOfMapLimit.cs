using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapLimit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para respawnear cosas que se caigan fuera del mapa
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelManager.sharedInstance.respawnPlayer();
        }
        else if (collision.CompareTag("Companion"))
            LevelManager.sharedInstance.respawnCompanion();
    }
}
