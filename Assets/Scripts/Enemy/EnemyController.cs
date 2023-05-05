using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Fuerza de knockback
    public float knockBackForce;
    //Contador de knockback
    float knockBackCounter;
    //Duraci�n del knockback
    public float knockBackLength;

    //Referencia al RB
    private Rigidbody2D theRB;

    //Punto donde de aparaci�n original del enemigo
    public Vector3 spawnPoint;

    //Puntos entre los que patrulla el enemigo
    public Transform[] patrolPoints;
    //Variable para saber en que punto del array estamos
    int currentPoint;
    //Velocidad del enemigo
    public float moveSpeed;

    //Variable donde guardar la distancia m�xima para atacar al player y velocidad del ataque y velocidad de persecuci�n
    public float distanceToAttack, attackSpeed, chaseSpeed;
    //Variable para saber si est� atacando
    bool isAttacking;

    //Objetivo a atacar 
    Vector3 target;
    //Tiempo entre ataques
    public float timeBetweenAttacks;
    float tBACounter;

    //Singleton
    public static EnemyController sharedInstance;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el RB
        theRB = GetComponent<Rigidbody2D>();

        //Guardamos punto de aparici�n
        spawnPoint = transform.position;

        //Sacamos los puntos de patrulla del Enemigo
        foreach (Transform p in patrolPoints)
            p.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        knockBackCounter -= Time.deltaTime;
        if (knockBackCounter <= 0)
        {
            
            //Si el contador de ataque est� lleno hacemos que se vac�e el contador
            if (tBACounter > 0)
                tBACounter -= Time.deltaTime;
            //Si el contador de ataque ya est� vac�o
            else
            {
                //Si el enemigo no ha visto al jugador a�n
                if (Vector3.Distance(transform.position, PlayerController.sharedInstance.transform.position) > distanceToAttack && target == Vector3.zero)
                {
                    //Movemos al enemigo
                    transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed * Time.deltaTime);
                    //Cuando el enemigo llegue a su destino
                    if (Vector3.Distance(transform.position, patrolPoints[currentPoint].position) < .1f)
                    {
                        currentPoint++;
                        if (currentPoint >= patrolPoints.Length)
                            currentPoint = 0;
                    }

                    //Si el enemigo llega al punto m�s a la izquierda lo rotamos para que cambie de direcci�n
                    //Esto es del SpriteRenderer ya lo har�
                }
                //Si el jugador puede ser atacado
                else if (!isAttacking)
                {
                    //Si el objetivo de ataque est� vac�o metemos al jugador
                    if (target == Vector3.zero || target != Vector3.zero)
                        target = PlayerController.sharedInstance.transform.position;

                    if (Vector3.Distance(transform.position, target) < distanceToAttack)
                    {
                        //El enemigo se lanza a por el jugador
                        //transform.position = Vector3.MoveTowards(transform.position, target, attackSpeed * Time.deltaTime);
                        Attack();
                        
                    }
                    else
                    {
                        Vector2 chaseDirection = new Vector2(PlayerController.sharedInstance.transform.position.x - transform.position.x, PlayerController.sharedInstance.transform.position.y - transform.position.y);
                        chaseDirection.Normalize();
                        //El enemigo persigue al jugador
                        theRB.velocity = chaseDirection * chaseSpeed;
                    }
                    //Si el enemigo ha llegado al target 
                    if (Vector3.Distance(transform.position, target) < .1f)
                        tBACounter = timeBetweenAttacks;
                }



            }
        }
    }

    //M�todo para hacer da�o al jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerHealthController.sharedInstance.DealWithDamage(34);
        }
    }

    //M�todo para knockback enemigo
    public void EnemyKnockback()
    {
        if(PlayerController.sharedInstance.isLeft)
        {
            theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
        }
        knockBackCounter = knockBackLength;
    }

    //M�todo por el que el enemigo ataca al jugador
    public void Attack()
    {
        StartCoroutine(AttackCo());
    }
    //Corrutina de ataque
    public IEnumerator AttackCo()
    {
        Vector2 attackDirection = new Vector2(PlayerController.sharedInstance.transform.position.x - transform.position.x, PlayerController.sharedInstance.transform.position.y - transform.position.y);
        attackDirection.Normalize();
        theRB.velocity = attackDirection * attackSpeed;
        isAttacking = true;
        yield return new WaitForSeconds(2f);
        theRB.velocity = Vector2.zero;
        isAttacking = false;
    }


}
