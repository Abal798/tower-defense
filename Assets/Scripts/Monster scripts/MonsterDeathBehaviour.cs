using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDeathBehaviour : MonoBehaviour
{
    [Header("modifiable")]
    public float healthPoints;
    
    [Header("a renseigner (les GD, tjr pas toucher)")]
    public RessourcesManager RM;
    
    [Header("automatique")]
    public float incomingDamage = 0;
    private float totalHealthPoints;

    void Start()
    {
        totalHealthPoints = healthPoints;
        gameObject.layer = LayerMask.NameToLayer("Ennemy");
    }
    private void Update()
    {
        if (incomingDamage >= totalHealthPoints)
        {
            gameObject.layer = LayerMask.NameToLayer("deadEnnemy");
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
        healthPoints -= 1;
        if (healthPoints <= 0)
        {
            if (other.gameObject.tag == "fire")
            {
                RM.fireSoul += 1;
            }
            if (other.gameObject.tag == "electric")
            {
                RM.electricSoul += 1;
            }
            if (other.gameObject.tag == "ice")
            {
                RM.iceSoul += 1;
            }
            Death();
        }
    }

    void Death()
    {
        {
            Destroy(gameObject);
        }
    }
}
