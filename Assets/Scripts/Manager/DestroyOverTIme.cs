using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTIme : MonoBehaviour
{
    //Tiempo de vida que va a tener el objeto asignado
    public float lifeTime;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
