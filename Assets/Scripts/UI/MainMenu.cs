using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Variables referenciando las escenas a las que ir
    public string newGameScene, continueGameScene;

    //Referencia al bot�n de continuar
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //M�todo para el bot�n de continuar
    public void ContinueButton()
    {
        SceneManager.LoadScene(continueGameScene);
    }

    //M�todo para el bot�n de comenzar nueva partida
    public void NewGameButton()
    {
        SceneManager.LoadScene(newGameScene);
    }

    //M�todo para el bot�n de salir del juego
    public void ExitGameButton()
    {
        Application.Quit();
        Debug.Log("I'm out");
    }
}
