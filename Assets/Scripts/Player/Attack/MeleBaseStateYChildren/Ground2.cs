using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground2 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuraci�n del ataque
        duration = .5f;
        anim.SetTrigger("Attack2");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resoluci�n del ataque
        if (time >= duration)
        {
            if (shouldCombo)
                StateMachine.setNextState(new Ground3());
            else
                StateMachine.setNextStateToMain();

        }
    }
}
