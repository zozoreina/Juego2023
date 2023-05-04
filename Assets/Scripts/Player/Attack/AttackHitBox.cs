using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && PlayerController.sharedInstance.canDmg)
        {
            Debug.Log("Le pegamos al enemigo");
            PlayerController.sharedInstance.canDmg = false;
            EnemyHealthController.sharedInstance.EnemyTakingDamage();
        }
    }
}
