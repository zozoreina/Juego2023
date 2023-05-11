using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawnController : MonoBehaviour
{
    //Punto donde de aparación original del enemigo
    public Vector3 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        //Guardamos punto de aparición
        spawnPoint = transform.position;
    }

    
}
