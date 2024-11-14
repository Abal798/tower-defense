using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int health;
    public float basicSpeed;
    public float speed;
    public float damages;
    public int type;
    public float damageFactorbonus = 1.5f;
    public float damageFactorMalus = 0.75f;

    private void Awake()
    {
        speed = basicSpeed;
    }

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        speed = basicSpeed;
        
        if (type == 1)
        {
            renderer.material.color = Color.red;
        }
        if (type == 2)
        {
            renderer.material.color = Color.blue;
        }
        if (type == 3)
        {
            renderer.material.color = Color.green;
        }
    }
}
