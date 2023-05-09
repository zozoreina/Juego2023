using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Array de posiciones a las que ir
    public Transform[] targets;
    //Para obtener la posici�n de la c�mara
    public int target;

    //Variables donde guardar las posiciones 
    private Vector2 lastPos;
    //Varibales para posiciones m�ximas de cam
    public float minHeight;

    //Variable para saber si el jugador est� ascendiendo de manera que necesita la c�mara (principalmente para usar en ascensores)
    bool goingUp;

    //Referencia al objeto donde se guardan las zonas donde se puede mover la camara en Y
    public GameObject yMovementZones;

    public static CameraController sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Al empezar el juego la �ltima posici�n ser� el jugador
        //lastPos = targets[0].transform.position;
        //Soltamos a los hijos
        for (int i = 1; i < targets.Length; i++)
            targets[i].transform.parent = null;
        yMovementZones.transform.parent = null;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == 0)
        {
            //Hacemos que si la camara est� persiguiendo al jugador, lo persiga en eje Y dependiendo de si est� ascendiendo o se mantiene en el nivel
            if (goingUp)
                //La cam sigue al jugador sin cambiar en z
                transform.position = new Vector3(targets[target].position.x, Mathf.Clamp(targets[target].transform.position.y, minHeight, Mathf.Infinity), transform.position.z);
            else transform.position = new Vector3(targets[target].position.x, transform.position.y, transform.position.z);
        }
        else transform.position = Vector3.MoveTowards(transform.position, targets[target].position, .5f);

        //Varibale para saber cuanto hay que moverse
        //float amountToMoveX = transform.position.x - lastPos.x;
        //float amountToMoveY = transform.position.y - lastPos.y;
        //Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        //lastPos = transform.position;


    }
    
    //M�todo para cambiar la posici�n a la que mandar la c�mara
    public void ChangeCurrentPos(int newpos)
    {
        target = newpos;
    }
    //M�todo para cambiar el zoom de la c�mara
    public void ChangeCurrentZoom(float newZoom)
    {
        GetComponent<Camera>().orthographicSize = newZoom;
    }
    //M�todo para cambiar si la c�mara se bloquea en Y
    public void AllowYMovement()
    {
        goingUp = true;
    }
    public void LimitYMovement()
    {
        goingUp = false;
    }
}
