using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDeathBehaviour : MonoBehaviour
{
    [Header("modifiable")]
    public float healthPoints;
    
    [Header("a renseigner (les GD, tjr pas toucher)")]
    

    public GameObject deathParticules;
    
    [Header("automatique")]
    public float incomingDamage = 0;
    private float totalHealthPoints;
    public RessourcesManager RM;
    
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
            Destroy(gameObject,5f);
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
            if (other.gameObject.tag == "water")
            {
                RM.waterSoul += 1;
            }
            if (other.gameObject.tag == "plant")
            {
                RM.plantSoul += 1;
            }
            Death();
        }
    }

    void Death()
    {
        {
            GameObject newParticules = Instantiate(deathParticules, transform.position, quaternion.identity);
            Destroy(newParticules,1f);
            Destroy(gameObject);
            Destroy(newParticules,0.3f);
        }
    }
}
