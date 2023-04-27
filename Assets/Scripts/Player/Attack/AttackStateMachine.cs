using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachine : MonoBehaviour
{
    //Estado actual
    public MeleBaseState currentState;
    //Referencia al Animator
    Animator anim;

    //Variable de duración de estados
    float duration;
    //Variable que guarda el tiempo
    float time;
    //Variable para saber si puede atacar
    bool shouldCombo;

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentState = new IdleCombat(duration, time, shouldCombo, anim);
    }

    private void Update()
    {
        currentState = currentState.Process();
    }

}
