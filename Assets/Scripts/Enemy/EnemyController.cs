using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //Fuerza de knockback
    public float knockBackForce;

    //Variable para saber a que dirección mira el enemigo
    private bool isLeft;

    //Referencia al RB
    private Rigidbody2D theRB;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(theRB.velocity.x < 0)
        {
            isLeft = true;
        }
        else if (theRB.velocity.x > 0)
        {
            isLeft = false;
        }
        
    }

    //Método para knockback enemigo
    public void EnemyKnockback()
    {
        if(isLeft)
        {
            theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
        }
    }

}
