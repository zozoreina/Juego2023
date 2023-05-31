using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOrLever : MonoBehaviour
{
    //Referencia a los sprites del botón
    public Sprite[] buttonSprites;
    //Referencia al SpriteRenderer
    SpriteRenderer thrSR;
    //Referencia a los objetos que el botón o palanca afecta
    public GameObject objectToAffect;
    //Referencia al Panel de Info
    public GameObject infoPanel;

    //Variable para saber si el botón o palanca ha sido activado
    bool isUsed;
    

    // Start is called before the first frame update
    void Start()
    {
        thrSR = GetComponent<SpriteRenderer>();
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
                //Cambiamos el sprite del botón
                thrSR.sprite = buttonSprites[1];
            }
            else
            {
                //Desactivamos el objeto
                objectToAffect.SetActive(false);
                //Cambiamos el estado del botón
                isUsed = false;
                //Cambiamos el sprite del botón
                thrSR.sprite = buttonSprites[0];
            }

        }
    }
}
