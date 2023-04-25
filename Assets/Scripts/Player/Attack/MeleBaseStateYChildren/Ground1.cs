using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
