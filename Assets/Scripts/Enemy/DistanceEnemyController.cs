using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceEnemyController : MonoBehaviour
{
    //Variable donde guardar la distancia máxima para atacar al player y velocidad del ataque y velocidad de persecución
    public float distanceToAttack;

    //Objetivo a atacar 
    Vector3 target;
    //Tiempo entre ataques
    public float timeBetweenAttacks;
    float tBACounter;

    //Referencia a la bala
    public GameObject bullet;
    //Referencia al punto de arma y punto de disparo
    public Transform weapon, firepPoint;

    //Singleton
    public static DistanceEnemyController sharedInstance;
    private void Awake()
    {
        if (sharedInstance == null)
            sharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Asignamos el target
        target = PlayerController.sharedInstance.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Creamos un apuntador al target y lo normalizamos
        Vector2 targetPointer = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        targetPointer.Normalize();
        float angle = Vector2.Angle(targetPointer, transform.forward);
        //Cambiamos la rotación del arma
        weapon.rotation = Quaternion.AngleAxis(angle, targetPointer);

        //Si el contador de tiempo entre ataques no está vacío hacemos que se vacíe
        if (tBACounter > 0)
            tBACounter -= Time.deltaTime;
        //Una vez vacío
        else
        {
            //Si detecta al jugador
            if (Vector3.Distance(transform.position, target) < distanceToAttack)
            {
                //Attacking player
                Instantiate(bullet, firepPoint.position, weapon.rotation);
                tBACounter = timeBetweenAttacks;
            }

        }
        
    }
}
