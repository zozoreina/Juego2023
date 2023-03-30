using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionsMenu : MonoBehaviour
{
    //Referencia al panel de aviso para hablar con los compañeros
    public GameObject infoPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(PlayerController.sharedInstance.transform.position, CompanionController.sharedInstance.transform.position) < 3 && PlayerController.sharedInstance.isLeft /*ME queda por añadir que no puede estar en combate*/)
        {
            infoPanel.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.E))
            {
                SetGameToConversation();
            }
        }
        else
            infoPanel.SetActive(false);
    }

    //Método por el que comienza la conversación con los compañeros
    public void SetGameToConversation()
    {
        Time.timeScale = 0f;

    }
}
