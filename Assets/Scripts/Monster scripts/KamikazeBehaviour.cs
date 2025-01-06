using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KamikazeBehaviour : MonoBehaviour
{
    public MonsterStats MS;
    
    public bool targetTower;
    public Collider2D tower;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent != null)
        {
            if(collider.transform.parent.CompareTag("Tower"))
            {
                collider.gameObject.GetComponent<TowerTakingDamage>().TakeDamage(1000000);
                Destroy(gameObject);
                
            }
        }
        
        if(collider.CompareTag("Base"))
        {
            Destroy(gameObject);
        }
    }

    
}
