using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStates
{
    //Variable donde se establece el tiempo que duran los estados activos
    protected float time;

    //Referencia a la Máquina de estados
    public AttackStateMachine attackStateMachine;
    
    public virtual void OnStart()
    {

    }

    public virtual void OnUpdate()
    {
        time += Time.deltaTime;
    }

    public virtual void OnExit()
    {

    }
}
