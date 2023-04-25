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
            SetNextState(nextState);

        if (currentState != null)
            currentState.OnUpdate();
    }

    //Método para cambiar al siguiente estado
    public void SetNextState(AttackStates newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnStart();
    }

    //Método para cambiar al estado principal
    public void SetNextStateToMain()
    {
        nextState = new IdleCombat();
    }
}
