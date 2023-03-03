using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimiento del player
    public float moveSpeed;

    //Llamamiento al RigidBody
    private Rigidbody2D theRB;

    //Fuerza de salto
    public float jumpForce;

    //Variable para saber si el jugador está en el suelo
    private bool isGrounded;
    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //Variable para saber si puede dar doble salto
    bool doubleJump;

    //Singleton
    public static PlayerController sharedInstance;

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
        //Inicializamos  el rigidbody
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //El movimiento del player
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        //Para saber si estamos tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .5f, whatIsGround);

        //El salto
        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                //El salto en sí
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                //Para reiniciar el salto
                doubleJump = true;
            }
            //Si el jugador no está en el suelo
            else
            {
                if(doubleJump)
                {
                    //El jugador salta
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    //Y no puede volver a hacerlo
                    doubleJump = false;
                }
            }
        }
    }
}
