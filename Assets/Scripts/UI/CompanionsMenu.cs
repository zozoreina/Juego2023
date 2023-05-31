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
    //Referencia a los sprites que indican que habilidades están activas
    public Image[] tellers;

    //Referencia a los botones de los compañeros
    public Button comp1Button, comp2Button;
    //Boleanas para saber que menú está activo
    bool comp1men = true, comp2men;

    

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

        TellerController();
            
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
        comp1men = true;
        companion2Settings.SetActive(false);
        comp2men = false;

    }
    public void Comp2ConvButton()
    {
        
        companion2Settings.SetActive(true);
        comp2men = true;
        companion1Settings.SetActive(false);
        comp1men = false;
    }

    //Método para activar y desactivar los tellers en base a las habilidades que el jugador tiene activas
    public void TellerController()
    {
        if (PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().airDash && comp1men)
            tellers[0].color = comp1Button.colors.selectedColor;
        else if (PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().airDash && comp2men)
            tellers[0].color = comp2Button.colors.selectedColor;
        else tellers[0].color = comp1Button.colors.normalColor;

        if (PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().doubleJump && comp1men)
            tellers[1].color = comp1Button.colors.selectedColor;
        else if (PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().doubleJump && comp2men)
            tellers[1].color = comp2Button.colors.selectedColor;
        else tellers[1].color = comp1Button.colors.normalColor;

        if (PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().attackCombo && comp1men)
            tellers[2].color = comp1Button.colors.selectedColor;
        else if (PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo && comp2men)
            tellers[2].color = comp2Button.colors.selectedColor;
        else tellers[2].color = comp1Button.colors.normalColor;

        if (PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().airAttack && comp1men)
            tellers[3].color = comp1Button.colors.selectedColor;
        else if (PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().airAttack && comp2men)
            tellers[3].color = comp2Button.colors.selectedColor;
        else tellers[3].color = comp1Button.colors.normalColor;

        if (PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().distanceAttack && comp1men)
            tellers[4].color = comp1Button.colors.selectedColor;
        else if (PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().distanceAttack && comp2men)
            tellers[4].color = comp2Button.colors.selectedColor;
        else tellers[4].color = comp1Button.colors.normalColor;
    }

}
