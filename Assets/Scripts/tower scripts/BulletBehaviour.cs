using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float bulletSpeed = 1;
    public float dammage;
    public GameObject target;
    public List<int> bulletElements = new List<int>();

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void MoveTowardsTarget()
    {
        Vector3 TargetDirection = new Vector3(transform.position.x - target.transform.position.x,
            transform.position.y - target.transform.position.y, 0).normalized;

        transform.position -= TargetDirection * bulletSpeed * Time.deltaTime;
    }

    
}
