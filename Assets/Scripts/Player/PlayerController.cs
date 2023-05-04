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
    

    //Variable para saber si el jugador está en el suelo
    public bool isGrounded;
    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //Fuerza de salto
    public float jumpForce;


    //Detectamos hacia donde mira el juador
    public bool isLeft;

    //Variables para el contador de tiempo del KnockBack
    public float knockBackLength, knockBackForce; //Valor que tendrá el contador de KnockBack
    private float knockBackCounter; //Contador de KnockBack
    public bool isHurt;

    //Variable para saber si el jugador se puede mover o no
    public bool canPlay;

    //Variable para saber si el jugador puede hacer daño
    public bool canDmg;

    //Referencia al animador
    private Animator anim;
    //Referencia al SR del jugador
    SpriteRenderer theSR;

    //Referencia al Menu de pausa
    public PauseMenu pauseMenu;
    //Referencia al Comanion Menu
    public CompanionsMenu companionMenu;   

    //Referencia a los compañeros
    public GameObject companion1, companion2;

    //Variable para saber si el compañero ha hecho su ataque a distancia o no
    bool comp1DistanceAttack, comp2DistanceAttack;
    float comp1DistanceAttackCounter, comp2DistanceAttackCounter;
    public GameObject Comp1Bullet, Comp2Bullet;

    //Variable para saber si el compañero puede atacar en el aire
    bool comp1AirAttack, comp2AirAttack;
    float comp1AirAttackCounter, comp2AirAttackCounter;
    
    //Variables para saber si el compañero te puede ayudar a hacer doble salto
    //Variable para saber si puede dar doble salto
    bool doubleJump1, doubleJump2;

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
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            //Para los ataques aereos de los aliados
            comp1AirAttackCounter -= Time.deltaTime;
            comp2AirAttackCounter -= Time.deltaTime;

            if (comp1AirAttackCounter <= 0)
                comp1AirAttack = true;
            else comp1AirAttack = false;
            if (comp2AirAttackCounter <= 0)
                comp2AirAttack = true;
            else comp2AirAttack = false;

            //Para los ataques a distancia de los aliados
            comp1DistanceAttackCounter -= Time.deltaTime;
            comp2DistanceAttackCounter -= Time.deltaTime;

            if (comp1DistanceAttackCounter <= 0)
                comp1DistanceAttack = true;
            else comp1DistanceAttack = false;

            if (comp2DistanceAttackCounter <= 0)
                comp2DistanceAttack = true;
            else comp2DistanceAttack = false;

            if (companion1.GetComponent<CompanionController>().distanceAttack && Input.GetButtonDown("Fire2") && comp1DistanceAttack)
            {
                comp1DistanceAttackCounter = 5f;
                companion1.GetComponent<CompanionController>().anim.SetTrigger("DistanceAttack");
                var Bullet = Instantiate(Comp1Bullet, companion1.GetComponent<CompanionController>().BulletPoint1.position, companion1.GetComponent<CompanionController>().BulletPoint1.rotation);
                Bullet.transform.localScale = transform.localScale;
                Debug.Log("Ataque a distancia 1");
            }
            if (companion2.GetComponent<CompanionController>().distanceAttack && Input.GetButtonDown("Fire2") && comp2DistanceAttack)
            {
                comp2DistanceAttackCounter = 2f;
                companion2.GetComponent<CompanionController>().anim.SetTrigger("DistanceAttack");
                var Bullet = Instantiate(Comp2Bullet, companion2.GetComponent<CompanionController>().BulletPoint2.position, companion2.GetComponent<CompanionController>().BulletPoint2.rotation);
                Bullet.transform.localScale = transform.localScale;
                Debug.Log("Ataque a distancia 2");
            }

            if (!isDashing)
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
                        transform.localScale = new Vector3(-1f, 1.5f, 1f);
                    }
                    else if (theRB.velocity.x > 0)
                    {
                        theSR.flipX = true;
                        isLeft = false;
                        transform.localScale = new Vector3(1f, 1.5f, 1f);
                    }

                    //Para habilitar la capacidad de atacar del jugador
                    if (anim.GetBool("AttackWindow"))
                    {
                        canDmg = true; Debug.Log("Preparado para hacer daño");
                    }
                    else canDmg = false;

                }

            

                //El salto y el ground dash
                if (isGrounded)
                {
                    //Saltar
                    doubleJump1 = true;
                    doubleJump2 = true;
                    if (Input.GetButtonDown("Jump"))
                    {
                        //El salto en sí
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
                    if (doubleJump1 && companion1.GetComponent<CompanionController>().doubleJump && Input.GetButtonDown("Jump"))
                    {
                        companion1.GetComponent<CompanionController>().anim.SetBool("DoubleJump", true);
                        //El jugador salta
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        //Y no puede volver a hacerlo
                        doubleJump1 = false;
                    }
                    else if (doubleJump2 && companion2.GetComponent<CompanionController>().doubleJump && Input.GetButtonDown("Jump"))
                    {
                        companion2.GetComponent<CompanionController>().anim.SetBool("DoubleJump", true);
                        //El jugador salta
                        theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                        //Y no puede volver a hacerlo
                        doubleJump2 = false;
                    }

                    //AirDash
                    if (Input.GetKeyDown(KeyCode.LeftShift) && companion1.GetComponent<CompanionController>().airDash && !isGrounded && canAirDash1)
                    {
                        companion1.GetComponent<CompanionController>().anim.SetBool("AirDash", true);
                        Dash(airDashLenght);
                        canAirDash1 = false;
                    }
                    else if(Input.GetKeyDown(KeyCode.LeftShift) && companion2.GetComponent<CompanionController>().airDash && !isGrounded && canAirDash2)
                    {
                        companion2.GetComponent<CompanionController>().anim.SetBool("AirDash", true);
                        Dash(airDashLenght);
                        canAirDash2 = false;
                    }

                    //Ataque en el aire
                    if (companion1.GetComponent<CompanionController>().airAttack && comp1AirAttack && Input.GetButtonDown("Fire1"))
                    {
                        comp1AirAttackCounter = 2f;
                        companion1.GetComponent<CompanionController>().anim.SetBool("AirAttack", true);
                        Debug.Log("AirAttack1");
                    }
                    if (companion2.GetComponent<CompanionController>().airAttack && comp2AirAttack && Input.GetButtonDown("Fire1"))
                    {
                        comp2AirAttackCounter = 2f;
                        companion2.GetComponent<CompanionController>().anim.SetBool("AirAttack", true);
                        Debug.Log("AirAttack1");
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


    //Métodos para activar y desactivar las ventanas de ataque del arma
    public void TurnOnAttackWindow()
    {
        anim.SetBool("AttackWindow", true);
    }
    public void TurnOffAttackWindow()
    {
        anim.SetBool("AttackWindow", false);
    }
}
