using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //Velocidad de la bala
    public float moveSpeed;
    //Referencai al rigidBody
    Rigidbody2D theRB;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        theRB.velocity = new Vector2(PlayerController.sharedInstance.transform.position.x , PlayerController.sharedInstance.transform.position.y) * moveSpeed;
    }

    private void Update()
    {
        
    }


}
