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
    //Referencia a la Máquina de estados
    public AttackStateMachine StateMachine;
    //Referencia para conocer el siguiente Estado al que ir
    protected MeleBaseState nextState;
    

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
    public MeleBaseState(float _duration, float _time, bool _shouldCombo, Animator _anim)
    {
        _duration = duration;
        _time = time;
        _shouldCombo = shouldCombo;
        _anim = anim;
        stage = EVENT.Start;
    }

    public virtual void OnStart()
    {
        stage = EVENT.Update;
    }

    public virtual void OnUpdate()
    {
        stage = EVENT.Update;

        time += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && PlayerController.sharedInstance.isGrounded) 
            shouldCombo = true;
            
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
    public void SetNextStateToMain()
    {
        nextState = new IdleCombat(duration, time, shouldCombo, anim);
    }
}

public class IdleCombat : MeleBaseState
{
    public IdleCombat(float _duration, float _time, bool _shouldCombo, Animator _anim) : base(_duration, _time, _shouldCombo, _anim)
    {
        name = STATE.IDLE;
    }

    public override void OnStart()
    {
        base.OnStart();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (shouldCombo)
        {
            Debug.Log("IdleEstabaActivo");
            nextState = new Ground1(duration, time, shouldCombo, anim);
            stage = EVENT.Exit;
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}

public class Ground1 : MeleBaseState
{
    public Ground1(float _duration, float _time, bool _shouldCombo, Animator _anim) : base(_duration, _time, _shouldCombo, _anim)
    {
        name = STATE.ATTACKENTRY;
    }


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
                nextState = new Ground2(duration, time, shouldCombo, anim);
            else
                SetNextStateToMain();

        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

public class Ground2 : MeleBaseState
{
    public Ground2(float _duration, float _time, bool _shouldCombo, Animator _anim) : base(_duration, _time, _shouldCombo, _anim)
    {
        name = STATE.ATTACKCOMBO;
    }
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
                nextState = new Ground3(duration, time, shouldCombo, anim);
            else
                SetNextStateToMain();

        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
public class Ground3 : MeleBaseState
{
    public Ground3(float _duration, float _time, bool _shouldCombo, Animator _anim) : base(_duration, _time, _shouldCombo, _anim)
    {
        name = STATE.ATTACKEXIT;
    }

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
                nextState = new Ground4(duration, time, shouldCombo, anim);
            else if (shouldCombo && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo && !PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().attackCombo)
                nextState = new Ground5(duration, time, shouldCombo, anim);
            else
                SetNextStateToMain();

        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

public class Ground4 : MeleBaseState
{
    public Ground4(float _duration, float _time, bool _shouldCombo, Animator _anim) : base(_duration, _time, _shouldCombo, _anim)
    {
        name = STATE.COMP1ATTACK;
    }

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
                nextState = new Ground5(duration, time, shouldCombo, anim);
            else
                SetNextStateToMain();

        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

public class Ground5 : MeleBaseState
{
    public Ground5(float _duration, float _time, bool _shouldCombo, Animator _anim) : base(_duration, _time, _shouldCombo, _anim)
    {
        name = STATE.COMP2ATTACK;
    }

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
            SetNextStateToMain();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
