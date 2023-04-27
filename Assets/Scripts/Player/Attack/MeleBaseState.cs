using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleBaseState
{
    //Referencia al Animator
    protected Animator anim;
    //Referencia a la Máquina de estados
    public AttackStateMachine StateMachine;

    //Duración de los estados antes de pasar al siguiente
    public float duration;
    //Variable que cuemprueba si continua el siguiente ataque de la secuencia
    protected bool shouldCombo;
    //Variable para saber que ataque de la secuencia reproducir
    protected int attackIndex;
    //Variable donde se establece el tiempo que duran los estados activos
    protected float time;
    
    public virtual void OnStart()
    {
        anim = PlayerController.sharedInstance.GetComponent<Animator>();
    }

    public virtual void OnUpdate()
    {
        time += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && anim.GetBool("AttackWindow"))
            shouldCombo = true;
            
    }

    public virtual void OnExit()
    {

    }
}

public class IdleCombat : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("IdleEstabaActivo");
            StateMachine.SetNextState(new Ground1());
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}

public class Ground1 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuración del ataque
        duration = .5f;
        anim.SetTrigger("Attack1");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resolución del ataque
        if (time >= duration)
        {
            if (shouldCombo)
                StateMachine.SetNextState(new Ground2());
            else
                StateMachine.SetNextStateToMain();

        }
    }
}

public class Ground2 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuración del ataque
        duration = .5f;
        anim.SetTrigger("Attack2");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resolución del ataque
        if (time >= duration)
        {
            if (shouldCombo)
                StateMachine.SetNextState(new Ground3());
            else
                StateMachine.SetNextStateToMain();

        }
    }
}
public class Ground3 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuración del ataque
        duration = .75f;
        anim.SetTrigger("Attack3");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resolución del ataque
        if (time >= duration)
        {
            if (shouldCombo && PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().attackCombo)
                StateMachine.SetNextState(new Ground4());
            else if (shouldCombo && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo && !PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().attackCombo)
                StateMachine.SetNextState(new Ground5());
            else
                StateMachine.SetNextStateToMain();

        }
    }
}

public class Ground4 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuración del ataque
        duration = .5f;
        PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().anim.SetTrigger("ComboAttack");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resolución del ataque y preparación del siguiente estado
        if (time >= duration)
        {
            if (shouldCombo && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo)
                StateMachine.SetNextState(new Ground5());
            else
                StateMachine.SetNextStateToMain();

        }
    }
}

public class Ground5 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuración del ataque
        duration = .5f;
        PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().anim.SetTrigger("ComboAttack");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resolución del ataque
        if (time >= duration)
            StateMachine.SetNextStateToMain();
    }
}
