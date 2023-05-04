using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttackHitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().anim.GetBool("AttackWindow"))
            EnemyHealthController.sharedInstance.EnemyTakingDamage();
        if (collision.CompareTag("Enemy") && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().anim.GetBool("AttackWindow"))
            EnemyHealthController.sharedInstance.EnemyTakingDamage();
    }
}
