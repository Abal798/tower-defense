using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDealingDamagesBehaviour : MonoBehaviour
{
    public MonsterStats MS;

    public float damages;
    private bool justHited = false;
    public bool targetTower;
    public Collider2D tower;
    public Collider2D baseTower;
    private MonsterMouvementBehaviours monsterMouvementBehaviours;
    

    private void Awake()
    {
        damages = MS.damages;
    }

    private void Start()
    {
        monsterMouvementBehaviours = GetComponent<MonsterMouvementBehaviours>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.transform.parent != null)
        {
            if(collider.transform.parent.CompareTag("Tower"))
            {
                targetTower = true;
                tower = collider;
                monsterMouvementBehaviours.speed = 0;
                Debug.Log("je pense de plus en plus au suicide");
            }
        }
        
        if(collider.CompareTag("Base"))
        {
            targetTower = true;
            baseTower = collider;
            monsterMouvementBehaviours.speed = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetTower = false;
        tower = null;
        baseTower = null;
        monsterMouvementBehaviours.speed = MS.basicSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if (justHited == false && targetTower && tower != null)
        {
            justHited = true;
            StartCoroutine(Hited());
            if (tower != null)
            {
                tower.gameObject.GetComponent<TowerTakingDamage>().TakeDamage(damages);
            }
            if (tower == null && baseTower == null)
            {
                targetTower = false;
                monsterMouvementBehaviours.speed = MS.basicSpeed;
            
            }
        }
        
        if (justHited == false && targetTower && baseTower != null)
        {
            justHited = true;
            StartCoroutine(Hited());
            if (baseTower != null)
            {
                baseTower.gameObject.GetComponent<BaseScript>().TakeDamage(damages);
            }
            if (tower == null && baseTower == null)
            {
                targetTower = false;
                monsterMouvementBehaviours.speed = MS.basicSpeed;
            
            }
        }
    }
    
    IEnumerator Hited()
    {
        
        yield return new WaitForSeconds(1f);
        justHited = false;
        
    }
}
