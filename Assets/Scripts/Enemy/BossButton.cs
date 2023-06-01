using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossButton : MonoBehaviour
{
    //Referencia a los sprites del botón
    public Sprite[] buttonSprites;
    
    //Referencia a los objetos que el botón o palanca afecta
    public GameObject objectToAffect;
    //Referencia al Panel de Info
    public GameObject infoPanel;

    //Variable para saber si el botón o palanca ha sido activado
    bool isUsed;


    

    // Update is called once per frame
    void Update()
    {
        //Si el jugador está lo suficientemente cerca del botón aparece la señal
        if (Vector2.Distance(transform.position, PlayerController.sharedInstance.transform.position) < .6)
            infoPanel.SetActive(true);
        else infoPanel.SetActive(false);

        if (Input.GetKeyDown(KeyCode.F) && infoPanel)
        {
            objectToAffect.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
