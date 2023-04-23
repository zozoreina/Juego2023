using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachine : MonoBehaviour
{
    //Estado actual
    public AttackStates currentState;
    //Estado siguiente
    AttackStates nextState;

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }

    //M�todo para cambiar al siguiente estado
    public void setNextState(AttackStates newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnStart();
    }

    //M�todo para cambiar al estado principal
    public void setNextStateToMain()
    {
        nextState = new IdleCombat();
    }
}
