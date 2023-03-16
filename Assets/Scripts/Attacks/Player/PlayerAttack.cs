using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        this.gameObject.transform.position = PlayerController.sharedInstance.attackPoint.transform.position;
        if (PlayerController.sharedInstance.isLeft)
        {            
            gameObject.transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
        else if(!PlayerController.sharedInstance.isLeft)
        {
            gameObject.transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
    }

   

    
}
