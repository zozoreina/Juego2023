using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    //Para poner la camara en negro
    bool shouldFadeToBlack, shouldFadeFromBlack;
    public Image fadeScreen;
    float fadeSpeed = .5f;

    public static FadeScreen sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    private void Start()
    {
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            //Cambiar la transparencia del color a opaco
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente opaco
            if (fadeScreen.color.a == 1f)
            {
                //Paramos de hacer fundido a negro
                shouldFadeToBlack = false;
            }
        }
        //Si hay que hacer fundido a transparente
        if (shouldFadeFromBlack)
        {
            //Cambiar la transparencia del color a transparente
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente transparente
            if (fadeScreen.color.a == 0f)
            {
                //Paramos de hacer fundido a transparente
                shouldFadeFromBlack = false;
            }
        }
    }

    //Método para hacer fundido a negro
    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }
    //Método para desacer el fundido
    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }
}
