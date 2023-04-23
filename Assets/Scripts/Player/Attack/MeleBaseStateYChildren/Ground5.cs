using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground5 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuraci�n del ataque
        duration = .5f;
        anim.SetTrigger("Attack5");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resoluci�n del ataque
        if (time >= duration)
            StateMachine.setNextStateToMain();
    }
}
