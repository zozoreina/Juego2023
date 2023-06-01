using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    //Velocidad de la bala
    public float moveSpeed;
    //Referencai al rigidBody
    Rigidbody2D theRB;

    Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        target = PlayerController.sharedInstance.transform.position - transform.position;
        target.Normalize();
        //theRB.velocity = new Vector2(PlayerController.sharedInstance.transform.position.x , PlayerController.sharedInstance.transform.position.y) * moveSpeed;
    }

    private void Update()
    {
        theRB.velocity = target * moveSpeed;
    }

    //Método por el que la bala impacta
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            PlayerHealthController.sharedInstance.DealWithDamage(50);
        
    }
}
