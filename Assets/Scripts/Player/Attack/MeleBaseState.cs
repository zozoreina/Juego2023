using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleBaseState
{
    //Lista de los estados a los que podemos ir
    public enum STATE
    {
        IDLE, ATTACKENTRY, ATTACKCOMBO, ATTACKEXIT, COMP1ATTACK, COMP2ATTACK
    };

    //Lista de los eventos que tendran lugar dentro de los estados
    public enum EVENT
    {
        Start, Update, Exit
    };

    //Referencia al Animator
    protected Animator anim;
    //Referencia para conocer el siguiente Estado al que ir
    protected MeleBaseState nextState;
    //Referencia al jugador, sobre el que queremos aplicar la máquina de estados
    protected GameObject player;
    

    //Nombre del estado actual
    public STATE name;
    //Para conocer el evento actual
    protected EVENT stage;
    //Duración de los estados antes de pasar al siguiente
    protected float duration;
    //Variable que cuemprueba si continua el siguiente ataque de la secuencia
    protected bool shouldCombo;
    //Variable donde se establece el tiempo que duran los estados activos
    protected float time;


    //El constructor
    public MeleBaseState(GameObject _player, Animator _anim)
    {
        anim = _anim;
        stage = EVENT.Start;
        player = _player;
    }

    //Métodos para el desarrollo de cada estado
    public virtual void OnStart()
    {
        stage = EVENT.Update;
    }

    public virtual void OnUpdate()
    {
        stage = EVENT.Update;
    }

    public virtual void OnExit()
    {
        stage = EVENT.Exit;
    }

    
    //Método por el que cambiamos entre estados
    public MeleBaseState Process()
    {
        //Si el evento en el que estoy es el de entrada, hago el método correspondiente de entrada
        if (stage == EVENT.Start) OnStart();
        //Si el evento en el que estoy es el de update, hago el método correspondiente
        if (stage == EVENT.Update) OnUpdate();
        //Si el evento en el que estoy es el de salida, hago el método correspondiente
        if (stage == EVENT.Exit)
        {
            OnExit();
            //Y devolvemos el siguiente estado al que ir
            return nextState;
        }
        //Devolvemos el resultado del método
        return this;
    }

    //Método por el que volvemos al estado principal
    public MeleBaseState SetNextStateToMain()
    {
        return nextState = new IdleCombat(player, anim);
    }
}

//Clases hijas de la clase estado

//Clase para cuando el jugador no este atacando
public class IdleCombat : MeleBaseState
{
    public IdleCombat(GameObject _player, Animator _anim) : base(_player, _anim)
    {
        name = STATE.IDLE;
    }

    public override void OnStart()
    {
        shouldCombo = false;
        Debug.Log("EnterIdle");
        base.OnStart();
    }


     public override void OnUpdate()
     {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && PlayerController.sharedInstance.isGrounded && PlayerController.sharedInstance.canPlay)
            shouldCombo = true;
        if (shouldCombo)
        {
            
            nextState = new Ground1(player, anim);
            stage = EVENT.Exit;
        }
     }

    public override void OnExit()
    {
        Debug.Log("IdleEstabaActivo");
        base.OnExit();
    }
}

//Clase para cuando el jugador este dando el primer golpe
public class Ground1 : MeleBaseState
{
    public Ground1(GameObject _player, Animator _anim) : base(_player, _anim)
    {
        name = STATE.ATTACKENTRY;
    }


    public override void OnStart()
    {
        //Configuración del ataque
        duration = .5f;
        time = 0f;
        shouldCombo = false;
        anim.SetTrigger("Attack1");

        base.OnStart();
    }

    public override void OnUpdate()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && PlayerController.sharedInstance.isGrounded && PlayerController.sharedInstance.canPlay)
            shouldCombo = true;
        //Resolución del ataque
        if (time >= duration)
        {
            
            if (shouldCombo)
            {
                nextState = new Ground2(player, anim);
                stage = EVENT.Exit;
            }
            else
            {
                
                SetNextStateToMain();
                stage = EVENT.Exit;
            }
        }
    }

    public override void OnExit()
    {
        Debug.Log("WasGround1");
        anim.ResetTrigger("Attack1");
        base.OnExit();
    }
}

