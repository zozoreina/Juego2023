using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    //Referencia al RB
    private Rigidbody2D theRB;
    
    //

    // Start is called before the first frame update
    void Start()
    {
        //inicializamos el RB
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = Mathf.MoveTowards(transform.position, )
    }
}
