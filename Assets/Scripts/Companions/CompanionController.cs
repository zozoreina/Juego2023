using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    //Referencia al rigid body del compa�ero
    Rigidbody2D theRB;

    //Velocidad de movimiento
    public float moveSpeed;

    //Referencia a la posici�n a la que se quieren mover los compa�eros
    public Transform[] objetivePos;
    //Referencia al objetivo actual de la posici�n
    int currentPos;

    //Direcci�n en la que mira el compa�ero y Saber si est� tocando el suelo
    bool isLeft, isGrounded;
    //Variable para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //Referencia al punto del suelo
    public Transform groundPoint;

    //Booleanas para compenetrar con player
    public bool airDash, chargedAttack, attackCombo, airAttack, distanceAttack;
    //Array para guardar que booleanas est�n activas
    string[] activeAbilities = new string[3];

    //Singleton
    public static CompanionController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, 1f, whatIsGround);

        //Para saber si mira a la izquierda o la derecha
        if (theRB.position.x < objetivePos[currentPos].position.x)
            isLeft = false;
        else if (theRB.position.x > objetivePos[currentPos].position.x)
            isLeft = true;

        //El compa�ero se mueve hacia el objetivo
        if (Mathf.Abs(theRB.position.x - objetivePos[currentPos].position.x) > .1f)
        {
            if (isLeft)
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            else if (!isLeft)
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else if (Mathf.Abs(theRB.position.x - objetivePos[currentPos].position.x) < .1f)
            theRB.velocity = new Vector2(0, theRB.velocity.y);

        if (theRB.position.y < objetivePos[currentPos].position.y -.1f && isGrounded) 
        {
            theRB.velocity = new Vector2(theRB.velocity.x, moveSpeed);          
        }



    }

    //M�todo para el bot�n Air Dash
    public void AirDashButton()
    {
        if (airDash)
        {
            airDash = false;
            DeactivateAbilities("airDash");
        }
        else
        {
            airDash = true;
            ActivateAbility("airDash");
        }
    }

    //M�todo para le bot�n Charged Attack
    public void ChargedAttackButton()
    {
        if(chargedAttack)
        { 
            chargedAttack = false;
            DeactivateAbilities("chargedAttack");
        }
        else
        {
            chargedAttack = true;
            ActivateAbility("chargedAttack");
        }
    }

    //M�todo apra el bot�n Attack Combo
    public void AttackComboButton()
    {
        if(attackCombo)
        {
            attackCombo = false;
            DeactivateAbilities("attackCombo");
        }
        else
        {
            attackCombo = true;
            ActivateAbility("attackCombo");
        }
    }

    //M�todo para el bot�n Air Attack
    public void AirAttackButton()
    {
        if(airAttack)
        {
            airAttack = false;
            DeactivateAbilities("airAttack");
        }
        else
        {
            airAttack = true;
            ActivateAbility("airAttack");
        }
    }

    //M�todo para el bot�n Distance Attack
    public void DistanceAttackButton()
    {
        if(distanceAttack)
        {
            distanceAttack = false;
            DeactivateAbilities("distanceAttack");
        }
        else
        {
            distanceAttack = true;
            ActivateAbility("distanceAttack");
        }
    }

    //M�todo para comprobar si el booleano objetivo est� dentro del array
    public void ActivateAbility(string ability)
    {
        for (int a = 0; a < activeAbilities.Length; a++)
        {
            if (activeAbilities[a] == null && !IsAbilityIn(ability))
                activeAbilities[a] = ability;
            else if (a == activeAbilities.Length - 1 && !IsAbilityIn(ability))
            {
                switch (activeAbilities[0])
                {
                    case "airDash":
                        airDash = false;
                        break;
                    case "chargedAttack":
                        chargedAttack = false;
                        break;
                    case "attackCombo":
                        attackCombo = false;
                        break;
                    case "airAttack":
                        airAttack = false;
                        break;
                    case "distanceAttack":
                        distanceAttack = false;
                        break;
                }
                activeAbilities[0] = activeAbilities[1];
                activeAbilities[1] = activeAbilities[2];
                activeAbilities[2] = ability.ToString();
            }
        }
    }

    //M�todo para desactivar habilidades
    public void DeactivateAbilities(string ability)
    {
        for (int a = 0; a < activeAbilities.Length; a++)
        {
            if (activeAbilities[a] == ability)
            {
                switch(a)
                {
                    case 0:
                        switch (activeAbilities[0])
                        {
                            case "airDash":
                                airDash = false;
                                break;
                            case "chargedAttack":
                                chargedAttack = false;
                                break;
                            case "attackCombo":
                                attackCombo = false;
                                break;
                            case "airAttack":
                                airAttack = false;
                                break;
                            case "distanceAttack":
                                distanceAttack = false;
                                break;
                        }
                        activeAbilities[0] = activeAbilities[1];
                        activeAbilities[1] = activeAbilities[2];
                        activeAbilities[2] = null;
                        break;

                    case 1:
                        switch (activeAbilities[1])
                        {
                            case "airDash":
                                airDash = false;
                                break;
                            case "chargedAttack":
                                chargedAttack = false;
                                break;
                            case "attackCombo":
                                attackCombo = false;
                                break;
                            case "airAttack":
                                airAttack = false;
                                break;
                            case "distanceAttack":
                                distanceAttack = false;
                                break;
                        }
                        activeAbilities[1] = activeAbilities[2];
                        activeAbilities[2] = null;
                        break;

                    case 2:
                        switch (activeAbilities[2])
                        {
                            case "airDash":
                                airDash = false;
                                break;
                            case "chargedAttack":
                                chargedAttack = false;
                                break;
                            case "attackCombo":
                                attackCombo = false;
                                break;
                            case "airAttack":
                                airAttack = false;
                                break;
                            case "distanceAttack":
                                distanceAttack = false;
                                break;
                        }
                        activeAbilities[2] = null;
                        break;
                }

                
            }
        }
    }

    //M�todo para saber si hay una habilidad dentro del array
    public bool IsAbilityIn(string ability)
    {
        for(int a = 0; a < activeAbilities.Length; a++)
        {
            if (activeAbilities[a] == ability)
            {
                switch (a)
                {
                    case 0:
                        switch (activeAbilities[0])
                        {
                            case "airDash":
                                return true;
                            case "chargedAttack":
                                return true;
                            case "attackCombo":
                                return true;
                            case "airAttack":
                                return true;
                            case "distanceAttack":
                                return true;
                        }
                        break;
                    case 1:
                        switch (activeAbilities[1])
                        {
                            case "airDash":
                                return true;
                            case "chargedAttack":
                                return true;
                            case "attackCombo":
                                return true;
                            case "airAttack":
                                return true;
                            case "distanceAttack":
                                return true;
                        }
                        break;
                    case 2:
                        switch (activeAbilities[2])
                        {
                            case "airDash":
                                return true;
                            case "chargedAttack":
                                return true;
                            case "attackCombo":
                                return true;
                            case "airAttack":
                                return true;
                            case "distanceAttack":
                                return true;
                        }
                        break;
                }
            }
        }
        return false;
    }
    
}
