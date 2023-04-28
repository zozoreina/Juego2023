using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movimiento del player
    public float moveSpeed;

    //Llamamiento al RigidBody
    private Rigidbody2D theRB;

    //Dash
    bool isDashing, canDash = true, canAirDash1, canAirDash2;
    public float dashForce, groundDashLength, airDashLenght, dashWaitTime;
    

    //Variable para saber si el jugador est� en el suelo
    public bool isGrounded;
    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //Variable para saber si puede dar doble salto
    bool doubleJump;
    //Fuerza de salto
    public float jumpForce;


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

    //Referencia al Menu de pausa
    public PauseMenu pauseMenu;
    //Referencia al Comanion Menu
    public CompanionsMenu companionMenu;

    //Variable para saber si el jugador se puede mover o no
    public bool canPlay;

    //Referencia a los compa�eros
    public GameObject companion1, companion2;

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
        //Para saber si el jugador puede moverse y jugar o no
        if (companionMenu.isConversationOn || pauseMenu.isPaused)
            canPlay = false;
        else canPlay = true;

        if (canPlay) 
        {

            //Para saber si estamos tocando el suelo
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .5f, whatIsGround);


            if (!isDashing)
            {            

                if (!isHurt)
                {
                
                    //El movimiento del player
                    theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                    //Giramos el sprite del jugador seg�n su direcci�n de movimiento
                    //Si el jugador se mueve a la izquierda
                    if (theRB.velocity.x < 0)
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

            

                //El salto y el ground dash
                if (isGrounded)
                {
                    //Saltar
                    doubleJump = true;
                    if (Input.GetButtonDown("Jump"))
                    {
                        //El salto en s�
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    }

                    canAirDash1 = true;
                    canAirDash2 = true;
                    //Para hacer dash
                    if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                    {
                        Dash(groundDashLength);
                    }

                    }
                else
                {
                    //Saltar
                    if (doubleJump && Input.GetButtonDown("Jump"))
                    {
                        //El jugador salta
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        //Y no puede volver a hacerlo
                        doubleJump = false;
                    }

                    //Dash
                    if (Input.GetKeyDown(KeyCode.LeftShift) && companion1.GetComponent<CompanionController>().airDash && !isGrounded && canAirDash1)
                    {
                        Dash(airDashLenght);
                        canAirDash1 = false;
                    }
                    else if(Input.GetKeyDown(KeyCode.LeftShift) && companion2.GetComponent<CompanionController>().airDash && !isGrounded && canAirDash2)
                    {
                        Dash(airDashLenght);
                        canAirDash2 = false;
                    }
                }

            
       

                //Si el contador de knockback se ha vaciado
                if (knockBackCounter <= 0)
                {
                    isHurt = false;
            
                }
                //Si el cotador no est� vacio
                else
                {
                    isHurt = true;
            
                    //Decrece el contador
                    knockBackCounter -= Time.deltaTime;
                    //si mira a la izquierda
                    if(!isLeft)
                    {
                        //Empujamos derecha
                        theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                    }
                    else
                    {
                        //aplicamos empuje a la derecha
                        theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
                    }
                }

            }
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

    //M�todo para hacer dash en el suelo
    public void Dash(float length)
    {
        StartCoroutine(DashCO(length));
    }

    //Corrutina para hacer dash
    public IEnumerator DashCO(float length)
    {
        isDashing = true;
        canDash = false;
        theRB.gravityScale = 0f;
        GetComponent<PlayerHealthController>().ChangeInvinvibleCounter(length);
        if(isLeft)
        {
            theRB.velocity = new Vector2(-dashForce, 0f);
        }
        else
        {
            theRB.velocity = new Vector2(dashForce, 0f);
        }
        yield return new WaitForSeconds(length);
        isDashing = false;
        theRB.gravityScale = 3f;
        yield return new WaitForSeconds(dashWaitTime);
        canDash = true;
    }


    //M�todos para activar y desactivar las ventanas de ataque del arma
    public void OpenOrCloseAttackWindow()
    {
        if (anim.GetBool("AttackWindow") == false)
            anim.SetBool("AttackWindow", true);
        else
            anim.SetBool("AttackWindow", false);
    }
    


}
