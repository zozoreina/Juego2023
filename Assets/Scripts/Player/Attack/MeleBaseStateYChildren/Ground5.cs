using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
