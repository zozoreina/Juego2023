using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCharacter : MonoBehaviour
{
    //Referencia a la máquina de estados
    AttackStateMachine stateMachine;

    //Referencia a la hitbox
    public Collider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<AttackStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && stateMachine.currentState.GetType() == typeof(IdleCombat))
        {
            stateMachine.SetNextState(new Ground1());
        }
    }
}
