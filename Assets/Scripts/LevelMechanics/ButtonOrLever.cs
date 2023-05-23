using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOrLever : MonoBehaviour
{
    //Referencia a los objetos que el bot�n o palanca afecta
    public GameObject objectToAffect;
    //Referencia al Panel de Info
    public GameObject infoPanel;

    //Variable para saber si el bot�n o palanca ha sido activado
    bool isUsed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Si el jugador est� lo suficientemente cerca del bot�n aparece la se�al
        if (Vector2.Distance(transform.position, PlayerController.sharedInstance.transform.position) < .6)
            infoPanel.SetActive(true);
        else infoPanel.SetActive(false);

        if (Input.GetKeyDown(KeyCode.F) && infoPanel)
        {
            //Si el interruptor no est� activado
            if (!isUsed)
            {
                //Activamos el objeto
                objectToAffect.SetActive(true);
                //Activamos el interruptor
                isUsed = true;
                //Activamos la animaci�n del bot�n

            }
            else
            {
                //Desactivamos el objeto
                objectToAffect.SetActive(false);
                //Cambiamos el estado del bot�n
                isUsed = false;
                //Activamos la animaci�n del bot�n
            }

        }
    }
}