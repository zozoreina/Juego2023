using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpoitnController : MonoBehaviour
{
    //Declaramos un array donde guardar los checkpoints de la escena
    private Checkpoint[] checkpoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para desactivar checkpoints
    public void DesactivateCheckpoint()
    {
        for(int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

}
