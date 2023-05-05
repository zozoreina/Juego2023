using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachine : MonoBehaviour
{
    //Estado actual
    MeleBaseState currentState;
    //Referencia al Animator
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentState = new IdleCombat(gameObject ,anim);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }


    public void SetCurrentStateToMain()
    {
        currentState = new IdleCombat(gameObject, anim);
    }

}