//Clase para cuando el jugador este dando el 2º golpe
public class Ground2 : MeleBaseState
{
    public Ground2(GameObject _player, Animator _anim) : base(_player, _anim)
    {
        name = STATE.ATTACKCOMBO;
    }
    public override void OnStart()
    {
        //Configuración del ataque
        duration = .5f;
        time = 0f;
        shouldCombo = false;
        anim.SetTrigger("Attack2");
        base.OnStart();
    }

    public override void OnUpdate()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && PlayerController.sharedInstance.isGrounded && PlayerController.sharedInstance.canPlay)
            shouldCombo = true;

        //Resolución del ataque
        if (time >= duration)
        {
            if (shouldCombo)
            {
                nextState = new Ground3(player, anim);
                stage = EVENT.Exit;
            }

            else
            {
                
                SetNextStateToMain();
                stage = EVENT.Exit;
            }

        }
    }

    public override void OnExit()
    {
        Debug.Log("WasGround2");
        base.OnExit();
    }
}

//Clase para cuando el jugador este dando el 3º golpe
public class Ground3 : MeleBaseState
{
    public Ground3(GameObject _player, Animator _anim) : base(_player, _anim)
    {
        name = STATE.ATTACKEXIT;
    }

    public override void OnStart()
    {
        //Configuración del ataque
        duration = .75f;
        time = 0f;
        shouldCombo = false;
        anim.SetTrigger("Attack3");
        base.OnStart();

    }

    public override void OnUpdate()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && PlayerController.sharedInstance.isGrounded && PlayerController.sharedInstance.canPlay)
            shouldCombo = true;

        //Resolución del ataque
        if (time >= duration)
        {
            if (shouldCombo && PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().attackCombo)
            {

                nextState = new Ground4(player, anim);
                stage = EVENT.Exit;
            }
            else if (shouldCombo && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo && !PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().attackCombo)
            {

                nextState = new Ground5(player, anim);
                stage = EVENT.Exit;
            }
            else
            {
                
                SetNextStateToMain();
                stage = EVENT.Exit;
            }
        }
    }

    public override void OnExit()
    {
        Debug.Log("WasGround3");
        base.OnExit();
    }
}

//Clase para cuando el compañero este dando el 4º golpe
public class Ground4 : MeleBaseState
{
    public Ground4(GameObject _player, Animator _anim) : base(_player, _anim)
    {
        name = STATE.COMP1ATTACK;
    }

    public override void OnStart()
    {

        //Configuración del ataque
        duration = .5f;
        time = 0f;
        shouldCombo = false;
        PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().anim.SetBool("ComboAttack", true);
        base.OnStart();

    }

    public override void OnUpdate()
    {
        time += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && PlayerController.sharedInstance.isGrounded && PlayerController.sharedInstance.canPlay)
            shouldCombo = true;

        //Resolución del ataque y preparación del siguiente estado
        if (time >= duration)
        {
            if (shouldCombo && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo)
            {

                nextState = new Ground5(player, anim);
                stage = EVENT.Exit;
            }
            else
            {
                
                SetNextStateToMain();
                stage = EVENT.Exit;
            }

        }
    }

    public override void OnExit()
    {
        Debug.Log("WasGround4");
        PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().anim.SetBool("ComboAttack", false);
        base.OnExit();
    }
}

//Clase para cuando el compañero este dando el 5º golpe
public class Ground5 : MeleBaseState
{
    public Ground5(GameObject _player, Animator _anim) : base(_player, _anim)
    {
        name = STATE.COMP2ATTACK;
    }

    public override void OnStart()
    {

        //Configuración del ataque
        duration = .5f;
        time = 0f;
        shouldCombo = false;
        PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().anim.SetBool("ComboAttack", true);
        base.OnStart();

    }

    public override void OnUpdate()
    {
        time += Time.deltaTime;
        //Resolución del ataque
        if (time >= duration)
        {
            SetNextStateToMain();
            stage = EVENT.Exit;
        }
    }

    public override void OnExit()
    {
        PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().anim.SetBool("ComboAttack", false);
        Debug.Log("WasGround5");
        base.OnExit();
    }
}
