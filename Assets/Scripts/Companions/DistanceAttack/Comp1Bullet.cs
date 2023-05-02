using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comp1Bullet : MonoBehaviour
{
     public float moveSpeed;

    public GameObject explosionEffect;

    // Update is called once per frame
    void Update()
    {
        //Movemos a la bala en horizontal, usamos el LocalScale para saber a donde debe apuntar la bala (derecha o izquierda)
        transform.position += new Vector3(moveSpeed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealthController.sharedInstance.EnemyTakingDamage();
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}
