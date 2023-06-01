using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    //Lista de estados por los que puede pasar el jefe final (Máquina de estados)
    public enum bossStates { shooting, moving };
    //Creamos una referencia al estado actual del jefe final
    public bossStates currentState;
    //referencia al sr
    SpriteRenderer theSR;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Movement")]
    //Puntos entre los que se mueve el enemigo
    public Transform[] points;
    int currentPoint;
    int chivato;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Shooting")]
    //Posicion del jugador 
    Vector2 target;
    Vector2 targetPointer;
    //Referencia a los proyectiles del enemigo
    public GameObject bullet;
    //Referencia al punto de arma y punto de disparo
    public Transform weapon, firePoint;
    float angle;
    //Tiempo entre disparos
    public float timeBetweenShots;
    //Contador de tiempo entre disparos
    private float shotCounter;

    //Atributo de las variables que genera un encabezado en el editor de Unity

    int prevHealth;

    //Posición del Boss
    public Transform theBoss;



    // Start is called before the first frame update
    void Start()
    {
        currentState = bossStates.shooting;
        theSR = GetComponent<SpriteRenderer>();
        prevHealth = EnemyHealthController.sharedInstance.maxHealth;
        currentPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //En base a los cambios en el valor del enum generamos los cambios de estado
        switch (currentState)
        {
            //En el caso en el que currentState = 0
            case bossStates.shooting:

                //Hacemos decrecer el contador entre disparos
                shotCounter -= Time.deltaTime;
                //Asignamos el target
                target = PlayerController.sharedInstance.transform.position;
                //Creamos un apuntador al target y lo normalizamos
                targetPointer = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
                targetPointer.Normalize();
                angle = Vector2.Angle(targetPointer, transform.right);
                //Cambiamos la rotación del arma
                //weapon.rotation = Quaternion.AngleAxis(angle, targetPointer);
                weapon.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

                //Si el contador de tiempo entre disparos se ha vaciado
                if (shotCounter <= 0)
                {
                    //Reiniciamos el contador de tiempo entre disparos
                    shotCounter = timeBetweenShots;
                    //Instanciamos la bala pero en una nueva referencia cada vez
                    var newBullet = Instantiate(bullet, firePoint.position, weapon.rotation);
                    //Como cada bala estará referenciada (será única) puedo aplicarle los cambios que queramos
                    //En este caso le diré a cada bala hacia donde debe apuntar según hacia donde mira el jefe final
                    newBullet.transform.localScale = theBoss.localScale;
                }

                if (points[0] || points[1])
                    theSR.flipX = true;
                else theSR.flipX = false;

                if (EnemyHealthController.sharedInstance.currentHealth < prevHealth)
                {
                    prevHealth = EnemyHealthController.sharedInstance.currentHealth;
                    currentState = bossStates.moving;
                }

               
                if (points[chivato] != points[currentPoint])
                {

                    points[chivato] = points[currentPoint];
                    transform.position = points[chivato].position;
                }

                break;


            //En el caso en el que currentState = 2
            case bossStates.moving:
                {
                    if (currentPoint == 0)
                        currentPoint = 2;
                    else if (currentPoint == 1)
                        currentPoint = 3;
                    else if (currentPoint == 2)
                        currentPoint = 1;
                    else currentPoint = 0;

                    currentState = bossStates.shooting;
                }
                break;
        }
    }
}
