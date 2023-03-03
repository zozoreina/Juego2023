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

    //Variable para saber si el jugador est� en el suelo
    private bool isGrounded;
    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //Variable para saber si puede dar doble salto
    bool doubleJump;
    //Detectamos hacia donde mira el juador
    public bool isLeft;

    //Referencia al animador
    private Animator anim;
    //Referencia al SR del jugador
    SpriteRenderer theSR;


    //Variables para el contador de tiempo del KnockBack
    public float knockBackLength, knockBackForce; //Valor que tendr� el contador de KnockBack
    private float knockBackCounter; //Contador de KnockBack
    public bool isHurt;

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

        //Inicia�izamos el animator
        anim = GetComponent<Animator>();

        //Inicializamos el spriteRenderer
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de knockback se ha vaciado
        if (knockBackCounter <= 0)
        {
            isHurt = false;

            //El movimiento del player
            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

            //Para saber si estamos tocando el suelo
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .5f, whatIsGround);

            //El salto
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    //El salto en s�
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    //Para reiniciar el salto
                    doubleJump = true;
                }
                //Si el jugador no est� en el suelo
                else
                {
                    if (doubleJump)
                    {
                        //El jugador salta
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        //Y no puede volver a hacerlo
                        doubleJump = false;
                    }
                }
            }

            //Giramos el sprite del jugador seg�n su direcci�n de movimiento
            //Si el jugador se mueve a la izquierda
            if(theRB.velocity.x < 0)
            {
                //No cambiamos el sprite
                theSR.flipX = false;
                //El jugador mira a la izquierda 
                isLeft = true;
            }
            else if (theRB.velocity.x > 0)
            {
                theSR.flipX = true;
                isLeft = false;
            }
        }
        //Si el cotador no est� vacio
        else
        {
            isHurt = true;
            //Decrece el contador
            knockBackCounter -= Time.deltaTime;
            //si mira a la izquierda
            if(!theSR.flipX)
            {
                //Empujamos derecha
                theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            }
            else
            {
                //aplicamos empuje a la derecha
                theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
            }
        }

        //Para que respawne si se cae del mapa
        if(this.gameObject.transform.position.y <= -10)
        {
            LevelManager.sharedInstance.respawnPlayer();
        }

        //Mantener animciones al final del update
        //Animaciones del jugador
        //Cambiamos el valor del parametro del animator "movespeed", dependiendo del valor de X de la velocidad de RB
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isHurt", isHurt);

    }

    //M�todo para gestionar el knockback 
    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }

}
