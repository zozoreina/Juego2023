using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    //Referencia a la posición del compañero
    Transform companion;

    //Velocidad de movimiento
    public float moveSpeed;

    //Referencia a la posición a la que se quieren mover los compañeros
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
