using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOrLever : MonoBehaviour
{
    //Referencia a los objetos que el botón o palanca afecta
    public GameObject objectToAffect;
    //Referencia al Panel de Info
    public GameObject infoPanel;

    //Variable para saber si el botón o palanca ha sido activado
    bool isUsed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si el jugador está lo suficientemente cerca del botón aparece la señal
        if (Vector2.Distance(transform.position, PlayerController.sharedInstance.transform.position) < .6)
            infoPanel.SetActive(true);
        else infoPanel.SetActive(false);

        if (Input.GetKeyDown(KeyCode.F) && infoPanel)
        {
            //Si el interruptor no está activado
            if (!isUsed)
            {
                //Activamos el objeto
                objectToAffect.SetActive(true);
                //Activamos el interruptor
                isUsed = true;
                //Activamos la animación del botón

            }
            else
            {
                //Desactivamos el objeto
                objectToAffect.SetActive(false);
                //Cambiamos el estado del botón
                isUsed = false;
                //Activamos la animación del botón
            }

        }
    }
}
