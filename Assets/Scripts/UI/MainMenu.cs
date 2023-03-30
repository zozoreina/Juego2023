using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Variables referenciando las escenas a las que ir
    public string newGameScene, continueGameScene;

    //Referencia al botón de continuar
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para el botón de continuar
    public void ContinueButton()
    {
        SceneManager.LoadScene(continueGameScene);
    }

    //Método para el botón de comenzar nueva partida
    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameScene);
    }

    //Método para el botón de salir del juego
    public void ExitGameButton()
    {
        Application.Quit();
        Debug.Log("I'm out");
    }
}
