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
    public float soulReward = 1f;
    
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
        healthPoints -= other.gameObject.GetComponent<BulletBehaviour>().dammage;
        
        if (healthPoints <= 0)
        {
            for (int i = 0; i < other.gameObject.GetComponent<BulletBehaviour>().bulletElements.Count; i++)
            {
                if (other.gameObject.GetComponent<BulletBehaviour>().bulletElements[i] == 1)
                {
                    RM.fireSoul += soulReward;
                }
                if (other.gameObject.GetComponent<BulletBehaviour>().bulletElements[i] == 2)
                {
                    RM.waterSoul += soulReward;
                }
                if (other.gameObject.GetComponent<BulletBehaviour>().bulletElements[i] == 3)
                {
                    RM.plantSoul += soulReward;
                }
            }
            Death();
        }
        Destroy(other.gameObject);
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

    public void DamageSpell(int damageID)
    {
        if (damageID == 1)
        {
            totalHealthPoints -= 60;
            if (totalHealthPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (damageID == 2)
        {
            totalHealthPoints -= 80;
            if (totalHealthPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
        else if (damageID == 3)
        {
            totalHealthPoints -= 100;
            if (totalHealthPoints <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
