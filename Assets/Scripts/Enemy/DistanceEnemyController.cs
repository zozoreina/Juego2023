using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyController : MonoBehaviour
{

    //Fuerza de knockback
    public float knockBackForce;
    //Contador de knockback
    float knockBackCounter;
    //Duración del knockback
    public float knockBackLength;

    //Referencia al RB
    private Rigidbody2D theRB;

    //Punto donde de aparación original del enemigo
    public Vector3 spawnPoint;

    //Variable donde guardar la distancia máxima para atacar al player y velocidad del ataque y velocidad de persecución
    public float distanceToAttack, attackSpeed, chaseSpeed;
    //Variable para saber si está atacando
    bool isAttacking;

    //Objetivo a atacar 
    Vector3 target;
    //Tiempo entre ataques
    public float timeBetweenAttacks;
    float tBACounter;

    //Singleton
    public static DistanceEnemyController sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el RB
        theRB = GetComponent<Rigidbody2D>();

        //Guardamos punto de aparición
        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        knockBackCounter -= Time.deltaTime;
        //Si el contador de knockback no está vacío hacemos que se vacíe
        if (knockBackCounter <= 0)
        {
            //Si el contador de tiempo entre ataques no está vacío hacemos que se vacíe
            if (tBACounter > 0)
                tBACounter -= Time.deltaTime;
            //Una vez vacío
            else
            {
                //Si detecta al jugador
                if ()
            }
        }
    }
}
