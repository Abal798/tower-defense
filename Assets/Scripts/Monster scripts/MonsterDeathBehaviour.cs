using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDeathBehaviour : MonoBehaviour
{
    [Header("modifiable")]
    
    public float soulReward = 1f;
    
    
    [Header("a renseigner (les GD, tjr pas toucher)")]
    
    public MonsterStats MS;
    public GameObject deathParticules;

    
    [Header("automatique")]
    public float healthPoints;
    public float incomingDamage = 0;
    private float totalHealthPoints;
    public RessourcesManager RM;
    
    private float damageFactorbonus;
    private float damageFactorMalus;
    
    
    void Start()
    {
        damageFactorbonus = MS.damageFactorbonus;
        damageFactorMalus = MS.damageFactorMalus;
        healthPoints = MS.health;
        totalHealthPoints = healthPoints;
        gameObject.layer = LayerMask.NameToLayer("Ennemy");
    }
    private void Update()
    {
        if (incomingDamage >= totalHealthPoints)
        {
            gameObject.layer = LayerMask.NameToLayer("deadEnnemy");
            Destroy(gameObject,2f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullets"))
        {
            TakeDamages(other);
                    
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
            healthPoints -= 60;
            if (healthPoints <= 0)
            {
                Death();
            }
            StartCoroutine(Dot(3f));
        }
        else if (damageID == 2)
        {
            healthPoints -= 80;
            if (healthPoints <= 0)
            {
                Death();
            }
            StartCoroutine(Dot(2f));
        }
        else if (damageID == 3)
        {
            healthPoints -= 100;
            if (healthPoints <= 0)
            {
                Death();
            }
        }
    }

    IEnumerator Dot( float dammages)
    {
        while (healthPoints > 0)
        {
            healthPoints -= dammages;
            if (healthPoints <= 0)
            {
                Death();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void TakeDamages(Collider2D other)
    {
        float damages = other.gameObject.GetComponent<BulletBehaviour>().dammage;
        List<int> bulletType = other.gameObject.GetComponent<BulletBehaviour>().bulletElements;
        
        if (MS.type == 0)
        {
            healthPoints -= damages;
        }

        if (MS.type == 1)
        {
            foreach (var i in bulletType)
            {
                if (i == 3)
                {
                    damages *= damageFactorbonus;
                }

                if (i == 2)
                {
                    damages *= damageFactorMalus;
                }
            }

            healthPoints -= damages;
        }
        if (MS.type == 2)
        {
            foreach (var i in bulletType)
            {
                if (i == 1)
                {
                    damages *= damageFactorbonus;
                }

                if (i == 3)
                {
                    damages *= damageFactorMalus;
                }
            }

            healthPoints -= damages;
        }
        if (MS.type == 3)
        {
            foreach (var i in bulletType)
            {
                if (i == 2)
                {
                    damages *= damageFactorbonus;
                }

                if (i == 1)
                {
                    damages *= damageFactorMalus;
                }
            }

            healthPoints -= damages;
        }
        
    }
}
