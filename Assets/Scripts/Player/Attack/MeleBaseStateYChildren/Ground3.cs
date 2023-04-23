using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                StateMachine.setNextState(new Ground4());
            else if (shouldCombo && PlayerController.sharedInstance.companion2.GetComponent<CompanionController>().attackCombo && !PlayerController.sharedInstance.companion1.GetComponent<CompanionController>().attackCombo)
                StateMachine.setNextState(new Ground5());
            else
                StateMachine.setNextStateToMain();

        }
    }
}
