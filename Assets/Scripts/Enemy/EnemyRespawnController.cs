using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    //Punto donde de aparaci�n original del enemigo
    public Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        //Guardamos punto de aparici�n
        spawnPoint = transform.position;
    }

    
}
