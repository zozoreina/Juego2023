using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    //Referencia al rigid body del compañero
    Rigidbody2D theRB;

    //Velocidad de movimiento
    public float moveSpeed;

    //Referencia a la posición a la que se quieren mover los compañeros
    public Transform[] objetivePos;
    //Referencia al objetivo actual de la posición
    int currentPos;

    //Dirección en la que mira el compañero y Saber si está tocando el suelo
    bool isLeft, isGrounded;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //Referencia al punto del suelo
    public Transform groundPoint;

    //Booleanas para compenetrar con player
    bool airDash, chargedAttack, forthAttack, airAttack, distanceAttack;

    //Singleton
    public static CompanionController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 1f, whatIsGround);

        //Para saber si mira a la izquierda o la derecha
        if (theRB.position.x < objetivePos[currentPos].position.x)
            isLeft = false;
        else if (theRB.position.x > objetivePos[currentPos].position.x)
            isLeft = true;

        //El compañero se mueve hacia el objetivo
        if (Mathf.Abs(theRB.position.x - objetivePos[currentPos].position.x) > .1f)
        {
            if (isLeft)
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            else if (!isLeft)
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else if (Mathf.Abs(theRB.position.x - objetivePos[currentPos].position.x) < .1f)
            theRB.velocity = new Vector2(0, theRB.velocity.y);

        if (theRB.position.y < objetivePos[currentPos].position.y -.1f && isGrounded) 
        {
            theRB.velocity = new Vector2(theRB.velocity.x, moveSpeed);          
        }



    }
    
}
