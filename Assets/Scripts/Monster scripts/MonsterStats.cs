using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    public int health;
    public float basicSpeed = 3;
    public float speed;
    public float damages;
    public int type;
    public float damageFactorbonus = 1.5f;
    public float damageFactorMalus = 0.75f;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
