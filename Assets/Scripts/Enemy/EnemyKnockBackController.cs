using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBackController : MonoBehaviour
{
    //Fuerza de knockback
    public float knockBackForce;
    //Contador de knockback
    public float knockBackCounter;
    //Duración del knockback
    public float knockBackLength;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        knockBackCounter -= Time.deltaTime;
    }

    //Método para knockback enemigo
    public void EnemyKnockback()
    {
        if (PlayerController.sharedInstance.isLeft)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-knockBackForce, GetComponent<Rigidbody2D>().velocity.y);
            //theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(knockBackForce, GetComponent<Rigidbody2D>().velocity.y);
            //theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
        }
        knockBackCounter = knockBackLength;
    }
}
