using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Referencia al attackPoint
    public GameObject attackPoint;

    //Singleton
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
            
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(PlayerController.sharedInstance.isLeft)
        {
            transform.position = attackPoint.transform.position;
        }
        else if(!PlayerController.sharedInstance.isLeft)
        {
            
        }
    }

   

    public void DealDamageToEnemy()
    {
        EnemyHealthController.sharedInstance.EnemyTakingDamage();
    }

}
