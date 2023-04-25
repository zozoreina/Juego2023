using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleBaseState : AttackStates
{
    //Referencia al Animator
    protected Animator anim;
    //Referencia a la Clase AttackStateMachine
    protected AttackStateMachine StateMachine;

    //Duración de los estados antes de pasar al siguiente
    public float duration;
    //Variable que cuemprueba si continua el siguiente ataque de la secuencia
    protected bool shouldCombo;
    //Variable para saber que ataque de la secuencia reproducir
    protected int attackIndex;

    public override void OnStart()
    {
        base.OnStart();
        anim = PlayerController.sharedInstance.GetComponent<Animator>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetButtonDown("Fire1"))
            shouldCombo = true;
    }

    public override void OnExit()
    {
        base.OnExit();
    }



}
