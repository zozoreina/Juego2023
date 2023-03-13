using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpoitnController : MonoBehaviour
{
    //Declaramos un array donde guardar los checkpoints de la escena
    private Checkpoint[] checkpoints;

    //Referencia a la posicion del jugador
    public Vector3 spawnPoint;

    //Singleton
    public static CheckpoitnController sharedInstance;

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
        //Buscamos todos los game objects que tengan el script asociado 
        checkpoints = GetComponentsInChildren<Checkpoint>();
        //Guardamos la posición inicial como punto de checkpoint
        spawnPoint = PlayerController.sharedInstance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para desactivar checkpoints
    public void DesactivateCheckpoint()
    {
        for(int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    //Método para generar el punto de aparición del jugador
    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }

}
