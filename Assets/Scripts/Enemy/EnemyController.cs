using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //Fuerza de knockback
    public float knockBackForce;

    //Referencia al RB
    private Rigidbody2D theRB;

    //Punto donde de aparaci�n original del enemigo
    public Vector3 spawnPoint;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

    

}
