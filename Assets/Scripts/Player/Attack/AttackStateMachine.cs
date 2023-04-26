using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachine : MonoBehaviour
{
    //Estado actual
    public AttackStates currentState;
    //Estado siguiente
    AttackStates nextState;

    private void Awake()
    {
        SetNextStateToMain();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextState != null)
            SetState(nextState);

        if (currentState != null)
            currentState.OnUpdate();
    }

    //M�todo para cambiar el estado actual
    public void SetState(AttackStates newState)
    {
        nextState = null;
        if (currentState != null)
            currentState.OnExit();
        currentState = newState;
        currentState.OnStart();
    }

    //M�todo para cambiar al siguiente estado
    public void SetNextState(AttackStates newState)
    {
        if (newState != null)
            nextState = newState;
    }

    //M�todo para cambiar al estado principal
    public void SetNextStateToMain()
    {
        nextState = new IdleCombat();
    }
}
