using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionsMenu : MonoBehaviour
{
    //Referencia al panel de aviso para hablar con los compañeros
    public GameObject infoPanel;
    //Referencia al Panel de conversación
    public GameObject conversationPanel;
    //Referencia a los botones de personalización de compañeros
    public GameObject companion1Settings;
    public GameObject companion2Settings;
    //public Button[] companion1Buttons; //No hace nada de momento

    //Variable para saber si estás conversando
    public bool isConversationOn;
    //Variables para saber que compañero está activo en la conversación
    //bool comp1, comp2; No hacen nada de momento

    //Referencia al Menu de pausa
    public PauseMenu pauseMenu;


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.sharedInstance.transform.position, CompanionController.sharedInstance.transform.position) < 3.5f && PlayerController.sharedInstance.isLeft && !isConversationOn && !pauseMenu.isPaused/*ME queda por añadir que no puede estar en combate*/)
        {
            infoPanel.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.E))
            {
                SetGameToConversation();
            }
        }
        else
            infoPanel.SetActive(false);

        if (isConversationOn && Input.GetKeyDown(KeyCode.Escape))
            OutOfConversation();            
            
    }

    //Método por el que comienza la conversación con los compañeros
    public void SetGameToConversation()
    {
        Time.timeScale = 0f;
        isConversationOn = true;
        conversationPanel.SetActive(true);
        companion1Settings.SetActive(true);
    }

    //Método para salir del modo conversación
    public void OutOfConversation()
    {
        Time.timeScale = 1f;
        isConversationOn = false;
        conversationPanel.SetActive(false);
    }

    //Método para activar el compañero 1 y desactivar el 2
    public void Comp1ConvButton()
    {
        
        companion1Settings.SetActive(true);
        companion2Settings.SetActive(false);

    }
    public void Comp2ConvButton()
    {
        
        companion2Settings.SetActive(true);
        companion1Settings.SetActive(false);
    }



}
