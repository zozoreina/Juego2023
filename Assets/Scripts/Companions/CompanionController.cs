using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    //Referencia a la posici�n del compa�ero
    Transform companion;

    //Velocidad de movimiento
    public float moveSpeed;

    //Referencia a la posici�n a la que se quieren mover los compa�eros
    public Transform[] objetivePos;

    // Start is called before the first frame update
    void Start()
    {
        companion = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        companion.position = Vector3.MoveTowards(transform.position, objetivePos[0].position, moveSpeed * Time.deltaTime);
    }
}
