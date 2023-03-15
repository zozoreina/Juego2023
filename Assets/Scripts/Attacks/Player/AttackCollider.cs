using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{    
    //Comrpueba si entra en contacto con el enemigo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealthController.sharedInstance.EnemyTakingDamage();
            gameObject.SetActive(false);
        }
    }
}
