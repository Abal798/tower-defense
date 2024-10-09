using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float bulletSpeed = 1;
    public float dammage;
    public GameObject target;

    void Update()
    {
        MoveTowardsTarget();
    }
    
    void MoveTowardsTarget()
    {
        Vector3 TargetDirection = new Vector3(transform.position.x - target.transform.position.x,
            transform.position.y - target.transform.position.y, 0).normalized;

        transform.position -= TargetDirection * bulletSpeed * Time.deltaTime;
    }
}
