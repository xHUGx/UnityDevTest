using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public static int killed = 0;
    public Slider slider;

    private void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    private void Update()
    {
        slider.value = CalculateHealth();
        Debug.Log(killed);
        if (health <= 0)
        {
            Destroy(gameObject);
            killed++;
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        CalculateHealth();
    }

    float CalculateHealth()
    {
        return health / maxHealth; 
    }
}
