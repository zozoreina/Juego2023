using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Para obtener la posición de la cámara
    public Transform target;

    //Variables donde guardar las posiciones 
    private Vector2 lastPos;
    //Varibales para posiciones máximas de cam
    public float minHeight;
    

    // Start is called before the first frame update
    void Start()
    {
        //al empezar el juego la última posición será el jugador
        lastPos = target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //La cam sigue al jugador sin cambiar en z
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        //Varibale para saber cuanto hay que moverse
        float amountToMoveX = transform.position.x - lastPos.x;
        float amountToMoveY = transform.position.y - lastPos.y;

        lastPos = transform.position;

    }
}
