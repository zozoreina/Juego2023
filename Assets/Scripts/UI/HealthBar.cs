using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Image healthBar;

    

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = PlayerHealthController.sharedInstance.currentHealth / PlayerHealthController.sharedInstance.maxHealth;
    }
}
