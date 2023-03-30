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
    bool isDashing, canDash = true;
    public float dashForce, dashLength, dashWaitTime;

    //Attack
    bool isAttacking;
    //Para cada ataque un bool
    bool[] canAttack = new bool[4];
    //Timepo de espera entre ataques
    public float attackWaitTime;
    //Referencia al punto del que salen los ataques
    public GameObject attackPoint;
    //Referencias a los ataques
    public GameObject attack1, attack3;
    //Duración de los ataques
    public float[] attacksDurations;

    

    //Variable para saber si el jugador está en el suelo
    private bool isGrounded;
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
    public float knockBackLength, knockBackForce; //Valor que tendrá el contador de KnockBack
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

        //Iniciañizamos el animator
        anim = GetComponent<Animator>();

        //Inicializamos el spriteRenderer
        theSR = GetComponent<SpriteRenderer>();

        //Ponemos el primer booleano de ataque en verdadero para que pueda emepezar la secuencia de ataques
        canAttack[0] = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Para saber si estamos tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .5f, whatIsGround);

        if (!isDashing)
        {
            if (!isAttacking)
            {

                if (!isHurt)
                {
                
                    //El movimiento del player
                    theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                     //Giramos el sprite del jugador según su dirección de movimiento
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

                    if (Input.GetButtonDown("Fire1") && canAttack[0])
                    {
                        Attack();
                    }
                }

            

                //El salto y el ground dash
                if (isGrounded)
                {
                    //Saltar
                    doubleJump = true;
                    if (Input.GetButtonDown("Jump"))
                    {
                        //El salto en sí
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    }

                    //Para hacer dash
                    if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                    {
                        GroundDash();
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
                }

            }
       

            //Si el contador de knockback se ha vaciado
            if (knockBackCounter <= 0)
            {
                isHurt = false;
            
            }
            //Si el cotador no está vacio
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
        

        //Mantener animciones al final del update
        //Animaciones del jugador
        //Cambiamos el valor del parametro del animator "movespeed", dependiendo del valor de X de la velocidad de RB
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isHurt", isHurt);

    }

    //Método para gestionar el knockback 
    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }

    //Método para hacer dash en el suelo
    public void GroundDash()
    {
        StartCoroutine(GroundDashCO());
    }

    //Corrutina para hacer dash
    public IEnumerator GroundDashCO()
    {
        isDashing = true;
        canDash = false;
        theRB.gravityScale = 0f;
        GetComponent<PlayerHealthController>().ChangeInvinvibleCounter(dashLength);
        if(isLeft)
        {
            theRB.velocity = new Vector2(-dashForce, 0f);
        }
        else
        {
            theRB.velocity = new Vector2(dashForce, 0f);
        }
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
        theRB.gravityScale = 3f;
        yield return new WaitForSeconds(dashWaitTime);
        canDash = true;
    }

    //Método para atacar
    public void Attack()
    {
        StartCoroutine(AttackCo());
    }

    //Corrutina de ataque
    IEnumerator AttackCo()
    {
        isAttacking = true;
        canAttack[0] = false;
        Instantiate(attack1, transform.GetChild(1).position, transform.GetChild(1).rotation);
        yield return new WaitForSeconds(attacksDurations[0]);
        canAttack[1] = true;
        for (float i = 0; i < attackWaitTime; i += Time.deltaTime)
        {
            if(Input.GetButtonDown("Fire1") && canAttack[1])
            {
                canAttack[1] = false;
                Instantiate(attack1, transform.GetChild(1).position, transform.GetChild(1).rotation);
                yield return new WaitForSeconds(attacksDurations[0]);
                canAttack[2] = true;
                for (float c = 0; c < attackWaitTime; c += Time.deltaTime)
                {
                    if (Input.GetButtonDown("Fire1") && canAttack[2])
                    {
                        canAttack[2] = false;
                        Instantiate(attack3, transform.GetChild(1).position, transform.GetChild(1).rotation);
                        yield return new WaitForSeconds(attacksDurations[1]);
                        
                    }
                }
            }
        }
        isAttacking = false;
        yield return new WaitForSeconds(attackWaitTime);
        canAttack[0] = true;
    }

}
