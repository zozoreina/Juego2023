using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public int posToGo;

    public CameraController cameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            CameraController.sharedInstance.ChangeCurrentPos(posToGo);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            CameraController.sharedInstance.ChangeCurrentPos(0);
    }
}
