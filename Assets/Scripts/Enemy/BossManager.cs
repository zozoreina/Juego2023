using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public string sceneToLoad;
    

    // Update is called once per frame
    void Update()
    {
        if (EnemyHealthController.sharedInstance.currentHealth == 0)
            FadeScreen.sharedInstance.FadeToBlack(); SceneManager.LoadScene(sceneToLoad);
    }
}
