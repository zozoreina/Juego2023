using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackStates
{
    protected float time;
    
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
