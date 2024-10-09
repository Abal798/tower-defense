using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            Debug.Log("je suis sensé mourir");
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(other.gameObject);
        healthPoints -= 1;
    }
}
