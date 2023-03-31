using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionsMenu : MonoBehaviour
{
    //Referencia al panel de aviso para hablar con los compa�eros
    public GameObject infoPanel;
    //Referencia al Panel de conversaci�n
    public GameObject conversationPanel;
    //Referencia a los botones de personalizaci�n de compa�eros
    public GameObject companion1Settings;
    public Button[] companion2Buttons;

    //Variable para saber si est�s conversando
    public bool isConversationOn;
    //Variables para saber que compa�ero est� activo en la conversaci�n
    bool comp1, comp2;

    //Referencia al Menu de pausa
    public PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.sharedInstance.transform.position, CompanionController.sharedInstance.transform.position) < 3 && PlayerController.sharedInstance.isLeft && !isConversationOn && !pauseMenu.isPaused/*ME queda por a�adir que no puede estar en combate*/)
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

    //M�todo por el que comienza la conversaci�n con los compa�eros
    public void SetGameToConversation()
    {
        Time.timeScale = 0f;
        isConversationOn = true;
        conversationPanel.SetActive(true);
        companion1Settings.SetActive(true);
    }

    //M�todo para salir del modo conversaci�n
    public void OutOfConversation()
    {
        Time.timeScale = 1f;
        isConversationOn = false;
        conversationPanel.SetActive(false);
    }

    //M�todo para activar el compa�ero 1 y desactivar el 2
    public void Comp1ConvButton()
    {
        comp1 = true;
        comp2 = false;               
    }
    public void Comp2ConvButton()
    {
        comp1 = false;
        comp2 = true;
    }



}
