using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleBaseState : AttackStates
{
    //Referencia al animador
    public Animator anim;

    //Duración de los estados antes de pasar al siguiente
    public float duration;
    //Variable que cuemprueba si continua el siguiente ataque de la secuencia
    bool shouldCombo;
    //Variable para saber que ataque de la secuencia reproducir
    int attackIndex;

    public void OnEnter(AttackStateMachine stateMachine)
    {
        base.OnStart(stateMachine);

    }

}
