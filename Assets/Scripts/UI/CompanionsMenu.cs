using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionsMenu : MonoBehaviour
{
    //Referencia al panel de aviso para hablar con los compañeros
    public GameObject infoPanel;
    //Referencia al Panel de conversación
    public GameObject conversationPanel;

    //Variable para saber si estás conversando
    public bool isConversationOn;
    //Variables para saber que compañero está activo en la conversación
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
        if (Vector3.Distance(PlayerController.sharedInstance.transform.position, CompanionController.sharedInstance.transform.position) < 3 && PlayerController.sharedInstance.isLeft && !isConversationOn && !pauseMenu.isPaused/*ME queda por añadir que no puede estar en combate*/)
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
    }

    //Método para salir del modo conversación
    public void OutOfConversation()
    {
        Time.timeScale = 1f;
        isConversationOn = false;
        conversationPanel.SetActive(false);
    }

    //Método para activar el compañero 1 y desactivar el 2
    public void Comp1Conv()
    {
        comp1 = true;
        comp2 = false;
        
    }

}
