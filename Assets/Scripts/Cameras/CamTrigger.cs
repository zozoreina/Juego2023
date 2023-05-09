using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    //Variable con la que sabemos a que posici�n del array del camera controller hay que mandar la c�mara.
    public int posToGo;
    //Varibale donde guardar los tama�os de las c�maras
    public float zoomDistance;
    //Variables para saber si la c�mara tiene que moverse a la posici�n, hacer zoom y moverse en Y
    public bool movePos, zoomPos, yMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && movePos)
            CameraController.sharedInstance.ChangeCurrentPos(posToGo);
        if (collision.CompareTag("Player") && zoomPos)
            CameraController.sharedInstance.ChangeCurrentZoom(zoomDistance);
        if (collision.CompareTag("Player") && yMovement)
            CameraController.sharedInstance.AllowYMovement();

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            CameraController.sharedInstance.ChangeCurrentPos(0);
        if (collision.CompareTag("Player"))
            CameraController.sharedInstance.ChangeCurrentZoom(9);
        if (collision.CompareTag("Player"))
            CameraController.sharedInstance.LimitYMovement();
    }
}
