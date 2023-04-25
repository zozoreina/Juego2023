using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEntryState : AttackStates
{
    public override void OnStart()
    {
        base.OnStart();

        AttackStates toNextState = (AttackStates)new Ground1();
        attackStateMachine.SetNextState(toNextState);
    }
}
