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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("bullets"))
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
}
