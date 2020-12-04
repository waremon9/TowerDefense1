using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject HealthBarUI;
    public Slider slider;

    private float health;
    private float maxHealth;

    private void Start()
    {
        HealthBarUI.SetActive(false);//don't show when full health

        health = maxHealth = gameObject.GetComponent<Enemy>().getMaxHP();//get monster health
        slider.value = CalculateHealth();//update slider
    }

    public void updateHealthBar()
    {
        health = gameObject.GetComponent<Enemy>().getHP();//update value

        if (health < maxHealth)//if took damage, show the health
        {
            HealthBarUI.SetActive(true);
        }
        else
        {
            HealthBarUI.SetActive(false);//no heal yet but it's here
        }

        if (health <= 0)//monster dead
        {
            GameManager.Instance.DeleteEnnemy(gameObject, true);
        }

        if (health > maxHealth)//no heal yet and if it happened, it will be in the Enemy script, but safety measure just in case.
        {
            health = maxHealth;
        }

        slider.value = CalculateHealth();//update slider
    }

    private float CalculateHealth()//return health %age
    {
        return health / maxHealth;
    }
}
