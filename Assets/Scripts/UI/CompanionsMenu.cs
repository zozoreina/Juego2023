using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionsMenu : MonoBehaviour
{
    //Referencia al panel de aviso para hablar con los compa�eros
    public GameObject infoPanel;
    //Referencia al Panel de conversaci�n
    public GameObject conversationPanel;

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
    }

    //M�todo para salir del modo conversaci�n
    public void OutOfConversation()
    {
        Time.timeScale = 1f;
        isConversationOn = false;
        conversationPanel.SetActive(false);
    }

    //M�todo para activar el compa�ero 1 y desactivar el 2
    public void Comp1Conv()
    {
        comp1 = true;
        comp2 = false;
        
    }

}
