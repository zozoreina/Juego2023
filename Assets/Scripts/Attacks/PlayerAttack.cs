using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //LLamamiento al RigidBody
    private Rigidbody2D theRB;
    //Lamamiento al Animator
    private Animator anim;


    public static PlayerAttack sharedInstance;

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
        theRB = GetComponentInChildren<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            DealDamageToEnemy();
        }
    }

    public void DealDamageToEnemy()
    {
        EnemyHealthController.sharedInstance.EnemyTakingDamage();
    }

}
