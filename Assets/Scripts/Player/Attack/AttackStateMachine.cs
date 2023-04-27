using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachine : MonoBehaviour
{
    //Estado actual
    public MeleBaseState currentState;
    //Estado siguiente
    MeleBaseState nextState;
    //Estado principal
    MeleBaseState mainState;

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

    //Método para cambiar el estado actual
    public void SetState(MeleBaseState newState)
    {
        nextState = null;
        if (currentState != null)
            currentState.OnExit();
        currentState = newState;
        currentState.OnStart();
    }

    //Método para cambiar al siguiente estado
    public void SetNextState(MeleBaseState newState)
    {
        if (newState != null)
            nextState = newState;
    }

    //Método para cambiar al estado principal
    public void SetNextStateToMain()
    {
        nextState = mainState;
    }

    
}
