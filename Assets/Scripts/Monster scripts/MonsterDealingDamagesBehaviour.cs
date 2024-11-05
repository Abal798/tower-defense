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
    private MonsterMouvementBehaviours monsterMouvementBehaviours;
    public float speedBackup;

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
        if(collider.transform.parent.CompareTag("Tower"))
        {
            targetTower = true;
            tower = collider;
            speedBackup = monsterMouvementBehaviours.speed;
            monsterMouvementBehaviours.speed = 0;
            Debug.Log("je pense de plus en plus au suicide");
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        targetTower = false;
        tower = null;
        monsterMouvementBehaviours.speed = speedBackup;
    }


    // Update is called once per frame
    void Update()
    {
        if (justHited == false && targetTower)
        {
            justHited = true;
            StartCoroutine(Hited());
            if (tower != null)
            {
                tower.gameObject.GetComponent<TowerTakingDamage>().TakeDamage(damages);
            }
            if (tower == null)
            {
                targetTower = false;
                monsterMouvementBehaviours.speed = speedBackup;
            
            }
        }
    }
    
    IEnumerator Hited()
    {
        yield return new WaitForSeconds(1f);
        justHited = false;
        
    }
}
