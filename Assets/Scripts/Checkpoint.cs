using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    //referencia al sprite renderer
    public SpriteRenderer theSR;
    //Sprite activo y desactivado
    public Sprite cpon, cpoff;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al SR
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el jugador entra en el checkpoint
        if(collision.CompareTag("Player"))
        {
            //Desactivamos los checkpoints anteriores
            CheckpoitnController.sharedInstance.DesactivateCheckpoint();

            //Cambiamos el sprite
            theSR.sprite = cpon;

            //Le pasamos el checkpoint controller la nueva pos de reaparicion
            CheckpoitnController.sharedInstance.SetSpawnPoint(transform.position);
        }
    }

    //Método para desactivar los checkpoints
    public void ResetCheckpoint()
    {
        theSR.sprite = cpoff;
    }

}
