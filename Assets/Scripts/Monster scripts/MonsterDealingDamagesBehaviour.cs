using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDealingDamagesBehaviour : MonoBehaviour
{
    public MonsterStats MS;

    public float damages;
    private bool justHited = false;

    private void Awake()
    {
        damages = MS.damages;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (justHited == false)
        {
            justHited = true;
            StartCoroutine(Hited());
            //ajouter un truc auquel faire des domages
        }
    }
    
    IEnumerator Hited()
    {
        yield return new WaitForSeconds(1f);
        justHited = false;
    }
}
