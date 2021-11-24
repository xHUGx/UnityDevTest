using System;
using System.Collections;
using System.Collections.Generic;
using Demo;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public float health;
    public float maxHealth;
    public static int killed;
    public Slider slider;

    private void Start()
    {
        killed = 0;
        health = maxHealth;
        slider.value = CalculateHealth();
    }

    private void Update()
    {
        slider.value = CalculateHealth();
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
