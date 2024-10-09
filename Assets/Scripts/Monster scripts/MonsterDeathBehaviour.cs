using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathBehaviour : MonoBehaviour
{

    public float healthPoints;
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

        if (healthPoints <= 0)
        {
            Destroy(transform);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject, 0.1f);
        healthPoints -= 1;
    }
}
