using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground4 : MeleBaseState
{
    public override void OnStart()
    {
        base.OnStart();

        //Configuraci�n del ataque
        duration = .5f;
        PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().anim.SetTrigger("ComboAttack");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //Resoluci�n del ataque y preparaci�n del siguiente estado
        if (time >= duration)
        {
            if (shouldCombo && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo)
                StateMachine.SetNextState(new Ground5());
            else
                StateMachine.SetNextStateToMain();

        }
    }
}
