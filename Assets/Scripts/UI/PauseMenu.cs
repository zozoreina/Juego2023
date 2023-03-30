using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Varibales para ir a las escenas que queremos
    string MainMenu = "MainMenu_Scene";

    //Referencia al GO del menu de pausa
    public GameObject pauseMenu;

    //Variable para saber si el juego est� pausado
    public bool isPaused;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Para pausar el juego
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseUnpause();
    }

    //M�todo para pausar o despausar el juego
    public void PauseUnpause()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //M�todo para el bot�n de volver al men� principal
    public void MainMenuButton()
    {
        SceneManager.LoadScene(MainMenu);
        Time.timeScale = 1f;
    }

    //M�todo para el bot�n de salir del juego
    public void ExitGameButton()
    {
        Application.Quit();
        Debug.Log("We outa here");
    }
}
